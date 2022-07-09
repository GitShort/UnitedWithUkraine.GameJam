using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool levelPassed = false;

    public GameObject UICanvas;
    public GameObject UICanvasPause;

    public Texture2D mouseCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public bool pauseGame = false;

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
            Cursor.visible = true;
            Cursor.SetCursor(mouseCursor, hotSpot, cursorMode);
        }
	}

	private void Update()
	{
        if(SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
		{
            UICanvasPause.SetActive(true);
            Cursor.visible = true;
            pauseGame = true;
        }
	}

	public void LevelFinished()
	{
        levelPassed = true;
        UICanvas.SetActive(true);
        pauseGame = true;
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
        Application.Quit();
	}

    public void ContinueGame()
	{
        UICanvasPause.SetActive(false);
        Cursor.visible = false;
        pauseGame = false;
    }
}
