using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public BatteryItem battery;
    public float lifeTime = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<Inventory>().HasOpening())
            {
                Inventory inventory = collision.GetComponent<Inventory>();
                inventory.AddBattery(battery);
                Destroy(gameObject);
            }
        }
    }

    private void Awake()
    {
        StartCoroutine(DestroyTimer());
    }
    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
