using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector3 velocity;
    private Animator animator;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        //animator = GetComponent<Animator>();
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerController.canAct)
        {
            rb.velocity = velocity * moveSpeed;
        }
    }
}