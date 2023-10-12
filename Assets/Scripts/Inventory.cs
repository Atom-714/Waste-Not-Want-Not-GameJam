using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<BatteryInstance> batteries = new List<BatteryInstance>();
    public BatteryInstance currentBattery;
    public int maxCapacity = 10;
    public GameObject inventoryUI;

    public void AddBattery(BatteryItem battery)
    {
        if (batteries.Count < maxCapacity)
        {
            BatteryInstance batteryInstance = new BatteryInstance(battery);
            batteries.Add(batteryInstance);
        }
    }
    public void RemoveBattery(BatteryInstance battery)
    {
        if (batteries.Contains(battery))
        {
            batteries.Remove(battery);
        }
    }

    public void OpenMenu()
    {
        inventoryUI.SetActive(!inventoryUI.active);

    }
}
