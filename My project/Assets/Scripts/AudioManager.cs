using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource music;

    public AudioClip mainMenuMusic;
    public AudioClip gameOverMusic;
    public AudioClip gameWinMusic;


    public AudioClip pressButtonSFX;
    public AudioClip shellBumpSFX;
    public AudioClip blockBreakSFX;
    public AudioClip powerupSFX;
    public AudioClip bulletBillSFX;
    public AudioClip chainChompSFX;
    public AudioClip thwompSFX;
    public AudioClip kingBooSFX;
    public AudioClip booSFX;



    void Start()
    {
        music.clip = mainMenuMusic;
        music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play(); 
    }

    public void PauseMusic()
    {
        music.Pause();
    }

    public void ResumeMusic()
    {
        music.UnPause();
    }

    void Update()
    {
        
    }
}
