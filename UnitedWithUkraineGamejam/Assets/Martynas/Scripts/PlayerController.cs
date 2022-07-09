using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    float movementSpeed = 4f;

    Vector3 forward;

    bool jump = false;
    bool movement = true;
    bool ground = false;
    public float jumpHeight = 0f;
    public float jumpSpeed = 0f;

    private float charger = 0f;
    public float chargeTime = 3f;

    bool falling = false;

    [TagSelector]
    public string[] jumptableTags = new string[] { };

    //---------------

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits

    [Space(15)]
    public GameObject indicator;

    [Space(15)]
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem landParticles;
    [SerializeField] ParticleSystem chargeParticles;

    [SerializeField] GameObject waterParticles;
    [SerializeField] GameObject borschtParticles;

    private void Start()
	{
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
	}

    private void Update()
    {
        if (Input.GetMouseButton(0) && !jump)
        {

            charger += Time.deltaTime;

            charger = charger / chargeTime;
            if (charger > 1)
            {
                charger = 1;
            }
            if (!chargeParticles.isPlaying)
                chargeParticles.Play();

            var emission = chargeParticles.emission;
            emission.rateOverTime = 20 * charger;

        }

        if (Input.GetMouseButtonUp(0) && !jump)
        {
            movement = false;
            if (chargeParticles.isPlaying)
                chargeParticles.Stop();
            StartCoroutine(Jump());
        }

    }

	private void FixedUpdate()
	{
        Move();
        // Cast a ray from the camera to the mouse cursor
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            // ...and if that object is the ground...
            if (cameraRayHit.transform.tag == "Ground")
            {
                // ...make the cube rotate (only on the Y axis) to face the ray hit's position 

                Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                indicator.transform.position = cameraRayHit.point;
                //Vector3 thisObjectPosition = new Vector3(transform.position.x, ca.transform.position.y, transform.position.z);

                //Vector3 direction = targetPosition - transform.position;
                //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);

                Debug.DrawRay(transform.position, indicator.transform.position - transform.position, Color.red);
                //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, (float)(1 - Mathf.Exp(-sensitivity * Time.deltaTime)));
                if (movement)
                {
                    transform.LookAt(targetPosition);
                }
            }
        }
    }

	private void Move()
    {
        if (jump)
        {
            if (falling)
            {
                transform.position += transform.forward * movementSpeed * Time.deltaTime;
            }
            else
			{
                transform.position += transform.forward * (movementSpeed * 3f) * Time.deltaTime;
			}
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.red);
	}

    IEnumerator Jump()
	{
        jumpParticles.Play();
        ground = false;
        float originalHeight = transform.position.y;
        float maxHeight = originalHeight + (jumpHeight*charger);
        rb.useGravity = false;
        jump = true;
        yield return null;

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

        while (transform.position.y < maxHeight && !ground)
		{
            //Debug.Log(rb.velocity.y);
            transform.position += transform.up * Time.deltaTime * jumpSpeed;
            yield return null;
        }

        rb.useGravity = true;

		while (!ground)
		{
            falling = true;
            transform.position -= transform.up * Time.deltaTime * jumpSpeed * 3f;
			yield return null;
		}

        falling = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;
        jump = false;
        charger = 0;
        movement = true;
        landParticles.Play();

        yield return null;
	}

	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log(collision.gameObject.tag);
        foreach(string item in jumptableTags)
		{
            if(collision.gameObject.tag == item)
			{
                ground = true;
            }
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Water"))
        {
            PlayParticles(waterParticles);
        }
    }

    void PlayParticles(GameObject gameobject)
    {
        var go = Instantiate(gameobject, transform.position, Quaternion.identity);
        if(!go.GetComponent<ParticleSystem>().isPlaying)
            go.GetComponent<ParticleSystem>().Play();
        Destroy(go, 3f);
    }

}
