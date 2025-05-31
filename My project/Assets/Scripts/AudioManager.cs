using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfx;
    [SerializeField] AudioSource music;

    public AudioClip mainMenuMusic;

    
    public AudioClip pressButtonSFX;


    void Start()
    {
        music.clip = mainMenuMusic;
        music.loop = true;
        music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    void Update()
    {
        
    }
}
