using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> footstepSoundClips;
    
    [SerializeField]
    private AudioSource soundSource;
    
    [SerializeField]
    public float pitchVariance = .15f;

    private bool isGrassSoundPlaying = true;


    void PlayFootsteps()
    {

        if (isGrassSoundPlaying == true)
        {
            soundSource.clip = footstepSoundClips[0]; //grass sound
            soundSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
            soundSource.Play();
        }

        if (isGrassSoundPlaying == false)
        {
            soundSource.clip = footstepSoundClips[1]; //dirt sound
            soundSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
            soundSource.Play();
        }

    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Background")
        {
            isGrassSoundPlaying = true;
        }

        if (other.tag == "Dirt")
        {
            isGrassSoundPlaying = false;
        }


    }
}
