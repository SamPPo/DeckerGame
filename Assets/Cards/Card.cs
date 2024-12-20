using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Card : MonoBehaviour
{

    PawnController controller;
    public List<CardEffect_Base> cardEffects;
    public bool endTurn;
    public bool spend;

    //Public delegates
    public delegate void OnCardPlayEnd(bool Bool);
    public static OnCardPlayEnd onCardPlayEnd;

    // Update is called once per frame
    void Update()
    {
        
    }

    //Start Couroutine for card movement
    public void PlayCard(Vector3 pos, Quaternion q, Vector3 sca, PawnController Controller)
    {
        controller = Controller;
        StartCoroutine(WaitWhileCardMoves(pos, q , sca, PublicVariables.TimeToMoveCard)); 
    }

    //Lerp for card movement during couroutine
    IEnumerator WaitWhileCardMoves(Vector3 pos, Quaternion q, Vector3 sca, float time)
    {
        Transform startT = transform;
        float deltaTime = 0;

        while (deltaTime < time)
        {
            transform.position = Vector3.Lerp(startT.position, pos, (deltaTime/time));
            transform.rotation = Quaternion.Lerp(startT.rotation, q, (deltaTime/time));
            transform.localScale = Vector3.Lerp(startT.localScale, sca, (deltaTime/time));
            deltaTime += Time.deltaTime;
            yield return null;
        }
        //make sure the transforms are correct before return
        transform.SetPositionAndRotation(pos, q);
        transform.localScale = sca;

        //Activate effects, call for delegate and return from coroutine
        PlayCardEffects();
        controller.AddCardToPlayPile(this);
        onCardPlayEnd(endTurn);
        yield return null;
    }

    //Activate card effects
    void PlayCardEffects()
    {
        if (cardEffects.Count > 0)
        { 
        foreach (CardEffect_Base item in cardEffects)
        {
            item.PrepareEffect(controller);
        }
        }

    }

}