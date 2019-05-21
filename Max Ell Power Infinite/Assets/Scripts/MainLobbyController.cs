using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainLobbyController : MonoBehaviour
{
    public GameState gameState = GameState.Idle;
    public GameObject player;
    public GameObject uiIdle;
    public GameObject uiHealth;
    public RawImage background;
    public GameObject follow;
    private Vector2 velocity;


    private bool waitStart;

    // Start is called before the first frame update
    void Start()
    {
        waitStart = false;
        Invoke("WaitStart", 0.25f);
        //AudioController.sharedInstance.PlayMusic();
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
            if (musicControl1)
                AudioController.sharedInstance.SelectMusic2();
            if (musicControl2)
                AudioController.sharedInstance.SelectMusic3();
            if (musicControl3)
                AudioController.sharedInstance.SelectMusic4();
        }

        if (gameState == GameState.Idle && userAction && waitStart)
        {
            gameState = GameState.Playing;
            follow.SendMessage("IsActive");
            uiIdle.SetActive(false);
            //uiHealth.SetActive(true);

            AudioController.sharedInstance.ChangeToMusic2();
        }
    }


    void GameReady()
    {
        gameState = GameState.Ready;
    }

    public void WaitStart()
    {
        waitStart = true;
    }
}
