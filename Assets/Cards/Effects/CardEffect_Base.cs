using Decker;
using UnityEngine;
using System.Collections.Generic;

public abstract class CardEffect_Base : ScriptableObject
{
    private PawnController controller;
    public PawnController target;
    public Card card;

    public string bodyText;
    public TargettingType targettingType;
    public string effectName;
    protected int magnitude;
    protected List<Card_SO> card_SOs;

    public delegate void OnEffectEnd();
    public static OnEffectEnd onEffectEnd;

    //setup controller and effect owner card
    public void InitializeOwners(PawnController Ocontroller, Card Ocard)
    {
        controller = Ocontroller;
        card = Ocard;
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
