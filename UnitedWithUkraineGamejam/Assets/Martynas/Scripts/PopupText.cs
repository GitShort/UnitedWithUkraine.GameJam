using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField] GameObject textObject;
    public float floatTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        textObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collected(string text)
	{
        textObject.SetActive(true);
        textObject.GetComponent<TextMeshPro>().text = text;
	}


}
