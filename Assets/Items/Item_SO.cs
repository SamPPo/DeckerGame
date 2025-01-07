using Decker;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_SO", menuName = "Scriptable Objects/Item_SO")]
public class Item_SO : ScriptableObject
{
    public string itemName;
    public List<CardEffect> itemEffects = new();
    public TriggerType triggerType;
}
