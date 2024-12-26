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
    public List<Transform> Deck = new();
    private List<Transform> PlayPile = new();
    [SerializeField]
    private Transform PlayPileTransform;
    [SerializeField]
    private Transform DeckTransform;
    private PawnController target;

    public GameMaster gameMaster;
    public Faction faction;

    //Public delegates 
    public delegate void OnTurnEnd();
    public static OnTurnEnd onTurnEnd;


    void Start()
    {
        if (PlayPileTransform == null || DeckTransform == null)
        {
            Debug.LogError("Transforms are not assigned.");
            return;
        }

        PlayPileTransform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, 0f));
        DeckTransform.rotation = Quaternion.LookRotation(new Vector3(0f, 0f, -1f), new Vector3(0f, -1f, 0f));
        SpawnCharacter();
        SpawnDeck();  
    }

    void SpawnCharacter()
    {
        if (Character_pfab == null)
        {
            Debug.LogError("Character prefab is not assigned.");
            return;
        }

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
        StTransform targetT;
        targetT.pos = pos;
        targetT.rot = PlayPileTransform.rotation;
        targetT.sca = PlayPileTransform.localScale;
        if (Deck.Count > 0)
        {
            Transform CurrentCard = TakeDeckTopCard();
            Card.onCardPlayEnd += StartWaitAfterCardPlay;
            CurrentCard.GetComponent<Card>().PlayCard(targetT, this);
        }
        else
        {
            StartCoroutine(ReshuffleDeck());
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
            onTurnEnd?.Invoke();
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
    public IEnumerator ReshuffleDeck()
    {
        // Move cards from PlayPile back to Deck
        int i = 1;
        foreach (var item in PlayPile)
        {
            item.transform.SetPositionAndRotation(DeckTransform.position + (PublicVariables.CardThickness * i * Vector3.up), DeckTransform.rotation);
            item.transform.localScale = DeckTransform.localScale;
            i++;
            Deck.Add(item);
        }
        PlayPile.Clear();

        // Visual shuffling effect
        for (int shuffleCount = 0; shuffleCount < 3; shuffleCount++)
        {
            for (int j = 0; j < Deck.Count; j++)
            {
                Transform card = Deck[j];
                Vector3 shuffleOffset = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0, UnityEngine.Random.Range(-0.2f, 0.2f));
                card.position += shuffleOffset;
                yield return new WaitForSeconds(0.05f);
                card.position -= shuffleOffset;
            }
        }

        // Continue the game after reshuffling
        StartCoroutine(WaitAfterCardPlay(true));
    }

    //Get closest target for card effect based on Targetting enum and Faction enum (in Decker namespace).
    public PawnController GetTarget(TargettingType t)
    {
        float closestDistance = float.MaxValue;
        PawnController closestTarget = null;

        switch (t)
        {
            case TargettingType.hostile:
                foreach (var item in gameMaster.pawnControllers)
                {
                    if (item.faction != faction)
                    {
                        float distance = Vector3.Distance(transform.position, item.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestTarget = item;
                        }
                    }
                }
                break;

            case TargettingType.ally:
                foreach (var item in gameMaster.pawnControllers)
                {
                    if (item.faction == faction && item != this)
                    {
                        float distance = Vector3.Distance(transform.position, item.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestTarget = item;
                        }
                    }
                }
                break;

            case TargettingType.self:
                closestTarget = this;
                break;
        }

        target = closestTarget;
        return target;
    }

    public StTransform GetDeckTopSpot()
    {
        StTransform stTransform;
        stTransform.rot = DeckTransform.rotation;
        stTransform.sca = DeckTransform.localScale;
        stTransform.pos = DeckTransform.position + (PublicVariables.CardThickness * Deck.Count * Vector3.up);

        return stTransform;
    }
}
