using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMusicManager : MonoBehaviour
{

    public AudioSource[] audios;
    public float musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        playMusic(audios[0]);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void playMusic(AudioSource musicToPlay)
    {
        
        musicToPlay.volume = 0;

        StartCoroutine(MusicFadeIn(musicToPlay));
        
    }

    void stopMusic(AudioSource musicToStop)
    {
        StartCoroutine(MusicFadeOut(musicToStop));
        
        
    }


    IEnumerator MusicFadeIn(AudioSource musicToPlay)
    {
        musicToPlay.Play();

        while (musicToPlay.volume < musicVolume)
        {
            musicToPlay.volume += 0.02f;
            yield return new WaitForSeconds(0.3f);
        }
        
    }

    IEnumerator MusicFadeOut(AudioSource musicToPlay)
    {
        while (musicToPlay.volume > 0)
        {
            musicToPlay.volume -= 0.0125f;
            yield return new WaitForSeconds(0.3f);
            
        }

        if(musicToPlay.volume <= 0)
        {
            musicToPlay.Stop();
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Noah")
        {
            foreach(AudioSource audioSource in audios)
            {
                if(audioSource.isPlaying == true && audioSource.name == "OverworldTheme")
                {
                    stopMusic(audioSource);
                }
            }

            playMusic(audios[1]);
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Noah" && canPlay == true && musicStopped == true)
    //    {
    //        playMusic(audios[1]);
    //        canPlay = false;
    //    }

    //}

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Noah")
        {
            foreach (AudioSource audioSource in audios)
            {
                if (audioSource.isPlaying == true && audioSource.name == "RivalTheme")
                {
                    stopMusic(audioSource);
                }
            }

            playMusic(audios[0]);
        }
    }

}
