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

    public enum TriggerType
    {
        roundStart,
        onCardPlayed,
        onReshuffle,
        onRoundEnd
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

public interface IEffect
{
    public delegate void Asd();
    public static Asd asd;

    //void SetController(PawnController_Sc controller, IEffect owner);
    public void PlayEffects();
}

