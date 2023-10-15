using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IController
{
    private MoveVelocity moveVelocity;
    private Vector3 direction;
    public Transform player;
    public bool canAct { get; set; }
    public GameObject projectile;
    public float projectileSpeed;
    public float fireRate = 1.5f;
    public float fireRateRange = 0.5f;
    private float nextFireTime = 0f;

    public int health = 3;

    private void Awake()
    {
        canAct = true;
        moveVelocity = GetComponent<MoveVelocity>();
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        direction = (player.position - transform.position).normalized;
        moveVelocity.SetVelocity(direction);

        if (Time.time > nextFireTime)
        {
            Shoot();

            nextFireTime = Time.time + Random.Range(fireRate - fireRateRange, fireRate + fireRateRange);
        }
    }

    public void Shoot()
    {
        GameObject p = Instantiate(projectile, transform.position, transform.rotation);
        p.GetComponent<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
