using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Decker;
using UnityEngine.InputSystem.XR;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "NewAddCardsEffect", menuName = "AddCard_CE")]
public class CE_AddCards : CardEffect_Base
{
    private int i = 0;
    public GameObject gameObject;

    public override void ApplyEffect()
    {
        SpawnAndMoveCards();
    }

    void SpawnAndMoveCards()
    {
        Card_Sc.onMoveEnd += CardAddMoveComplete;
        i = 0;
        foreach (Card_SO item in card_SOs)
        {

            // Instantiate the new card
            var newCard = Instantiate(gameObject, card.gameObject.transform.position + (Vector3.up * (0.2f + (PublicVariables.CardThickness * i))) + (Vector3.back * (i + PublicVariables.CardWidth)), card.gameObject.transform.rotation);
            var c = newCard.GetComponent<Card_Sc>();
            c.CSO = item;
            c.controller = target;
            c.controller.Deck.Add(newCard.transform);

            // Set target transform
            StTransform targetT = target.GetDeckTopSpot();

            // order card to move
            newCard.GetComponent<Card_Sc>().MoveCard(1f, targetT, PublicVariables.TimeSpawnCardMove + (i * 0.2f));

            if (i + 1 >= card_SOs.Count)
            {
                i += 1000;
            }

            i++;

        }
    }

    void CardAddMoveComplete()
    {
        i--;
        if (i == 1000)
        {
            Card_Sc.onMoveEnd -= CardAddMoveComplete;
            EndEffect();
        }
    }

    void EndEffect()
    {
        onEffectEnd();
    }
}
