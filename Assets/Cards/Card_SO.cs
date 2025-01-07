using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Decker;

[CreateAssetMenu(fileName = "Card_SO", menuName = "Scriptable Objects/Card_SO")]
public class Card_SO : ScriptableObject
{
    public string cardName;
    public List<CardEffect> cardEffects = new();
    public bool endTurn;
    public bool spend;


}


