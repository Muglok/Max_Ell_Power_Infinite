using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallController : MonoBehaviour
{
    public static FireWallController sharedInstance;

    public GameObject player;
    public GameObject game;
    public float velocity = 2f;
    [Range(-16f, -5f)]
    public float securityZone = -16f;
    [Range(20f, 11.6f)]
    public float limitSeparation = 20;
    private Rigidbody2D rb2d;
    
    private float smoothTime = 0;

    private Vector2 velocityVector;

    // Start is called before the first frame update
    void Start()
    {
        sharedInstance = this;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        bool gamePlaying = game.GetComponent<InfinityModeController>().gameState == GameState.Playing;
        if (gamePlaying)
        {
            rb2d.velocity = Vector2.right * velocity;
            if (player.transform.position.x - transform.position.x > limitSeparation)
            {
                float posX = Mathf.SmoothDamp(transform.position.x,
                player.transform.position.x + securityZone, ref velocityVector.x, smoothTime);

                float fixedSpeed = (velocity + 15) * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(posX, transform.position.y, transform.position.z), fixedSpeed);
            }

            else
            {
                rb2d.velocity = Vector2.right * velocity;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("Death");
        }

        if (col.gameObject.tag == "LateralSpiker" || col.gameObject.tag == "UpperSpiker")
        {
            col.SendMessage("Destroy");
        }
    }
}
