using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    string popUpText = "";

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			Destroy(gameObject);
		}
	}
}
