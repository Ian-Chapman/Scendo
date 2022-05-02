using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Will prompt the user what to do, and enable/disable UI elements at different times
public class EncounterUI : MonoBehaviour
{
    HealthBarScript healthBarScript;
    SaveLoadSystem saveLoadSystem;

    [SerializeField]
    EncounterInstance encounter;

    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject abilityPanel;

    public GameObject player;
    public GameObject enemy;

    GameObject AIHealthBar;

    public Image blackFade;

    //These are buttons
    GameObject move1, move2, move3, move4;
    GameObject fightButton;
    GameObject runButton;

    public MonsterDatabase Mourntooth;

    [SerializeField]
    float timeBetweenCharacters = 0.1f;

    public BattleManager battleManager;
    public MasterScendoDatabase scendoDatabase;
    public AttackDatabase attackDatabase;
    GameObject PlayerHealthBar;

    public int chosenMove;
    public string chosenMoveName;

    AttackAnimationController attackAnimController;

    Vector2 moveLocation;

    Ability[] mourntoothAttacks;

    //This is a reference to keep track of our coroutine
    private IEnumerator animateTextCoroutineRef = null;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBarScript = GetComponent<HealthBarScript>();

        StartCoroutine(DelayAndFadeFromBlack());

        //For fading from black on stage opening
        blackFade.canvasRenderer.SetAlpha(1.0f);

        // For Fading to black
        blackFade.canvasRenderer.SetAlpha(0.0f);

        animateTextCoroutineRef = AnimateTextCoroutine("You have encountered an opponent!");
        //Animate some text to say what you encountered
        StartCoroutine(animateTextCoroutineRef);


        //StopCoroutine((animateTextCoroutineRef));
        //encounter.onCharacterTurnBegin.AddListener(AnnounceCharacterTurnBegin);

        //OnCharaterTurnBegin, announce whose turn it is now
        encounter.onPlayerTurnBegin.AddListener(EnablePlayerUI);

        //On player turn end, disable UI
        encounter.onPlayerTurnEnd.AddListener(DisablePlayerUI);

        
        encounter.onEnemyTurnBegin.AddListener(DisablePlayerUI);

        
        encounter.onEnemyTurnEnd.AddListener(EnablePlayerUI);

