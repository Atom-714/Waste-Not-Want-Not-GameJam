using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Inventory inventory;
    public int slotIndex;
    public Image slotImage = null;
    public TMP_Text textMeshPro;
    public Color selectedColor;
    public Color unselectedColor;
    public bool isStartSelect;

    private Image backgroundImage;
    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
        if (isStartSelect)
        {
            inventory.currentBattery = inventory.batteries[slotIndex];
            foreach (Slot slot in inventory.slots)
            {
                slot.Unselect();
            }
            backgroundImage.color = selectedColor;
            inventory.UpdateUI();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventory.currentBattery = inventory.batteries[slotIndex];
            foreach (Slot slot in inventory.slots)
            {
                slot.Unselect();
            }
            backgroundImage.color = selectedColor;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventory.batteries[slotIndex].battery = null;
            inventory.batteries[slotIndex].currentCharge = 0;
            inventory.batteries[slotIndex].maxCharge = 0;
            SetUI();
        }
    }

    public void SetUI()
    {
        if (inventory.batteries[slotIndex].battery == null)
        {
            slotImage.sprite = null;
            textMeshPro.text = "0%";
            return;
        }

        BatteryInstance battery = inventory.batteries[slotIndex];
        
        if (battery.battery.icon != null)
        {
            slotImage.sprite = battery.battery.icon;
            textMeshPro.text = (int)((float)battery.currentCharge / battery.maxCharge * 100) + "%";
        }
    }

    public void Unselect()
    {
        backgroundImage.color = unselectedColor;
    }
}
