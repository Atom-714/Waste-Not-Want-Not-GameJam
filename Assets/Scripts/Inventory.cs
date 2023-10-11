using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BatteryItem> batteries = new List<BatteryItem>();
    public int maxCapacity = 10;

    public void AddBattery(BatteryItem battery)
    {
        if (batteries.Count < maxCapacity)
        {
            batteries.Add(battery);
        }
    }
    public void RemoveBattery(BatteryItem battery)
    {
        if (batteries.Contains(battery))
        {
            batteries.Remove(battery);
        }
    }
}
