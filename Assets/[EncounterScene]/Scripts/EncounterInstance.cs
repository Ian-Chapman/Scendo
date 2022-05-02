using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EncounterInstance : MonoBehaviour
{
    [SerializeField]
    private EncounterPlayerCharacter player;
    [SerializeField]
    private AICharacter enemy;
    
    public AICharacter Enemy
    {
        get {return enemy;}
        private set { enemy = value; } 
    }

    public EncounterPlayerCharacter Player
    {
        get { return player; }
        private set { player = value; }
    }

    //Events
    public UnityEvent<ICharacter> onCharacterTurnBegin;
    public UnityEvent<ICharacter> onCharacterTurnEnd;
    public UnityEvent<EncounterPlayerCharacter> onPlayerTurnBegin;
    public UnityEvent<EncounterPlayerCharacter> onPlayerTurnEnd;
    public UnityEvent<AICharacter> onEnemyTurnBegin;
    public UnityEvent<AICharacter> onEnemyTurnEnd;


    //Turn Counter
    private int turnNumber = 0;

    [SerializeField]
    public ICharacter currentCharacterTurn;


    // Start is called before the first frame update
    void Start()
    {
        currentCharacterTurn = player;
        onPlayerTurnBegin.Invoke(player);
    }


    public void AdvanceTurns()
    {
        

        if (currentCharacterTurn == player)
        {
            onPlayerTurnEnd.Invoke(player);
            currentCharacterTurn = enemy;
            onCharacterTurnBegin.Invoke(currentCharacterTurn);


        }
        else if(currentCharacterTurn == enemy)
        {
            onCharacterTurnEnd.Invoke(currentCharacterTurn);
            currentCharacterTurn = player;
            onPlayerTurnBegin.Invoke(player);
            
        }
        turnNumber++;

        //onCharacterTurnBegin.Invoke(currentCharacterTurn);
        currentCharacterTurn.TakeTurn(this);

    }

    public void EndBattle()
    {

    }
}
