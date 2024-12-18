using UnityEngine;
using Decker;

public abstract class CardEffect : MonoBehaviour
{
    public int magnitude;

    public PawnController controller;
    public GameObject target;
    public TargettingType targettingType;

    public void SetTarget(GameObject Target)
    {
        target = Target;
    }
  

    public void PrepareEffect(PawnController Controller)
    {
        controller = Controller;
    }

    public abstract void PlayEffect();
}

