using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public BatteryItem battery;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory inventory = collision.GetComponent<Inventory>();
            if (inventory.batteries.Count < inventory.maxCapacity)
            {
                inventory.AddBattery(battery);
                Destroy(gameObject);
            }
        }
    }
}
