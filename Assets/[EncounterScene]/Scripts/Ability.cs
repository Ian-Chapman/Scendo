using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "AbilitySystem/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField]
    public new string name;

    [SerializeField]
    public int damage;



    [SerializeField]
    public int accuracy;

    [SerializeField]
    private string description;

    [SerializeField]
    public MonsterType type;

    [SerializeField]
    public MoveEffect moveEffect;

    [SerializeField]
    public GameObject animation;

    [SerializeField]
    public Vector2 animationTransform;

    public Vector3 animationRotation;

    [SerializeField]
    public IEffect[] effects;

    [SerializeField]
    public int index;




    public void Cast(ICharacter self, ICharacter other)
    {
        Debug.Log("Used: " + name);
    }


}

public enum MoveEffect
{
    DAMAGE,
    STATUS,
    HEALING
}
