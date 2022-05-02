using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : ICharacter
{

    public MonsterDatabase ScendoMonster;
    Ability[] scendoAttacks;


    [SerializeField]
    private EncounterPlayerCharacter opponent;
    
    [SerializeField]
    private EncounterInstance myEncounter;

    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    float timeBetweenCharacters = 0.1f;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    public GameObject mainPanel;

    [SerializeField]
    public GameObject abilityPanel;

    int moveToUse;


    GameObject AIHealthBar;
    GameObject PlayerHealthBar;
    public BattleManager battleManager;
    public EncounterUI encounterUI;

    AttackAnimationController attackAnimController;

    private void Start()
    {
        attackAnimController = GameObject.Find("AttackAnimationController").GetComponent<AttackAnimationController>();
        player = GameObject.Find("Mourntooth");
    }

    private IEnumerator animateTextCoroutineRef = null;

    bool isAttacking = false;

    public override void TakeTurn(EncounterInstance encounter)
    {
        //myEncounter = encounter;
        //opponent = myEncounter.Player;
        //StartCoroutine(DelayDecision(myEncounter));

    }

    public void loadScendo()
    {
        this.GetComponent<SpriteRenderer>().sprite = ScendoMonster.FrontSprite;
        scendoAttacks = ScendoMonster.MonsterAbilities;
        ScendoMonster.CurrentHp = ScendoMonster.TotalHp;
        AIHealthBar = GameObject.Find("EnemyHealthBar");
        PlayerHealthBar = GameObject.Find("PlayerHealthBar");
    }

    public void UseAbility(int slot)
    {

        scendoAttacks[slot].Cast(this, opponent);
        //scendoAttacks[slot].Cast(this, opponent);
        //myEncounter.AdvanceTurns();

    }

    public void TakeDamage(int damageRecieved)
    {
        ScendoMonster.CurrentHp -= damageRecieved;
        //AIHealthBar.GetComponent<HealthBarScript>().UpdateHealthBar();
    }

    public void HealScendo(int healAmount)
    {
        if (ScendoMonster.CurrentHp + healAmount > ScendoMonster.TotalHp)
        {
            ScendoMonster.CurrentHp = ScendoMonster.TotalHp;
        }
        else
        {
            ScendoMonster.CurrentHp += healAmount;
        }
    }


    public IEnumerator DelayDecisionBetter()
    {
        if (ScendoMonster.CurrentHp > 0)
        {
            Debug.Log("Enemy taking turn");

            moveToUse = Random.Range(0, 2);
            //moveToUse = 1;

            animateTextCoroutineRef = AnimateTextCoroutine("Enemy " + ScendoMonster.MonsterName + " used " + scendoAttacks[moveToUse].name + "!");
            //encounterUI.TakeDamage(ScendoMonster.MonsterAbilities[moveToUse], ScendoMonster.MonsterAbilities[moveToUse].damage);
            //Debug.Log(ScendoMonster.MonsterAbilities[moveToUse]);

            if (ScendoMonster.MonsterAbilities[moveToUse].moveEffect == MoveEffect.DAMAGE)
            {
                int damageToDeal = battleManager.TakeDamage(ScendoMonster.MonsterAbilities[moveToUse], ScendoMonster, encounterUI.Mourntooth);
                encounterUI.TakeDamage(damageToDeal);
            }
            else if (ScendoMonster.MonsterAbilities[moveToUse].moveEffect == MoveEffect.HEALING)
            {
                int hpToHeal = battleManager.HealHealth(ScendoMonster.MonsterAbilities[moveToUse], ScendoMonster);
                HealScendo(hpToHeal);
            }


            abilityPanel.SetActive(false);
            mainPanel.SetActive(false);

            StartCoroutine(animateTextCoroutineRef);

            if (ScendoMonster.MonsterAbilities[moveToUse].moveEffect == MoveEffect.DAMAGE)
            {
                attackAnimController.OnAttackAnim(ScendoMonster.MonsterAbilities[moveToUse], PlayerHealthBar.GetComponent<HealthBarScript>());
            }
            else if (ScendoMonster.MonsterAbilities[moveToUse].moveEffect == MoveEffect.HEALING)
            {
                attackAnimController.OnAttackAnim(ScendoMonster.MonsterAbilities[moveToUse], AIHealthBar.GetComponent<HealthBarScript>());

            }



            if (battleManager.playerFaster == false)
            {
                yield return new WaitForSeconds(4.0f);
                StartCoroutine(encounterUI.DoAttack(encounterUI.chosenMove, encounterUI.chosenMoveName));
            }

            else if (battleManager.playerFaster == true)
            {
                yield return new WaitForSeconds(4.0f);
                mainPanel.SetActive(true);
                encounterUI.ResetTurn();
            }

        }
        
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
