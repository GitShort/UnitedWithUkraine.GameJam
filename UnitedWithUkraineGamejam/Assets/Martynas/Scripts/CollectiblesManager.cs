using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManager : MonoBehaviour
{
    public static CollectiblesManager Instance { get; private set; }

    [SerializeField] List<GameObject> CollectibleGO;
    private int count = 0;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void FoodCollected()
	{
        count++;
        if (CollectibleGO.Count == count)
		{
            GameManager.Instance.LevelFinished();
		}
	}
}
