using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<BatteryInstance> batteries = new List<BatteryInstance>();
    public Slot[] slots;
    public BatteryInstance currentBattery;
    public BatteryItem baseBattery;
    public int maxCapacity = 6;
    public GameObject inventoryUI;
    public float slowMoScale = 0.5f;
    private bool isInventoryOpen = false;
    private float originalTimeScale;


    public void Start()
    {
        originalTimeScale = Time.timeScale;
        BatteryInstance newBatteryInstance = new BatteryInstance(baseBattery);
        batteries[0].battery = newBatteryInstance.battery;
        batteries[0].currentCharge = newBatteryInstance.battery.powerLevel;
        batteries[0].maxCharge = newBatteryInstance.battery.powerLevel;
        UpdateUI();
    }

    public void AddBattery(BatteryItem battery)
    {
        for (int i = 0; i < batteries.Count; i++)
        {
            if (batteries[i].battery == null)
            {
                BatteryInstance newBatteryInstance = new BatteryInstance(battery);
                batteries[i].battery = newBatteryInstance.battery;
                batteries[i].currentCharge = newBatteryInstance.battery.powerLevel;
                batteries[i].maxCharge = newBatteryInstance.battery.powerLevel;
                break;
            }
        }
        UpdateUI();
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
        UpdateUI();
        inventoryUI.SetActive(!isInventoryOpen);
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen)
        {
            Time.timeScale = slowMoScale;
        }
        else
        {
            Time.timeScale = originalTimeScale;
        }
    }
    public void UpdateUI()
    {
        foreach (Slot slot in slots)
        {
            slot.SetUI();
        }
    }
}
