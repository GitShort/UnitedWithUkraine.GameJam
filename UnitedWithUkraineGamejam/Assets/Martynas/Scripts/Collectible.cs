using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    string popUpText = "";

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PopupText>() != null)
		{
			other.GetComponent<PopupText>().Collected("text");
			Debug.Log(gameObject);
			Destroy(gameObject, 1f);
		}
	}
}
