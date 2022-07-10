using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    private float floatTime = 3f;
    [SerializeField] float startingTime = 3f;
    private Transform CameraLocation;
    private bool startMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        CameraLocation = Camera.main.transform;
        floatTime = startingTime;
        this.gameObject.SetActive(false);
    }

	private void FixedUpdate()
	{
        if (startMoving)
        {
            floatTime -= Time.deltaTime;
        }
    }

	public void Collected(string text)
	{
        floatTime = startingTime;
        GetComponent<TextMeshPro>().text = text;
        startMoving = true;
        StartCoroutine(floatUp());
	}

    IEnumerator floatUp()
	{
        while (floatTime > 0f)
        {
            transform.position += transform.up * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(transform.position - CameraLocation.position);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
	}
}
