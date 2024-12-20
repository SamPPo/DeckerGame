using Decker;
using UnityEngine;

public abstract class CardEffect_Base : ScriptableObject
{
    private PawnController controller;
    public PawnController target;

    public TargettingType targettingType;
    public string effectName;
    public int magnitude;
    
    //setup controller and target for effect
    public void PrepareEffect(PawnController pawnController)
    {
        controller = pawnController;
        target = pawnController.GetTarget(targettingType);
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
