using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    float movementSpeed = 4f;

    Vector3 forward, right;

    bool jump = false;
    bool movement = true;
    public float jumpHeight = 0f;
    public float jumpSpeed = 0f;

    private float charger = 0f;
    public float chargeTime = 3f;

    Vector3 rightMovement;
    Vector3 upMovement;
    Vector3 destination;

    private void Start()
	{
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
        if (Input.GetKey(KeyCode.Space) && !jump)
        {
            charger += Time.deltaTime;
            movement = false;
        }
        Move();

        if (Input.GetKeyUp(KeyCode.Space) && !jump)
        {
            StartCoroutine(Jump());
        }
    }

    private void Move()
    {
        if (!jump)
        {
            rightMovement = right * movementSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            upMovement = forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        }
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);
        if (heading != Vector3.zero && movement)
        {
            transform.forward = heading;
            destination = transform.forward;
        }
        if(jump)
		{
            transform.position += destination * movementSpeed * Time.deltaTime;
        }
	}

    IEnumerator Jump()
	{
        charger = charger / chargeTime;
        if(charger > 1)
		{
            charger = 1;
		}
        float originalHeight = transform.position.y;
        float maxHeight = originalHeight + (jumpHeight*charger);
        rb.useGravity = false;
        jump = true;
        yield return null;

        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

        while(transform.position.y < maxHeight)
		{
            transform.position += transform.up * Time.deltaTime * jumpSpeed;
            yield return null;
        }

        rb.useGravity = true;

        while (transform.position.y > originalHeight)
		{
            transform.position -= transform.up * Time.deltaTime * jumpSpeed;
            yield return null;
		}


        rb.useGravity = true;
        jump = false;
        charger = 0;
        movement = true;
        yield return null;
	}
}
