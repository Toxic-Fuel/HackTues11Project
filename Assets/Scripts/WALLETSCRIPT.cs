using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WALLETSCRIPT : MonoBehaviour
{
    public class Wallet
    {

        private float money = 0;

       public void AddMoney(float amount)
        {
            money += amount;
            Debug.Log("Robot received " + amount + ". New balance: " + money);
        }

        public bool RemoveMoney(float amount)
        {
            if (money >= amount)
            {
                money -= amount;
                Debug.Log("Robot spent " + amount + ". Remaining balance: " + money);
                return true;
            }
            else
            {
                Debug.Log("Not enough money!");
                return false;
            }
        }

        public float GetBalance()
        {
            return money;
        }
    }

    public Wallet robotWallet;

    void Start()
    {

    }

    void GiveDailyMoney()
    {

    }
}
