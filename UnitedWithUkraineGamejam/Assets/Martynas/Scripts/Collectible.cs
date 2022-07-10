using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] string popUpText = "";
	[SerializeField] GameObject eatParticles;

    private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerController>() != null)
		{
			TextsBuffer.Instance.MoveText(other.transform.position,popUpText);
			CollectiblesManager.Instance.FoodCollected();

			var go = Instantiate(eatParticles, transform.position, Quaternion.identity);
			if (!go.GetComponent<ParticleSystem>().isPlaying)
				go.GetComponent<ParticleSystem>().Play();
			Destroy(go, 3f);

			Destroy(gameObject);
		}
	}
}
