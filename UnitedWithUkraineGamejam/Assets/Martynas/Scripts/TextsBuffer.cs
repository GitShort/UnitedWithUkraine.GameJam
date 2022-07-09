using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextsBuffer : MonoBehaviour
{
    [SerializeField] List<GameObject> textObjects;
    [SerializeField] GameObject textPrefab;
    private int count = 0;

	public static TextsBuffer Instance = null;

	// Initialize the singleton instance.
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

	private void Start()
	{
        CreateTexts();
    }

	private void CreateTexts()
	{
        int instantiates = CollectiblesManager.Instance.getTextCount();
        for(int i = 0; i < instantiates; i++)
		{
            textObjects.Add(Instantiate(textPrefab, transform.position, Quaternion.identity));

        }
	}

	public void MoveText(Vector3 position, string text)
	{
        textObjects[count].SetActive(true);
        textObjects[count].transform.position = position;
        textObjects[count].GetComponent<PopupText>().Collected(text);
        count++;
	}
}
