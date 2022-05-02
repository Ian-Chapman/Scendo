using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idea for ScriptableObjects from Game Engines III lab with Joss

[CreateAssetMenu(fileName = "Monster", menuName = "Monster/Create Monster")]
public class MonsterDatabase : ScriptableObject
{


    [SerializeField]
    string monsterName;

    [SerializeField]
    public int level;

    [TextArea]
    [SerializeField]
    string description;

    [SerializeField]
    MonsterType type;

    [SerializeField]
    int totalHp;

    [SerializeField]
    int currentHp;

    [SerializeField]
    int attack;

    [SerializeField]
    int defense;

    [SerializeField]
    int speed; //decides who attacks first

    [SerializeField]
    Ability[] monsterAbilities;

    //Still need to create sprites
    //May only need from sprite for enemy monsters
    [SerializeField]
    Sprite frontSprite;

    [SerializeField]
    Sprite backSprite;


    //C# properties. Similar to using a Getter function
    //Need monster name available outside of the class
    public string MonsterName
    {
        get { return monsterName; }
        set { monsterName = value; }
    }

    public string Description
    {
        get { return description; }
    }



    //public int TotalHp { get; set; }

    public int TotalHp
    {

        get { return totalHp; }
        set { totalHp = value; }

    }

    public int CurrentHp { get; set; }

    public int Attack
    {
        get { return attack; }
        set { attack = value; }

    }

    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }


    public Sprite FrontSprite
    {
        get { return frontSprite; }
        set { frontSprite = value; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
        set { backSprite = value; }
    }

    public Ability[] MonsterAbilities
    {
        get { return monsterAbilities; }
        set { monsterAbilities = value; }

    }

    public MonsterType ScendoType
    {
        get { return type; }
        set { type = value; }
    }

}





//Idea for enum from Mobile Game Development lab with Tom Tsiliopolus
public enum MonsterType
{
    NONE,
    WATER,
    FIRE,
    GRASS,
    NORMAL
    // can add other types as needed
}
