using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    MonsterDatabase monsterDatabase;
    int level; //XP level of the monster

    
    
    
    //Constructor
    public Monster(MonsterDatabase mBase, int mLevel)
    {

        monsterDatabase = mBase;
        level = mLevel;
    }

    //Using the attack formula from the actual Pokemon games
    //https://bulbapedia.bulbagarden.net/wiki/Damage

    public int TotalHp
    {
        get { return Mathf.FloorToInt((monsterDatabase.Speed * level) / 100f) + 10; }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((monsterDatabase.Attack * level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((monsterDatabase.Attack * level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((monsterDatabase.Attack * level) / 100f) + 5; }
    }


}
