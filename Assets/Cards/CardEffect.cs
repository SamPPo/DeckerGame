using UnityEngine;

namespace Decker
{
    public enum Targetting
    {
        hostile,
        ally,
        self
    }
}
public abstract class CardEffect : MonoBehaviour
{
    public enum EffectTypes
    {
        dealDamage,
        giveArmor,
        heal,
        addCards,
        special
    }

   

    public EffectTypes effectType;
    public int magnitude;

    private PawnController controller;
    private GameObject target;

    public void SetTarget(GameObject Target)
    {
        target = Target;
    }
  

    public void PlayEffect(PawnController Controller)
    {
        controller = Controller;
        
        switch (effectType)
        {
            case EffectTypes.dealDamage:
                DealDamage();
                break;
            case EffectTypes.giveArmor:
                GiveArmor(); 
                break;
            case EffectTypes.heal: 
                Heal();
                break;
            case EffectTypes.addCards:
                AddCards();
                break;
            case EffectTypes.special:
                Special();
                break;
            default:
                break;
        }
    }

    public void DealDamage()
    {
        Attributes attributes = target.GetComponent<Attributes>();

        if (attributes)
        {
            target.GetComponent<Attributes>().DealDamage(magnitude);
        }
        else
        {
            print("Target doesn't contain Attributes script!");
        }
        
    }

    public void GiveArmor()
    {

    }

    public void Heal()
    {

    }

    public void AddCards()
    {

    }

    public abstract void Special();

}

