using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField] GameObject textObject;
    private float floatTime = 3f;
    [SerializeField] float startingTime = 3f;
    private Vector3 CameraLocation;
    // Start is called before the first frame update
    void Start()
    {
        CameraLocation = Camera.main.transform.position;
        floatTime = startingTime;
        textObject.SetActive(false);
    }

	private void FixedUpdate()
	{
        if (textObject.activeSelf)
        {
            floatTime -= Time.deltaTime;
        }
    }

	public void Collected(string text)
	{
        textObject.SetActive(true);
        floatTime = startingTime;
        textObject.GetComponent<TextMeshPro>().text = text;
        StartCoroutine(floatUp());
	}

    IEnumerator floatUp()
	{
        while (floatTime > 0f)
        {
            textObject.transform.position += transform.up * Time.deltaTime;
            textObject.transform.LookAt(CameraLocation);
            yield return null;
        }
        textObject.SetActive(false);
        yield return null;
	}
}
