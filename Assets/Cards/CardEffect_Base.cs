using Decker;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardEffect_Base : ScriptableObject
{
    private PawnController controller;
    public GameObject target;
    public TargettingType targettingType;


    public void PrepareEffect(PawnController pawnController)
    {
        controller = pawnController;
        target = pawnController.GetTarget(targettingType);
    }

    
}
