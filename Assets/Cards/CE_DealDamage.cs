using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CE_DealDamage : CardEffect
{

    public override void PlayEffect()
    {
        Attributes attributes = target.GetComponent<Attributes>();
        if (attributes)
        {
            attributes.DealDamage(magnitude);
        }
    }

}
