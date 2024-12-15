using UnityEngine;

public class Attributes_Scr : MonoBehaviour
{
    private int Health;
    public int MaxHealth;
    private int Armor;
    public int SetArmor;
    private int Initiative;

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
        int AmountDelta = Amount;
        AmountDelta = SubtractArmor(AmountDelta);
        SubtractHealth(AmountDelta);
    }
    public void GiveArmor(int Amount)
    {
        AddArmor(Amount);
    }

    public void GiveHealth(int Amount)
    {
        AddHealth(Amount);
    }

    private int SubtractArmor(int Amount)
    {
        int Delta;

        if (Amount > Armor)
        {
            Delta = Amount - Armor;
            Armor = 0;
        }
        else
        {
            Delta = 0;
            Armor -= Amount;
        }
        return Delta;
    }

    private void SubtractHealth(int Amount)
    {
        
        if (Amount >= Health)
        {
            Health = 0;
            CharacterDied();
        }
        else
        {
            Health -= Amount;
        }
        
    }

    private void AddArmor(int Amount)
    {
        Armor += Amount;
    }
    private void AddHealth(int Amount)
    {
        Health += Amount;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }
    
    void CharacterDied()
    {

    }
    void ArmorDestroyed()
    {

    }

}
