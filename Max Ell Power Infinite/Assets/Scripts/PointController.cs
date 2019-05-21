using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    private Animator anim;
    private GameObject game;
    bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim != null)
            anim.SetBool("Collected", collected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && collected == false)
        {
            collected = true;
            switch(transform.gameObject.tag)
            {
                case "TerrainPoint":
                    other.SendMessage("IncreasePoints", 10);
                    break;
                case "Fruit":
                    other.SendMessage("IncreasePoints", 15);
                    break;
                case "Coin":
                    other.SendMessage("IncreasePoints", 40);
                    break;
                case "Diamond":
                    other.SendMessage("IncreasePoints", 80);
                    break;
                case "Star":
                    other.SendMessage("IncreasePoints", 120);
                    break;
            }
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
