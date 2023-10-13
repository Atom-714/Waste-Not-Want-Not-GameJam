using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battery Item")]
public class BatteryItem : ScriptableObject
{
    public string batteryName;
    public int powerLevel;
    public Sprite icon;
}

[System.Serializable]
public class BatteryInstance
{
    [SerializeField] public BatteryItem battery;
    [SerializeField] public int currentCharge;
    public int maxCharge;

    public BatteryInstance(BatteryItem battery)
    {
        this.battery = battery;
        maxCharge = battery.powerLevel;
        currentCharge = maxCharge;
    }

    public void UpdateCharge(int amount)
    {
        Debug.Log(amount);
        currentCharge -= amount;
        if (currentCharge < 0)
        {
            currentCharge = 0;
        }
    }
    public bool CanShoot(int cost)
    {
        if(currentCharge < cost)
        {
            return false;
        }
        return true;
    }
    public int GetCurrentCharge()
    {
        return currentCharge;
    }
}
