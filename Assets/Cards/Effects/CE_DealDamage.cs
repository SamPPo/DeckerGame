using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageEffect", menuName = "DealDamage_CE")]
public class CE_DealDamage : CardEffect_Base
{
    public override void ApplyEffect()
    {
        target.gameObject.GetComponent<Attributes_Sc>().DealDamage(magnitude);
        onEffectEnd();
    }
}
