using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float maxSpeed = 1f;
    public float speed = 1f;

    private Rigidbody2D rb2d;
    private Animator anim;

    private bool dying;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        dying = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!dying)
        {
            rb2d.AddForce(Vector2.right * speed);
            float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
            rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

            if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f)
            {
                speed = -speed;
                rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            }

            if (speed < 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (speed > 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (transform.gameObject.tag == "LateralSpiker")
                LateralSpikerCollision(col);
            else if (transform.gameObject.tag == "UpperSpiker")
                UpperSpikerCollision(col);

        }

        if (col.gameObject.tag == "Attack")
        {
            if (transform.gameObject.tag == "LateralSpiker")
            {
                col.GetComponentInParent<PlayerController>().SendMessage("AttackBlockedKnockBack", transform.position.x);
            }
            else if (transform.gameObject.tag == "UpperSpiker")
            {
                dying = true;
                rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                col.GetComponentInParent<PlayerController>().SendMessage("SlashEnemy");
                col.GetComponentInParent<PlayerController>().SendMessage("IncreasePoints", 50);
                anim.SetTrigger("Slashed");
                Invoke("Destroy", 0.25f);
            }
        }
    }

    void LateralSpikerCollision(Collider2D col)
    {
        float yOffset = 0.2f;
        if (transform.position.y + yOffset < col.transform.position.y)
        {
            dying = true;
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            col.SendMessage("EnemyJump");
            col.SendMessage("IncreasePoints", 25);
            anim.SetTrigger("Smashed");
            Invoke("Destroy", 0.25f);
        }
        else
        {
            col.SendMessage("EnemyKnoclBack", transform.position.x);
        }
    }

    void LateralSpikerBlockAttack(Collider2D col)
    {
        col.SendMessage("EnemyKnoclBack", transform.position.x);
    }

    void UpperSpikerCollision(Collider2D col)
    {
        col.SendMessage("EnemyKnoclBack", transform.position.x);
    }

    void SetDaying()
    {
        dying = true;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
