using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool levelPassed = false;

    public GameObject UICanvas;

    public Texture2D mouseCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Cursor.visible = false;
            UICanvas.SetActive(false);
        }
        else
		{
            Cursor.SetCursor(mouseCursor, hotSpot, cursorMode);
        }
	}

	private void Update()
	{

	}

	public void LevelFinished()
	{
        levelPassed = true;
        UICanvas.SetActive(true);

    }

    public bool getLevelStatus()
	{
        return levelPassed;
	}

    public void LoadNextLevel()
	{
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

    public void LoadMenuScene()
	{
        SceneManager.LoadSceneAsync(0);
	}

    public void ExitGame()
	{
        Debug.Log("quiting");
        Application.Quit();
	}
}
