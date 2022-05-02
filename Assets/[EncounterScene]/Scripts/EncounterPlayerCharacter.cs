using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterPlayerCharacter : ICharacter
{
    [SerializeField]
    private AICharacter opponent;
    [SerializeField]
    private EncounterInstance myEncounter;

    public MasterScendoDatabase scendoDatabase;
    Ability[] scendoAttacks;

    public BattleManager battleManager;
    public EncounterUI encounterUI;

    private void Start()
    {
        //this.GetComponent<SpriteRenderer>().sprite = scendoDatabase.Mourntooth.BackSprite;
        //scendoAttacks = scendoDatabase.Mourntooth.MonsterAbilities;

    }

    public override void TakeTurn(EncounterInstance encounter)
    {
        myEncounter = encounter;
        opponent = encounter.Enemy;
        Debug.Log("Player taking turn");
    }

    public void UseAbility(int slot)
    {
        scendoAttacks[slot].Cast(this, opponent);

        //scendoAttacks[slot].Cast(this, opponent);
        //myEncounter.AdvanceTurns();



        //Add move animation for move here
    }
}
