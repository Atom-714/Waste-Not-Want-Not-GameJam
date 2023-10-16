using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 1;
    public Transform firePoint;
    public Transform lookTarget;
    public int ammoCost = 1;
    public GameObject meleeProjectile;
    public SpriteRenderer playerSprite;

    private Vector2 direction;
    private Inventory inventory;
    private SpriteRenderer gunSprite;

    private void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
        gunSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        direction = (lookTarget.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (lookTarget.position.x < transform.position.x)
        {
            //on the left
            playerSprite.flipX = true;
            gunSprite.flipY = true;
        }
        else
        {
            //on the right
            playerSprite.flipX = false;
            gunSprite.flipY = false;
        }
    }

    public void Shoot()
    {
        if (inventory.currentBattery.CanShoot(ammoCost) && !inventory.isInventoryOpen)
        {
            GameObject p = Instantiate(projectile, firePoint.position, transform.rotation);
            p.GetComponent<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);

            inventory.currentBattery.UpdateCharge(ammoCost);
            inventory.UpdateUI();
        }
    }
    public void Melee()
    {
        if (!inventory.isInventoryOpen)
        {
            GameObject p = Instantiate(meleeProjectile, firePoint.position, transform.rotation);
            p.GetComponent<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
        }
    }
}
