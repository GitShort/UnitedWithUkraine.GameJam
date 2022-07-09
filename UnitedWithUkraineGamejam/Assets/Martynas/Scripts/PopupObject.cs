using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupObject : MonoBehaviour
{
    public float DestroyTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject);
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
