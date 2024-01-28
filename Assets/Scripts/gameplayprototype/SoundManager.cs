using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    
    private void Awake()
    {
        if(!instance) instance = this;
        else if(instance != this) Destroy(gameObject);
        DontDestroyOnLoad(instance);
    }

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public void playBGM(AudioClip bgm)
    {
        bgmSource.clip = bgm;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void stopBGM()
    {
        bgmSource.Stop();
    }

    public void playSFX(AudioClip sfx)
    {
        bgmSource.PlayOneShot(sfx);
    }
}
