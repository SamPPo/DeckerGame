using Decker;
using UnityEngine;

public abstract class CardEffect_Base : ScriptableObject
{
    private PawnController controller;
    public PawnController target;

    public TargettingType targettingType;
    public string effectName;
    public int magnitude;
    

    public void PrepareEffect(PawnController pawnController)
    {
        controller = pawnController;
        target = pawnController.GetTarget(targettingType);
        if (target == null)
        {
            Debug.Log("null BRO");
        }
        ApplyEffect();
    }

    public abstract void ApplyEffect();
    
}
