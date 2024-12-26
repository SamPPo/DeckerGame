using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using Decker;
using Newtonsoft.Json.Bson;

public class Card : MonoBehaviour
{

    public PawnController controller;
    public TextMeshProUGUI tmpTitle;
    public TextMeshProUGUI tmpBody;
    public Card_SO CSO;

    int effectIndex;

    //Public delegates
    public delegate void OnCardPlayEnd(bool Bool);
    public static OnCardPlayEnd onCardPlayEnd;

    public delegate void OnMoveEnd();
    public static OnMoveEnd onMoveEnd;

    private void Start()
    {
        InitializeCard();
    }

    private void InitializeCard()
    {
        SetTitleText();
        SetBodyText();
    }

    //Set card UI title text
    private void SetTitleText()
    {
        tmpTitle.text = CSO.cardName;
    }

    //Set card UI body text. Extrapolated from CardEffect_Base list in Card_SO
    private void SetBodyText()
    {
        string s = "";
        int i = 0;
        foreach (CardEffect item in CSO.cardEffects)
        {
            if (item.cardEffect is CE_AddCards)
            {
                foreach(Card_SO c in item.Add_CSOs)
                {
                    s += "Add ";
                    s += c.cardName;
                    s += System.Environment.NewLine;
                }

            }
            else 
            { 
                s += item.magnitude + " " + item.cardEffect.bodyText;
                s += System.Environment.NewLine;
            }

            i++;
        }

        if (CSO.endTurn)
        {
            s += "END";
            s += System.Environment.NewLine;
        }

        if (CSO.spend)
        {
            s += "SPEND";
            s += System.Environment.NewLine;
        }

        tmpBody.text = s;
    }


    //Card movement coroutine
    public void MoveCard(float wait, StTransform targetT, float time)
    {
        StartCoroutine(WaitWhileCardMoves(wait, targetT, time));
    }

    //Start Couroutine for card movement
    public void PlayCard(StTransform targetT, PawnController Controller)
    {
        effectIndex = 0;
        controller = Controller;
        onMoveEnd += ActivateCardAfterMove;
        StartCoroutine(WaitWhileCardMoves(0, targetT, PublicVariables.TimeToMoveCard));
    }


    IEnumerator WaitWhileCardMoves(float wait, StTransform targetT, float time)
    {
        StTransform startT = PublicVariables.MakeTransform(transform);
        float deltaTime = 0;
        // Calculate the peak height of the arc
        float peakHeight = 2.0f; // Adjust this value to control the height of the arc

        if (wait > 0) { yield return new WaitForSeconds(wait); }

        while (deltaTime < time)
        {
            float t = deltaTime / time;

            // Calculate the parabolic position
            Vector3 currentPos = Vector3.Lerp(startT.pos, targetT.pos, t);
            currentPos.y += peakHeight * Mathf.Sin(Mathf.PI * t); // Add the arc effect

            // Interpolate rotation and scale
            transform.SetPositionAndRotation(currentPos, Quaternion.Lerp(startT.rot, targetT.rot, t));
            transform.localScale = Vector3.Lerp(startT.sca, targetT.sca, t);

            deltaTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final transforms are set correctly
        transform.SetPositionAndRotation(targetT.pos, targetT.rot);
        transform.localScale = targetT.sca;

        // Activate effects, call for delegate and return from coroutine
        onMoveEnd?.Invoke();
        yield return null;
    }

    void ActivateCardAfterMove()
    {
        // Add card to playpile
        controller.AddCardToPlayPile(this);
        onMoveEnd -= ActivateCardAfterMove;
        effectIndex = 0;
        PlayCardEffects();
    }

    //Activate card effects
    void PlayCardEffects()
    {
        if (effectIndex < CSO.cardEffects.Count)
        {
            var item = CSO.cardEffects[effectIndex];
            CardEffect_Base.onEffectEnd += PostCardEffect; //bind delegate
            item.cardEffect.InitializeOwners(controller, this);
            item.cardEffect.PrepareEffect(item.magnitude, item.Add_CSOs);
        }
        else
        {
            onCardPlayEnd?.Invoke(CSO.endTurn);
        }
    }

    void PostCardEffect()
    {
        StartCoroutine(PostCardEffectWait());
    }

    IEnumerator PostCardEffectWait()
    {
        yield return new WaitForSeconds(PublicVariables.TimeAfterEffect);
        CardEffect_Base.onEffectEnd -= PostCardEffect;
        effectIndex++;
        PlayCardEffects();
    }

}