using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Decker;
using System;

public class PawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject Character_pfab;
    [SerializeField]
    private List<GameObject> PreviewDeck = new();
    private List<Transform> Deck = new();
    private List<GameObject> PlayPile = new();
    [SerializeField]
    private Transform PlayPileTransform;
    [SerializeField]
    private Transform DeckTransform;
    private PawnController target;

    public GameMaster gameMaster;
    public Faction faction;
    public delegate void OnTurnEnd();
    public static OnTurnEnd onTurnEnd;


    void Start()
    {
        SpawnCharacter();
        SpawnDeck();
    }

    void SpawnCharacter()
    {
        Instantiate(Character_pfab, transform.position, transform.rotation);
    }

    void SpawnDeck()
    {
        foreach (var item in PreviewDeck)
        {
            var itemT = Instantiate(item, (DeckTransform.position + new Vector3(0f, Deck.Count * 0.13f, 0f)), DeckTransform.rotation);
            Deck.Add(itemT.transform);
        }
    }

    private Transform TakeDeckTopCard()
    {
        Transform TopCard = Deck.Last();
        Deck.Remove(Deck.Last());
        return TopCard;
    }
    
    [ContextMenu("PlayTurn")]
    public void PlayTurn()
    {
        Card.onCardPlayEnd += CardPlayDone;
        Transform CurrentCard = TakeDeckTopCard();
        CurrentCard.GetComponent<Card>().PlayCard(PlayPileTransform.transform, this);
    }
    void CardPlayDone()
    {
        Card.onCardPlayEnd -= CardPlayDone;
        onTurnEnd();
    }

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
