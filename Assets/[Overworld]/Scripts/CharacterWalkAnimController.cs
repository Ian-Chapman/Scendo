using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CardinalDirection
{
    North   = 0,
    East    = 1,
    South   = 2,
    West    = 3
}

public class CharacterWalkAnimController : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> encounterSoundClips;

    OverworldAIEncounterBehaviour overworldAIEncounterBehaviour;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private CardinalDirection facing = CardinalDirection.South;

    [SerializeField]
    private Rigidbody2D rigidbody;

    private bool isMoving = false;

    public Image blackFade;

    public AudioSource preEncounterSounds;

    PlayerMovement playerMovement;



    // Start is called before the first frame update
    void Start()
    {
        //For fading from black on stage opening
        blackFade.canvasRenderer.SetAlpha(1.0f);

        // For Fading to black
        blackFade.canvasRenderer.SetAlpha(0.0f);

        playerMovement = GetComponent<PlayerMovement>();
        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //If the character is mostly walking up, set direction to up
        //if not walking, set is walking to false
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        Vector2 velocity = new Vector2(inputX, inputY);

        bool isWalking = velocity.sqrMagnitude > float.Epsilon;
        bool isMovingHorizontally = Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y);

        if (isMovingHorizontally)
        {
            if (velocity.x < 0)
            {
                facing = CardinalDirection.West;
            }
            else
            {
                facing = CardinalDirection.East;
            }

            isMoving = true;
        }
        else
        {
            if (velocity.y < 0)
            {
                facing = CardinalDirection.South;
            }
            else
            {
                facing = CardinalDirection.North;
            }
            isMoving = true;
        }

        if ((velocity.y == 0) && (velocity.x == 0))
        {
            isMoving = false;
        }

        animator.SetBool("isWalking", isWalking);
        animator.SetInteger("walkDirection", (int)facing); //walk up
    }


    //Insperation for finding random encounters in the overworld
    //https://www.youtube.com/watch?v=fePYYZaesSM&t=25s

    private void OnTriggerStay2D(Collider2D other)
    {
        //Player cannot encounter something unless that are moving
        if ((other.tag == "Tall Grass") && (isMoving == true))
        {
            //A 1/100 chance to encounter something if the player stays in the Tall Grass
            //for any amount of time.
            if (Random.Range(1, 101) <= 1)
            {
                //tapeSounds.clip = tapeSoundClips[1];
                //tapeSounds.Play();

                Debug.Log("A wild scendo appeared");

                PlayerPrefs.SetInt("EncounterCheck", 0); // 0 = wild encounter, 1 = trainer encounter

                if (Random.Range(1, 11) <= 4)
                {
                    PlayerPrefs.SetString("RandomEncounter", "Parchpaw");
                }
                else if (Random.Range(1, 11) >= 5)
                {
                    PlayerPrefs.SetString("RandomEncounter", "Dampurr");
                }
                
                Debug.Log(PlayerPrefs.GetString("RandomEncounter"));
                StartCoroutine(DelayAndFadeToBlack());
            }
        }
    }


    public void fadeToBlack()
    {
        //This is changing the FadeIn/Out image to 1 (0 = invisible / 1 = visible)
        // 2nd argument is the amount of time the fade takes to complete
        blackFade.CrossFadeAlpha(1, 2.0f, false);
        Debug.Log("Fading to encounter...");

    }

    public IEnumerator DelayAndFadeToBlack()
    {
        //wait 2 seconds for tape sound

        //Disable movement and animation

        
        preEncounterSounds.clip = encounterSoundClips[0];
        preEncounterSounds.Play();

        playerMovement.canMove = false;
        rigidbody.velocity = new Vector2(0, 0);
        
        fadeToBlack();
        yield return new WaitForSeconds(2.75f);

        PlayerPrefs.SetFloat("tempXPos", this.gameObject.transform.position.x);
        PlayerPrefs.SetFloat("tempYPos", this.gameObject.transform.position.y);
        StartCoroutine(DelaySceneLoading());
    }

    public IEnumerator DelaySceneLoading()
    {
        yield return new WaitForSeconds(.01f);
        playerMovement.canMove = true;
        SceneManager.LoadScene("EncounterScene");
    }
}
