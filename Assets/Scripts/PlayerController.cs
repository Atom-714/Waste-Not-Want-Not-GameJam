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
    private Inventory inventory;

    public bool canAct { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        canAct = true;
        playerInput = new PlayerInput();
        moveVelocity = GetComponent<MoveVelocity>();
        gun = GetComponentInChildren<Gun>();
        inventory = GetComponent<Inventory>();
    }

    public void OnEnable()
    {
        movement = playerInput.Game.Movement;
        movement.Enable();

        //Shoot input
        playerInput.Game.Shoot.performed += Shoot;
        playerInput.Game.Shoot.Enable();

        //Menu input
        playerInput.Game.OpenMenu.performed += OpenMenu;
        playerInput.Game.OpenMenu.Enable();
    }
    public void OnDisable()
    {
        movement.Disable();
        playerInput.Game.Shoot.Disable();
        playerInput.Game.OpenMenu.Disable();
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
        if (inventory.currentBattery.GetCurrentCharge() == 0 || inventory.currentBattery == null)
        {
            Die();
        }
        else
        {
            inventory.currentBattery.UpdateCharge(amount);
        }
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if (!canAct || inventory.currentBattery == null)
        {
            return;
        }
        gun.Shoot();
    }
    private void OpenMenu(InputAction.CallbackContext obj)
    {
        Debug.Log("Menu");
        inventory.OpenMenu();
    }

    public void Die()
    {

    }
}
