using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAnimationController : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> attackSoundClips;

    [SerializeField]
    private AudioSource soundSource;

    public GameObject leafBlastAnim;
    public GameObject slapAnim;
    public GameObject scratchAnim;
    public GameObject convalesceAnim;
    public GameObject bubbleBlastAnim;
    public GameObject flamethrowerAnim;

    // Start is called before the first frame update
    void Start()
    {
        leafBlastAnim.SetActive(false);
        slapAnim.SetActive(false);
        scratchAnim.SetActive(false);
        convalesceAnim.SetActive(false);
        bubbleBlastAnim.SetActive(false);
        flamethrowerAnim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnLeafBlastAttackAnim()
    {
        StartCoroutine(LeafBlastAnim());
    }

    public void OnSlapButtonAttackAnim()
    {
        StartCoroutine(SlapAnim());
    }

    public void OnScratchAttackAnim()
    {
        StartCoroutine(ScratchAnim());
    }

    public void OnConvalesceAttackAnim()
    {
        StartCoroutine(ConvalesceAnim());
    }

    public void OnBubbleBlastAttackAnim()
    {
        StartCoroutine(BubbleBlastAnim());
    }

    public void OnFlamethrowerAttackAnim()
    {
        StartCoroutine(FlamethrowerAnim());
    }

    public void OnAttackAnim(Ability move, HealthBarScript healthBarScript)
    {
        StartCoroutine(SpawnAndPlayAnim(move, healthBarScript));
    }




    IEnumerator LeafBlastAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        leafBlastAnim.SetActive(true); //Play animation
        soundSource.clip = attackSoundClips[1];
        soundSource.Play();
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        leafBlastAnim.SetActive(false); // deactivate animation
    }

    IEnumerator SlapAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        slapAnim.SetActive(true); //Play animation
        soundSource.clip = attackSoundClips[0];
        soundSource.Play();
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        slapAnim.SetActive(false); // deactivate animation
    }

    IEnumerator ScratchAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        scratchAnim.SetActive(true); //Play animation
        soundSource.clip = attackSoundClips[2];
        soundSource.Play();
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        scratchAnim.SetActive(false); // deactivate animation
    }

    IEnumerator ConvalesceAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        convalesceAnim.SetActive(true); //Play animation
        soundSource.clip = attackSoundClips[3];
        soundSource.Play();
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        convalesceAnim.SetActive(false); // deactivate animation
    }

    IEnumerator BubbleBlastAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        bubbleBlastAnim.SetActive(true); //Play animation
        soundSource.clip = attackSoundClips[4];
        soundSource.Play();
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        bubbleBlastAnim.SetActive(false); // deactivate animation
    }

    IEnumerator FlamethrowerAnim()
    {
        yield return new WaitForSeconds(1.0f); //for text to print
        flamethrowerAnim.SetActive(true); //Play animation
        soundSource.clip = attackSoundClips[5];
        soundSource.Play();
        yield return new WaitForSeconds(4.0f); //wait for animation to finish
        flamethrowerAnim.SetActive(false); // deactivate animation
    }

    IEnumerator SpawnAndPlayAnim(Ability move, HealthBarScript healthBarScript)
    {

       

        
        yield return new WaitForSeconds(0.5f); //for text to print
        GameObject animation = Instantiate(move.animation, move.animationTransform, Quaternion.Euler(move.animationRotation));
        soundSource.clip = attackSoundClips[move.index];
        soundSource.Play();
        float delay = animation.GetComponent<ParticleSystem>().main.duration;
        yield return new WaitForSeconds(delay); //wait for animation to finish
        Destroy(animation.gameObject);
        healthBarScript.UpdateHealthBar();
    }


}
