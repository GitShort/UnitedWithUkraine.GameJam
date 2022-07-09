using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] string popUpText = "";

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PopupText>() != null)
		{
			other.GetComponent<PopupText>().Collected(popUpText);
			Destroy(gameObject);
		}
	}
}
