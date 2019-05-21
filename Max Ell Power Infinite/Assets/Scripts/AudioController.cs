using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController sharedInstance;

    public GameObject audioPlayer;
    private AudioSource music;

    public GameObject audioPlayer2;
    private AudioSource music2;

    public GameObject audioPlayer3;
    public GameObject audioPlayer4;

    public GameObject jump;
    private AudioSource jumpClip;

    public GameObject point;
    private AudioSource pointClip;

    public GameObject attack;
    private AudioSource attackClip;

    public GameObject realoadAttack;
    private AudioSource reloadAttackClip;

    public GameObject damage;
    private AudioSource damageClip;

    public GameObject smashEnemy;
    private AudioSource smashEnemyClip;

    public GameObject slashEnemy;
    private AudioSource slashEnemyClip;

    public GameObject attackBlocked;
    private AudioSource attackBlockedClip;

    public GameObject emote;
    private AudioSource emoteClip;

    public GameObject die;
    private AudioSource dieClip;

    // Start is called before the first frame update
    void Start()
    {
        sharedInstance = this;

        music = audioPlayer.GetComponent<AudioSource>();
        music2 = audioPlayer2.GetComponent<AudioSource>();

        jumpClip = jump.GetComponent<AudioSource>();
        pointClip = point.GetComponent<AudioSource>();
        attackClip = attack.GetComponent<AudioSource>();
        reloadAttackClip = realoadAttack.GetComponent<AudioSource>();
        damageClip = damage.GetComponent<AudioSource>();
        smashEnemyClip = smashEnemy.GetComponent<AudioSource>();
        slashEnemyClip = slashEnemy.GetComponent<AudioSource>();
        attackBlockedClip = attackBlocked.GetComponent<AudioSource>();
        emoteClip = emote.GetComponent<AudioSource>();
        dieClip = die.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void PlayMusic()
    {
        music.Play();
    }
    public void ChangeToMusic2()
    {
        music.Stop();
        music2.Play();
    }

    public void SelectMusic2()
    {
        PlayReloadAttackClip();
        music2 = audioPlayer2.GetComponent<AudioSource>();
    }

    public void SelectMusic3()
    {
        PlayReloadAttackClip();
        music2 = audioPlayer3.GetComponent<AudioSource>();
    }

    public void SelectMusic4()
    {
        PlayReloadAttackClip();
        music2 = audioPlayer4.GetComponent<AudioSource>();
    }

    public void PlayJumpClip()
    {
        jumpClip.Play();
    }

    public void PlayPointClip()
    {
        pointClip.Play();
    }

    public void PlayAttackClip()
    {
        attackClip.Play();
    }

    public void PlayReloadAttackClip()
    {
        reloadAttackClip.Play();
    }

    public void PlayDamageClip()
    {
        damageClip.Play();
    }

    public void PlaySmashEnemyClip()
    {
        smashEnemyClip.Play();
    }

    public void PlaySlashEnemyClip()
    {
        slashEnemyClip.Play();
    }

    public void PlayAttackBlockedClip()
    {
        attackBlockedClip.Play();
    }

    public void PlayEmoteClip()
    {
        emoteClip.Play();
    }

    public void PlayDieClip()
    {
        dieClip.Play();
    }

    public void MusicStop()
    {
        music2.Stop();
    }

    public void FadeOut()
    {
        StartCoroutine(AudioFadeScript.FadeOut(music2, 0.75f));
    }

    public void FadeIn()
    {
        StartCoroutine(AudioFadeScript.FadeIn(music2, 2.5f));
    }
}
