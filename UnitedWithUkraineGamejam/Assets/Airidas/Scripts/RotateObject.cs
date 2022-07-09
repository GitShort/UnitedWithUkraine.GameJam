using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 20f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    private void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        this.gameObject.transform.Rotate(0f, Time.deltaTime * rotationSpeed, 0f);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * 0.5f) * 0.25f;

        transform.position = tempPos;
    }
}
