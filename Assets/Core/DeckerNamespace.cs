using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Decker
{
    public enum TargettingType
    {
        hostile,
        ally,
        self
    }

    public enum EffectType
    {
        dealDamage,
        heal,
        giveArmor,
        special
    }

    public enum Faction
    {
        enemy,
        player,
        neutral
    }

    [System.Serializable]
    public class CardEffect
    {
        public int magnitude;
        public CardEffect_Base cardEffect;
        public List<Card_SO> Add_CSOs;
    }

    public struct StTransform
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 sca;
    }
}


