using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { Idle, Playing, Ended, Ready };
public class InfinityModeController : MonoBehaviour
{
    [Range(0f, 0.20f)]
    public float parallaxSpeed = 0.02f;
    [Range(0, 100)]
    public int difficulty = 0;
    public int difficultyProgresion = 5;
    public RawImage background;
    public GameState gameState = GameState.Idle;
    public GameObject player;
    public GameObject uiHealth;

    public GameObject follow;
    private Vector2 velocity;

    private int points = 0;
    private bool waitStart;

    // Start is called before the first frame update
    void Start()
    {
        waitStart = false;
        Invoke("WaitStart",0.25f);
        AudioController.sharedInstance.PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = Input.GetKeyDown("up") || Input.GetKeyDown("left") || 
            Input.GetKeyDown("right") || Input.GetKeyDown("space") || Input.GetKeyDown("f")
            || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d")
            || Input.GetKeyDown("w");

        bool musicControl1 = Input.GetKeyDown("1");
        bool musicControl2 = Input.GetKeyDown("2");
        bool musicControl3 = Input.GetKeyDown("3");

        if (gameState == GameState.Idle)
        {
            if(musicControl1)
                AudioController.sharedInstance.SelectMusic2();
            if (musicControl2)
                AudioController.sharedInstance.SelectMusic3();
            if (musicControl3)
                AudioController.sharedInstance.SelectMusic4();
        }

        if (gameState == GameState.Idle && userAction && waitStart)
        {
            gameState = GameState.Playing;
            player.SendMessage("IsGameStatePlaying");
            follow.SendMessage("IsActive");
            HUDController.sharedInstance.uiIdle.SetActive(false);
            HUDController.sharedInstance.uiScore.SetActive(true);
            HUDController.sharedInstance.uiHealth.SetActive(true);

            AudioController.sharedInstance.ChangeToMusic2();

            Vector2 position = new Vector2(14.94f, -0.49f);
            LevelGenerator.sharedInstance.GenerateTerrain(position);

            DifficultyController.sharedInstance.ImplementsMoreDifficulty(difficulty);
            InvokeRepeating("IncreaseDifficulty", 5f, 5f);
        }
        else if (gameState == GameState.Playing)
        {
            Parallax();
        }
        else if (gameState == GameState.Ready)
        {
            if (Input.GetButtonDown("Interact"))
            {
                ResetTimeScale();
                SceneManager.LoadScene("MainLobby");
            }

            else if (userAction)
            {
                RestartGame();
            }
        }
        BackGroundFollow();
    }

    void BackGroundFollow()
    {
        float posX = follow.transform.position.x;
        float posY = follow.transform.position.y;

        transform.position = new Vector3( posX, posY, transform.position.z);
    }

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
    }

    public void PlayerDead()
    {
        gameState = GameState.Ended;
        Invoke("GameReady", 1.5f);
    }

    void GameReady()
    {
        gameState = GameState.Ready;
    }

    public void RestartGame()
    {
        ResetTimeScale();
        SceneManager.LoadScene("InfinityMode");
    }

    public void WaitStart()
    {
        waitStart = true;
    }

    void IncreaseDifficulty()
    {
        difficulty += difficultyProgresion;
        DifficultyController.sharedInstance.ImplementsMoreDifficulty(difficulty);
    }

    public void ResetTimeScale(float newTimeScale = 1f)
    {
        CancelInvoke("IncreaseDifficulty");
        Time.timeScale = newTimeScale;
    }

    public void IncreasePoints(int increasePoint)
    {
        
        points += increasePoint;
        HUDController.sharedInstance.UpdateScore(points);
        if (points > GetMaxScore())
        {
            HUDController.sharedInstance.UpdateMaxScore(points);
            SaveScore(points);
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points", 0);
    }

    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }
}
