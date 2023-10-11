using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float time;
    [SerializeField] private LayerMask dontHitLayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.GetComponent<IController>() != null && other.gameObject.layer != dontHitLayer)
        {
            other.transform.GetComponent<IController>().Damage(damage);
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        StartCoroutine(DestroyProjectile());
    }
    private IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
