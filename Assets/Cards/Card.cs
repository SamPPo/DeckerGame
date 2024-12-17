using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    public delegate void OnCardPlayEnd();
    public static OnCardPlayEnd onCardPlayEnd;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayCard(Transform PlayPileTransform)
    {
        gameObject.transform.position = PlayPileTransform.position;
        gameObject.transform.localScale = PlayPileTransform.localScale;
        onCardPlayEnd();
    }
}

public struct CardSetup
{
    List<CardEffect> CardEffects;
    bool EndTurn;
    bool Spend;
}