using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource trainerTheme;
    public AudioSource wildTheme;

    private void Start()
    {
        //wildTheme = this.gameObject.transform.GetChild(0).GetComponent<AudioSource>();
        //trainerTheme = this.gameObject.transform.GetChild(1).GetComponent<AudioSource>();
    }
    public void PlayTrainerTheme()
    {
        trainerTheme.Play();
    }

    public void PlayWildTheme()
    {
        wildTheme.Play();
    }
}