        GameObject[] allButtons = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allButtons)
        {
            if (go.name == "FightButton")
            {
                fightButton = go;
            }
            else if(go.name == "RunButton")
            {
                runButton = go;
            }
        }

        fightButton.GetComponent<Button>().onClick.AddListener(OnFightButtonPressed);
        runButton.GetComponent<Button>().onClick.AddListener(OnRunButtonPressed);

        abilityPanel.SetActive(false);
        PlayerHealthBar = GameObject.Find("PlayerHealthBar");
        AIHealthBar = GameObject.Find("EnemyHealthBar");
        //player.GetComponent<EncounterPlayerCharacter>().Mourntooth.CurrentHp = player.GetComponent<EncounterPlayerCharacter>().Mourntooth.TotalHp;
        PlayerHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
       
        attackAnimController = GameObject.Find("AttackAnimationController").GetComponent<AttackAnimationController>();

       


    }

    public void loadScendo()
    {
        Mourntooth = new MonsterDatabase();

        if (PlayerPrefs.GetInt("NewMoveLearned") == 0)
        {
            mourntoothAttacks = new Ability[] { attackDatabase.Slap, attackDatabase.LeafBlast, attackDatabase.Convalesce };
        }
        else
        {
            mourntoothAttacks = new Ability[] { attackDatabase.Slap, attackDatabase.LeafBlast, attackDatabase.Convalesce, attackDatabase.Slash };
        }
            

        if (PlayerPrefs.HasKey("CurrentHP"))
        {
            Mourntooth = battleManager.createScendo(Mourntooth, scendoDatabase.Mourntooth.MonsterName, 7, MonsterType.GRASS, 27, PlayerPrefs.GetInt("CurrentHP"), 23, 15, 45, mourntoothAttacks, scendoDatabase.Mourntooth.FrontSprite, scendoDatabase.Mourntooth.BackSprite);
        }
        else
        {
            Mourntooth = battleManager.createScendo(Mourntooth, scendoDatabase.Mourntooth.MonsterName, 7, MonsterType.GRASS, 27, 27, 23, 15, 45, mourntoothAttacks, scendoDatabase.Mourntooth.FrontSprite, scendoDatabase.Mourntooth.BackSprite);

        }

        player.GetComponent<SpriteRenderer>().sprite = Mourntooth.BackSprite;

    }

    void EnablePlayerUI(ICharacter characterTurn)
    {
        mainPanel.SetActive(true);
    }

    void DisablePlayerUI(ICharacter characterTurn)
    {
        mainPanel.SetActive(false);
        abilityPanel.SetActive(false);
    }




    ////---------------------------------------------------------------------- Panel Control via MAIN BUTTONS --------------------------------------------------
    public void OnFightButtonPressed()
    {
        StopCoroutine((animateTextCoroutineRef));
        animateTextCoroutineRef = AnimateTextCoroutine("What will " + Mourntooth.MonsterName + " do?");
        StartCoroutine(animateTextCoroutineRef);
        abilityPanel.SetActive(true);
        mainPanel.SetActive(false);

        GameObject[] allButtons = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach (GameObject go in allButtons)
        {
            if (go.name == "Move1")
            {
                move1 = go;
                move1.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Mourntooth.MonsterAbilities[0].name;
            }

            else if (go.name == "Move2")
            {
                move2 = go;
                move2.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Mourntooth.MonsterAbilities[1].name;
            }

            else if(go.name == "Move3")
            {
                move3 = go;
                move3.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Mourntooth.MonsterAbilities[2].name;

            }

            else if(go.name == "Move4")
            {
                move4 = go;
                if (PlayerPrefs.GetInt("NewMoveLearned") == 0)
                {
                    move4.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "";
                }
                else
                {
                    move4.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = Mourntooth.MonsterAbilities[3].name;
                }
            }
        }

        move1.GetComponent<Button>().onClick.RemoveAllListeners();
        move1.GetComponent<Button>().onClick.AddListener(delegate{OnAttackButtonPressed(0,move1.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text);});

        move2.GetComponent<Button>().onClick.RemoveAllListeners();
        move2.GetComponent<Button>().onClick.AddListener(delegate{OnAttackButtonPressed(1,move2.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text);});

        move3.GetComponent<Button>().onClick.RemoveAllListeners();
        move3.GetComponent<Button>().onClick.AddListener(delegate { OnAttackButtonPressed(2, move3.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text); });

        if (PlayerPrefs.GetInt("NewMoveLearned") == 1)
        {
            move4.GetComponent<Button>().onClick.RemoveAllListeners();
            move4.GetComponent<Button>().onClick.AddListener(delegate { OnAttackButtonPressed(3, move4.GetComponent<Button>().GetComponentInChildren<TMPro.TextMeshProUGUI>().text); });
        }


            fightButton.GetComponent<Button>().onClick.RemoveAllListeners();

    }

    //----------------------------------------------------------------------- Panel Control via ABILITY BUTTONS --------------------------------------------------

    public void OnAttackButtonPressed(int move, string moveName)
    {
        chosenMove = move;
        chosenMoveName = moveName;
        battleManager.TurnStart(move, moveName);
        Debug.Log("button pressed");
    }

    public void OnRunButtonPressed()
    {
        StartCoroutine(Run());
    }

    public IEnumerator DoAttack(int move, string moveName)
    {
        StopCoroutine(animateTextCoroutineRef);
        animateTextCoroutineRef = AnimateTextCoroutine(Mourntooth.MonsterName + " used " + moveName + "!");
        //enemy.GetComponent<AICharacter>().TakeDamage(player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[move], player.GetComponent<EncounterPlayerCharacter>().Mourntooth.MonsterAbilities[move].damage);




        if (Mourntooth.MonsterAbilities[move].moveEffect == MoveEffect.DAMAGE)
        {
            int damageToDeal = battleManager.TakeDamage(Mourntooth.MonsterAbilities[move], Mourntooth, enemy.GetComponent<AICharacter>().ScendoMonster);
            enemy.GetComponent<AICharacter>().TakeDamage(damageToDeal);
        }
        else if (Mourntooth.MonsterAbilities[move].moveEffect == MoveEffect.HEALING)
        {
            int hpToHeal = battleManager.HealHealth(Mourntooth.MonsterAbilities[move], Mourntooth);
            HealScendo(hpToHeal);
        }

        abilityPanel.SetActive(false);
        mainPanel.SetActive(false);
        StartCoroutine(animateTextCoroutineRef);

        if(Mourntooth.MonsterAbilities[move].moveEffect == MoveEffect.DAMAGE)
        {
            attackAnimController.OnAttackAnim(Mourntooth.MonsterAbilities[move], AIHealthBar.GetComponent<HealthBarScript>());
        }
        else if(Mourntooth.MonsterAbilities[move].moveEffect == MoveEffect.HEALING)
        {
            attackAnimController.OnAttackAnim(Mourntooth.MonsterAbilities[move], PlayerHealthBar.GetComponent<HealthBarScript>());
        }

        

        if (battleManager.playerFaster == true)
        {
            yield return new WaitForSeconds(4.0f);
            //start opponents turn
            StartCoroutine(enemy.GetComponent<AICharacter>().DelayDecisionBetter());
        }

        else
        {
            yield return new WaitForSeconds(4.0f);
            mainPanel.SetActive(true);
            //reset panel
            ResetTurn();
        }
    }

    public void ResetTurn()
    {
        fightButton.GetComponent<Button>().onClick.RemoveListener(OnFightButtonPressed);
        fightButton.GetComponent<Button>().onClick.AddListener(OnFightButtonPressed);
        Debug.Log("----------TURN RESET----------");

        animateTextCoroutineRef = AnimateTextCoroutine("What will " + Mourntooth.MonsterName + " do?");
        StartCoroutine(animateTextCoroutineRef);
    }

    public void TakeDamage(int damageRecieved)
    {
        Mourntooth.CurrentHp -= damageRecieved;
        //PlayerHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
    }

    public void HealScendo(int healAmount)
    {
        if(Mourntooth.CurrentHp + healAmount > Mourntooth.TotalHp)
        {
            Mourntooth.CurrentHp = Mourntooth.TotalHp;
        }
        else
        {
            Mourntooth.CurrentHp += healAmount;
        }
    }


    public void OnPlayerWin()
    {
        //if (Monster.CurrentHp <= 0)
        //{
           StartCoroutine(PlayerWin());
        //}
    }

    public void OnPlayerLose()
    {
        //if (Mourntooth.CurrentHp <= 0)
        //{
            StartCoroutine(PlayerLose());
        //}

    }



    void updateScendoHP()
    {
        PlayerPrefs.SetInt("CurrentHP", Mourntooth.CurrentHp);
    } 



    public void fadeToBlack()
    {
        //This is changing the FadeIn/Out image to 1 (0 = invisible / 1 = visible)
        // 2nd argument is the amount of time the fade takes to complete
        blackFade.CrossFadeAlpha(1, 2.0f, false);
        Debug.Log("Fading from encounter...");

    }

    public void fadeFromBlack()
    {
        //This is changing the FadeIn/Out image to 0 (0 = invisible / 1 = visible)
        // 2nd argument is the amount of time the fade takes to complete
        blackFade.CrossFadeAlpha(0, 2.0f, false);
        Debug.Log("Fading into encounter...");
    }

    IEnumerator DelayAndFadeFromBlack()
    {
        fadeFromBlack();
        yield return new WaitForSeconds(1.6f);
    }

    IEnumerator Run()
    {
        animateTextCoroutineRef = AnimateTextCoroutine("Got away safely!");
        StartCoroutine(animateTextCoroutineRef);
        yield return new WaitForSeconds(1.0f);
        fadeToBlack();
        yield return new WaitForSeconds(2.0f);

        updateScendoHP();
        PlayerPrefs.SetInt("ReturnFromEncounter", 1);
        SceneManager.LoadScene("Overworld");
    }

    IEnumerator PlayerWin()
    {
        //----------------------------------------------- Make enemy sprite flash ------------------------
        enemy.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        enemy.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        enemy.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        enemy.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        enemy.SetActive(false);
        //----------------------------------------------- Make enemy sprite flash -----------------------

        animateTextCoroutineRef = AnimateTextCoroutine("You have won this battle! You live to jam another day.");
        StartCoroutine(animateTextCoroutineRef);
        yield return new WaitForSeconds(2.0f);
        fadeToBlack();
        yield return new WaitForSeconds(2.0f);

        //load players last position in overworld
        updateScendoHP();
        PlayerPrefs.SetInt("NewMoveLearned", 1);
        PlayerPrefs.SetInt("ReturnFromEncounter", 1);
        SceneManager.LoadScene("Overworld");
    }

    IEnumerator PlayerLose()
    {
        
        //----------------------------------------------- Make player sprite flash ------------------------
        player.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        player.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        player.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        player.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        player.SetActive(false);
        //----------------------------------------------- Make player sprite flash ------------------------

        animateTextCoroutineRef = AnimateTextCoroutine("You have lost this battle. You will return to a previous moment of triumph!");
        StartCoroutine(animateTextCoroutineRef);
        yield return new WaitForSeconds(3.0f);
        fadeToBlack();
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Overworld");//Load from that last time the player saved.
        
    }



    IEnumerator AnimateTextCoroutine(string message)
    {
        encounterText.text = "";
        for (int currentCharater = 0; currentCharater < message.Length; currentCharater++)
        {
            encounterText.text += message[currentCharater];
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }


}
