using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Decker;

[CreateAssetMenu(fileName = "Card_SO", menuName = "Scriptable Objects/Card_SO")]
public class Card_SO : ScriptableObject
{
    [SerializeField]
    public string cardName;
    [SerializeField]
    public List<CardEffect> cardEffects = new();
    [SerializeField]
    public bool endTurn;
    [SerializeField]
    public bool spend;


}


