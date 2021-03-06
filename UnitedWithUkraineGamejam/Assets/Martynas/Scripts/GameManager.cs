using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool levelPassed = false;

    public GameObject UICanvas;
    public GameObject UICanvasPause;
    public GameObject FinishCanvas;
    public TextMeshProUGUI jumpText;

    public Texture2D mouseCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public bool pauseGame = false;

    public PlayerController player;

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
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            ContinueGame();
        }
    }

	private void Update()
	{
        if(SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape) && !levelPassed)
		{
            UICanvasPause.SetActive(true);
            Cursor.visible = true;
            pauseGame = true;
            Time.timeScale = 0;
        }
	}

	public void LevelFinished()
	{
        levelPassed = true;
        if (SceneManager.sceneCountInBuildSettings - 1 > SceneManager.GetActiveScene().buildIndex)
        {
            UICanvas.SetActive(true);
        }
        else
		{
            FinishCanvas.SetActive(true);
        }
        pauseGame = true;
        Cursor.visible = true;
        jumpText.gameObject.SetActive(true);
        jumpText.text = "Jump count: "+ player.getJumps().ToString();
    }

    public bool getLevelStatus()
	{
        return levelPassed;
	}

    public void LoadNextLevel()
	{
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMenuScene()
	{
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
	}

    public void ExitGame()
	{
        Application.Quit();
	}

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueGame()
	{
        UICanvasPause.SetActive(false);
        Cursor.visible = false;
        pauseGame = false;
        Time.timeScale = 1;
    }
}
