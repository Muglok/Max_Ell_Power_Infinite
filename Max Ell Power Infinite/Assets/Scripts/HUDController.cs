using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController sharedInstance;

    public GameObject follow;
    public GameObject uiIdle;
    public GameObject uiScore;
    public GameObject uiHealth;
    public Text pointText;
    public Text recordText;

    // Start is called before the first frame update
    void Start()
    {
        recordText.text = "BEST: " + PlayerPrefs.GetInt("Max Points", 0).ToString();
        sharedInstance = this;
    }

    // Update is called once per frame
    void Update()
    {
        HUDFollow();
    }

    void HUDFollow()
    {
        float posX = follow.transform.position.x;
        float posY = follow.transform.position.y;

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

    public void UpdateScore(int points)
    {
        pointText.text = points.ToString();
    }

    public void UpdateMaxScore(int points)
    {
        recordText.text = "BEST: " + points.ToString();
    }

}
