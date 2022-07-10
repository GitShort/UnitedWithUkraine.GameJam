using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] string popUpText = "";



	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			TextsBuffer.Instance.MoveText(other.transform.position,popUpText);
			CollectiblesManager.Instance.FoodCollected();
			Destroy(gameObject);
		}
	}
}
