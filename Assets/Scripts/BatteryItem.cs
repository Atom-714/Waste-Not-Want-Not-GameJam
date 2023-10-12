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
    [SerializeField] BatteryItem battery;
    [SerializeField] int currentCharge;
    private int maxCharge;

    public BatteryInstance(BatteryItem battery)
    {
        this.battery = battery;
        maxCharge = battery.powerLevel;
        currentCharge = maxCharge;
    }

    public void UpdateCharge(int amount)
    {
        currentCharge -= amount;
        if (currentCharge < 0)
        {
            currentCharge = 0;
        }
        else if (currentCharge > maxCharge)
        {
            currentCharge = maxCharge;
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
