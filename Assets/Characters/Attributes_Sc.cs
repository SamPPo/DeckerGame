using UnityEngine;

public class Attributes_Sc : MonoBehaviour
{
    private int Health;
    public int MaxHealth;
    private int Armor;
    public int SetArmor;
    private int Initiative;
    private int DamageDelta;

    [SerializeField]
    private GameObject healthBar;
 

    private void Start()
    {
        Health = MaxHealth;
        Armor = SetArmor;
        healthBar.GetComponent<HealthBar>().SetMaxHealth(MaxHealth);
        //healthBar = Instantiate(HP_Bar)
    }

    public void DealDamage(int Amount)
    {
        int AmountDelta = Amount;
        if (Armor > 0)
        {
            AmountDelta = SubtractArmor(AmountDelta);
        }
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
            ArmorDestroyed();
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
        healthBar.GetComponent<HealthBar>().UpdateHealth(Health);
        
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
        healthBar.GetComponent<HealthBar>().UpdateHealth(Health);
    }
    
    void CharacterDied()
    {

    }
    void ArmorDestroyed()
    {

    }

}
