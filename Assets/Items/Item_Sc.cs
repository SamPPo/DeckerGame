using Decker;
using UnityEditor;
using UnityEngine;
using static Card_Sc;

public class Item_Sc : MonoBehaviour, IEffect
{
    public PawnController_Sc controller;
    public TriggerType triggerType;
    public Item_SO ISO;

    private int effectIndex;

    public void BindTriggerType()
    {
        switch (triggerType)
        {
            case TriggerType.roundStart:
                PawnController_Sc.roundStart += TriggerItemEffect;
                break;
            case TriggerType.onCardPlayed:
                break;
            case TriggerType.onReshuffle:
                break;
            case TriggerType.onRoundEnd:
                break;
            default:
                break;
        }
    }

    void TriggerItemEffect(int i)
    {
        if (i == controller.id)
        {
            Debug.Log("Item effect triggered.");
        }
    }

    //Activate card effects
    void IEffect.PlayEffects()
    {
        if (effectIndex < ISO.itemEffects.Count)
        {
            var item = ISO.itemEffects[effectIndex];
            CardEffect_Base.onEffectEnd += PostItemEffect; //bind delegate
            item.cardEffect.InitializeOwners(controller, this);
            item.cardEffect.PrepareEffect(item.magnitude, item.Add_CSOs);
        }
        else
        {
            onCardPlayEnd?.Invoke(false);
        }
    }

    void PostItemEffect()
    {
        effectIndex++;
        ((IEffect)this).PlayEffects();
    }
}
