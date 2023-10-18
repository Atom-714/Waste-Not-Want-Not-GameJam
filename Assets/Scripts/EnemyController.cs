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
    public int cost;
    public WaveHandler waveHandler;
    private float nextFireTime = 0f;
    private Animator animator;
    private SpriteRenderer sprite;
    public GameObject[] itemDrops;
    public AudioSource audioSource;
    public AudioSource shootSound;

    public int health = 3;

    private void Awake()
    {
        canAct = true;
        moveVelocity = GetComponent<MoveVelocity>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!canAct)
        {
            return;
        }

        direction = (player.position - transform.position).normalized;
        moveVelocity.SetVelocity(direction);

        if (Time.time > nextFireTime)
        {
            Shoot();

            nextFireTime = Time.time + Random.Range(fireRate - fireRateRange, fireRate + fireRateRange);
        }

        if (direction.x > 0)
        {
            sprite.flipX = false;
        } 
        else
        {
            sprite.flipX = true;
        }
    }

    public void Shoot()
    {
        shootSound.Play();
        GameObject p = Instantiate(projectile, transform.position, transform.rotation);
        p.GetComponent<Rigidbody2D>().AddForce(direction.normalized * projectileSpeed, ForceMode2D.Impulse);
    }

    public void Damage(int amount)
    {
        health -= amount;
        
        audioSource.Play();
        if (health <= 0)
        {
            animator.SetTrigger("Die");
            moveVelocity.Stop();
            canAct = false;
            StartCoroutine(DieTimer());
        }
        else
        {
            animator.SetTrigger("Hurt");
        }
    }

    private IEnumerator DieTimer()
    {
        yield return new WaitForSeconds(0.5f);
        Die();
    }
    public void Die()
    {
        waveHandler.spawnedEnemies.Remove(this.gameObject);
        GameObject item = itemDrops[Random.Range(0, itemDrops.Length)];
        if (item != null)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
