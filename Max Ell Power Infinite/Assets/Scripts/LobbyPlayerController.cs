using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerController : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float speed = 2f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public GameObject game;
    public ParticleSystem dust;
    public Animator animHealth;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;
    private bool jump = false;
    private bool doubleJump = true;
    private bool movement = true;
    private bool emote = false;
    private bool attack = true;
    private bool gamePlaying = false;
    private int health = 3;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Emote", emote);

        animHealth.SetInteger("Health", health);
        anim.SetBool("Dead", dead);

        if (gamePlaying && !dead)
        {
            if (grounded)
            {
                doubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                emote = false;
                if (grounded)
                {
                    jump = true;
                    doubleJump = true;
                    AudioController.sharedInstance.PlayJumpClip();
                }
                else if (doubleJump)
                {
                    jump = true;
                    doubleJump = false;
                    AudioController.sharedInstance.PlayJumpClip();
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                emote = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && grounded && attack)
            {
                emote = false;
                anim.SetTrigger("Attacking");
                attack = false;
                Invoke("RestartAttack", 0.7f);

            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;
        float h = 0;
        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }

        if (gamePlaying) h = Input.GetAxis("Horizontal");
        if (h != 0) emote = false;
        if (!movement) h = 0;

        rb2d.AddForce(Vector2.right * speed * h);

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);


        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (grounded && (h > 0.1f || h < -0.1f))
        {
            DustPlay();
        }
        else
        {
            DustPause();
        }

        if (jump)
        {
            Jump(jumpPower);
        }
    }

    /*private void OnBecameInvisible()
    {
         transform.position = new Vector3(-0.2701356f, -0.975936f, 0);
    }*/

    public void EnemyJump()
    {
        AudioController.sharedInstance.PlaySmashEnemyClip();
        jump = true;
    }

    public void EnemyKnoclBack(float enemyPosX)
    {
        health--;
        AudioController.sharedInstance.PlayDamageClip();
        if (health > 0)
        {
            jump = true;

            float side = Mathf.Sign(enemyPosX - transform.position.x);

            rb2d.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);

            movement = false;
            Invoke("EnableMovement", 0.7f);

            Color color = new Color(255 / 255f, 106 / 255f, 0 / 255f);
            spr.color = color;
        }

        if (health == 0)
        {
            Death();
        }
    }

    public void AttackBlockedKnockBack(float enemyPosX)
    {
        AudioController.sharedInstance.PlayAttackBlockedClip();

        float side = Mathf.Sign(enemyPosX - transform.position.x);

        rb2d.AddForce(Vector2.left * side * 15f, ForceMode2D.Impulse);

        movement = false;
        Invoke("EnableMovement", 0.2f);

        Color color = new Color(188 / 255f, 188 / 255f, 1888 / 255f);
        spr.color = color;
    }

    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }

    void Jump(float jumpPower)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        jump = false;
    }

    public void PlayPointSound()
    {
        AudioController.sharedInstance.PlayPointClip();

    }

    public void IncreasePoints(int points)
    {
        bool objectPoint = /*points == 10 ||*/ points == 15 || points == 40 || points == 80 || points == 120;

        if (objectPoint) PlayPointSound();

        game.SendMessage("IncreasePoints", points);
    }

    public void RestartAttack()
    {
        if (!attack)
        {
            AudioController.sharedInstance.PlayReloadAttackClip();
            attack = true;
        }
    }

    public void IsGameStatePlaying()
    {
        gamePlaying = true;
    }

    void SlashEnemy()
    {
        Invoke("RestartAttack", 0.2f);
        AudioController.sharedInstance.PlaySlashEnemyClip();
    }

    public void AttackSound()
    {
        AudioController.sharedInstance.PlayAttackClip();
    }

    public void EmoteSound()
    {
        if (emote == true)
        {
            AudioController.sharedInstance.PlayEmoteClip();
        }
    }

    public void DisableEmote()
    {
        emote = false;
    }

    void DustPlay()
    {
        dust.Play();
    }

    void DustPause()
    {
        dust.Pause();
        dust.Clear();
    }

    void Death()
    {
        health = 0;
        dead = true;
        jump = true;
        rb2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        AudioController.sharedInstance.MusicStop();
        AudioController.sharedInstance.PlayDieClip();

        game.SendMessage("PlayerDead");
    }
}
