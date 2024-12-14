using UnityEngine;

public class Attributes_Scr : MonoBehaviour
{
    private int Health;
    public int MaxHealth;
    private int Armor;
    public int SetArmor;
    private int Initiative;

    private int HealthDelta;
    private int ArmorDelta;
    private int DamageDelta;

    private void Start()
    {
        Health = MaxHealth;
        Armor = SetArmor;
    }

    [ContextMenu("TestClick")]
    public void Test()
    {
        int Amount = 5;
        DealDamage(Amount);
        print("Health");
        print(Health);
        print("Armor");
        print(Armor);
    }

    public void DealDamage(int Amount)
    {
        int DamageAmount = -Amount;
        DamageAmount = -ModifyArmor(DamageAmount);
        ModifyHealth(DamageAmount);
    }

    public void GiveArmor(int Amount)
    {
        ModifyArmor(Amount);
    }

    public void GiveHealth(int Amount)
    {
        ModifyHealth(Amount);
    }

    private void ModifyHealth(int Amount)
    {

        if (Amount == 0)
        {
            HealthDelta = 0;
        }
        else if (Amount < 0)
        {
            HealthDelta = Health;
            Health += Amount;
            if (Health <= 0)
            {
                CharacterDied();
            }
            else
            {
                HealthDelta = Amount;
            }
        }
        else
        {
            HealthDelta = MaxHealth - Health;
            Health += Amount;
            if (Health >= MaxHealth)
            {
                Health = MaxHealth;
            }
            else 
            {
                HealthDelta = Amount;
            }
        }

    }
    private int ModifyArmor(int Amount)
    {

        if (Amount == 0)
        {
            ArmorDelta = 0;
        }
        else if (Amount < 0)
        {
            ArmorDelta = Armor;
            Armor += Amount;
            if (Armor <= 0)
            {
                ArmorDestroyed();
            }
            else
            {
                ArmorDelta = Amount;
            }
        }
        else
        {
            ArmorDelta = Amount;
            Armor += Amount;
        }
        DamageDelta = Amount - ArmorDelta;
        return DamageDelta;

    }

    void CharacterDied()
    {

    }
    void ArmorDestroyed()
    {

    }

}
