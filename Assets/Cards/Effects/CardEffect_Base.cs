using Decker;
using UnityEngine;
using System.Collections.Generic;

public abstract class CardEffect_Base : ScriptableObject
{
    private PawnController_Sc controller;
    public PawnController_Sc target;
    public IEffect effectOwner;

    public string bodyText;
    public TargettingType targettingType;
    public string effectName;
    protected int magnitude;
    protected List<Card_SO> card_SOs;

    public delegate void OnEffectEnd();
    public static OnEffectEnd onEffectEnd;

    //setup controller and effect owner card
    public void InitializeOwners(PawnController_Sc c1, IEffect c2)
    {
        controller = c1;
        effectOwner = c2;
    }

    //setup controller and target for effect
    public void PrepareEffect(int mag, List<Card_SO> csos)
    {
        card_SOs = csos;
        magnitude = mag;
        target = controller.GetTarget(targettingType);
        if (target == null)
        {
            Debug.Log("null BRO");
        }
        else
        {
            ApplyEffect();
        }
    }

    //activate effect. Implemented in inherited card effects.
    public abstract void ApplyEffect();
    
}
