using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Decker;
using System;
using Unity.Jobs;
using System.Security.Claims;

public class PawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject Character_pfab;
    [SerializeField]
    private List<GameObject> PreviewDeck = new();
    private List<Transform> Deck = new();
    private List<Transform> PlayPile = new();
    [SerializeField]
    private Transform PlayPileTransform;
    [SerializeField]
    private Transform DeckTransform;
    private PawnController target;

    public GameMaster gameMaster;
    public Faction faction;

    private bool end;
    private bool spend;

    //Public delegates 
    public delegate void OnTurnEnd();
    public static OnTurnEnd onTurnEnd;


    void Start()
    {
        PlayPileTransform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, 0f));
        DeckTransform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, -1f), new Vector3(0f, -1f, 0f));
        SpawnCharacter();
        SpawnDeck();  
    }

    void SpawnCharacter()
    {
        Instantiate(Character_pfab, transform.position, transform.rotation);
    }

    //Spawn deck and place cards by offset so they form a pile. NOTE! Current i = 1 is not preferable, figure out a way to do this later
    void SpawnDeck()
    {
        var i = 1;
        foreach (var item in PreviewDeck)
        {
            var itemT = Instantiate(item, DeckTransform.position + (PublicVariables.CardThickness * i * Vector3.up), DeckTransform.rotation);
            Deck.Add(itemT.transform);
            i++;
        }
    }

    //Play turn. Draw top card of deck and activate PlayCard method of the drawn card.
    public void PlayTurn()
    {
        var pos = PlayPileTransform.position + PublicVariables.CardThickness * PlayPile.Count * Vector3.up;
        if (Deck.Count > 0)
        {
            Transform CurrentCard = TakeDeckTopCard();
            Card.onCardPlayEnd += StartWaitAfterCardPlay;
            CurrentCard.GetComponent<Card>().PlayCard(pos, PlayPileTransform.rotation, PlayPileTransform.localScale, this);
        }
        else
        {
            ReshuffleDeck();
            StartCoroutine(WaitAfterCardPlay(true));
        }
    }

    //Take top card of deck and remove it from the deck.
    private Transform TakeDeckTopCard()
    {
        Transform TopCard = Deck.Last();
        Deck.Remove(Deck.Last());
        return TopCard;
    }
    
    //Wait After delegate event from card "OnCardPlayEnd". Start couroutine->.
    void StartWaitAfterCardPlay(bool B)
    {
        Card.onCardPlayEnd -= StartWaitAfterCardPlay;
        StartCoroutine(WaitAfterCardPlay(B));
    }

    //Wait after card play couroutine. After timer is finished call onTurnEnd event to pass priority back to GameMaster
    IEnumerator WaitAfterCardPlay(bool B)
    {
        yield return new WaitForSeconds(PublicVariables.TimeAfterCardPlay);
        if (B)
        {
            onTurnEnd();
        }
        else
        {
            PlayTurn();
        }
        
    }

    //Well... Adds card to playpile
    public void AddCardToPlayPile(Card item)
    {
        PlayPile.Add(item.transform);
    }

    //Reshuffle deck and move the card to deck
    public void ReshuffleDeck()
    {
        int i = 1;
        foreach (var item in PlayPile)
        {
            item.transform.SetPositionAndRotation(DeckTransform.position + (PublicVariables.CardThickness * i * Vector3.up), DeckTransform.rotation);
            item.transform.localScale = DeckTransform.localScale;
            i++;
            Deck.Add(item);

        }
        PlayPile.Clear();
    }

    //Get target for card effect based on Targetting enum and Faction enum (in Decker namespace).
    public PawnController GetTarget(TargettingType t)
    {
        
        switch (t)
        {
            case TargettingType.hostile:
                if (faction == Faction.player)
                {
                    foreach (PawnController item in gameMaster.pawnControllers)
                    {
                        if (item.faction == Faction.enemy)
                        {
                            target = item;
                        }
                    }
                }
                else if (faction == Faction.enemy)
                {
                    foreach (PawnController item in gameMaster.pawnControllers)
                    {
                        if (item.faction == Faction.player)
                        {
                            target = item;
                        }
                    }
                }
                break;
            case TargettingType.ally:
                break;
            case TargettingType.self:
                target = this;
                break;
        }
        return target;
    }

}
