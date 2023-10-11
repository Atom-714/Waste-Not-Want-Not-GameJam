using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IController
{
    private PlayerInput playerInput;
    public InputAction movement;
    private Vector2 moveInput;
    private MoveVelocity moveVelocity;
    private Gun gun;

    public bool canAct { get; set; }
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Awake()
    {
        canAct = true;
        playerInput = new PlayerInput();
        moveVelocity = GetComponent<MoveVelocity>();
        gun = GetComponentInChildren<Gun>();
    }

    public void OnEnable()
    {
        movement = playerInput.Game.Movement;
        movement.Enable();

        //Shoot input
        playerInput.Game.Shoot.performed += Shoot;
        playerInput.Game.Shoot.Enable();
    }
    public void OnDisable()
    {
        movement.Disable();
        playerInput.Game.Shoot.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        moveInput = movement.ReadValue<Vector2>().normalized;
        moveVelocity.SetVelocity(moveInput);
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        gun.Shoot();
    }

    public void Die()
    {

    }
}
