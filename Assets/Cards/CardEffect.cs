using UnityEngine;

public class CardEffect : MonoBehaviour
{
    [SerializeField]
    EffectType effectType;
}

enum EffectType
{ 
    DealDamage,
    GiveArmor,
    Heal,
    AddCards,
    Special
}

