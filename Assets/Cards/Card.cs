using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public delegate void OnCardPlayEnd();
    public static OnCardPlayEnd onCardPlayEnd;

    PawnController controller;
    public List<CardEffect_Base> cardEffects;
    public bool endTurn;
    public bool spend;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCard(Transform PlayPileTransform, PawnController Controller)
    {
        controller = Controller;
        gameObject.transform.position = PlayPileTransform.position;
        gameObject.transform.localScale = PlayPileTransform.localScale;
        PlayCardEffects();
        onCardPlayEnd();
    }

    void PlayCardEffects()
    {
        foreach (CardEffect_Base item in cardEffects)
        {
            item.PrepareEffect(controller);
        }

    }

}