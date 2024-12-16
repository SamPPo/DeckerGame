using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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

    /*
    [ContextMenu("GetTopCard")]
    public void Debug()
    {
        print(GetDeckTopCard());
    }
    */

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    void PlayTurn()
    {

        Card.onCardPlayEnd += CardPlayDone;
        Transform CurrentCard = TakeDeckTopCard();
        CurrentCard.GetComponent<Card>().PlayCard(PlayPileTransform.transform);
    }
    void CardPlayDone()
    {
       
    }

}
