using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupText : MonoBehaviour
{
    [SerializeField] GameObject textObject; 
    // Start is called before the first frame update
    void Start()
    {
        //textObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collected(string text)
	{
        textObject.SetActive(true);
	}
}
