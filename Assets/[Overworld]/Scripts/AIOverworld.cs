using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOverworld : MonoBehaviour
{
    public int DialougeIndex = 0;
    public string[] Dialouge = 
        { "The dialouge for this character has not been set.", 
        "This is a test dialouge p1" };

    public string name = "Placeholder";

    public Canvas canvas;
    public GameObject spacebarIcon;
    public Animator animator;

    [SerializeField]
    TMPro.TextMeshProUGUI messagePrefab;
    public GameObject borderPrefab;

    [SerializeField]
    float timeBetweenCharacters = 0.0001f;

    private IEnumerator animateTextCoroutineRef = null;

    bool canInstantiate = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator AnimateTextCoroutine(string message)
    {
        messagePrefab.text = "";
        for (int currentCharater = 0; currentCharater < message.Length; currentCharater++)
        {
            messagePrefab.text += message[currentCharater];
            yield return new WaitForSecondsRealtime(timeBetweenCharacters);
        }
        animateTextCoroutineRef = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Noah")
        {
            collision.gameObject.GetComponent<PlayerInputHandler>().NPC = this.gameObject;
            collision.gameObject.GetComponent<PlayerInputHandler>().inRangeOfNPC = true;

            spacebarIcon.SetActive(true);
            animator.SetBool("isPlayerInRange", true);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Noah")
        {


            collision.gameObject.GetComponent<PlayerInputHandler>().NPC = null;
            collision.gameObject.GetComponent<PlayerInputHandler>().inRangeOfNPC = false;

            spacebarIcon.SetActive(false);
            animator.SetBool("isPlayerInRange", false);
            cleanMessage();


        }
    }

    public void SayMessage()
    {

            borderPrefab.SetActive(true);
            messagePrefab.gameObject.SetActive(true);
            animateTextCoroutineRef = AnimateTextCoroutine(Dialouge[DialougeIndex]);
            StartCoroutine(animateTextCoroutineRef);


    }

    public void cleanMessage()
    {
        borderPrefab.SetActive(false);
        messagePrefab.gameObject.SetActive(false);
    }
}
