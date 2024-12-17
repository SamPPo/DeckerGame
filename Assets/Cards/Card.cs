using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public delegate void OnCardPlayEnd();
    public static OnCardPlayEnd onCardPlayEnd;

    PawnController controller;
    public List<CardEffect> cardEffects;
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
        foreach (CardEffect item in cardEffects)
        {
            item.PlayEffect(controller);
        }

    }

}