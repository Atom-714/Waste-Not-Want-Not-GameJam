using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed = 1;
    public Transform firePoint;

    private Vector3 mousePosition;
    private Vector2 direction;
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void Shoot()
    {
        GameObject p = Instantiate(projectile, firePoint.position, transform.rotation);
        p.GetComponent<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
    }
}
