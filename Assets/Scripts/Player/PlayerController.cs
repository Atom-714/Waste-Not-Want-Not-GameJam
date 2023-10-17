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
    private Animator animator;
    public CameraShake cameraShake;

    public bool canAct { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        canAct = true;
        playerInput = new PlayerInput();
        moveVelocity = GetComponent<MoveVelocity>();
        gun = GetComponentInChildren<Gun>();
        inventory = GetComponent<Inventory>();
        animator = GetComponentInChildren<Animator>();
    }

    public void OnEnable()
    {
        movement = playerInput.Game.Movement;
        movement.Enable();

        //Shoot input
        playerInput.Game.Shoot.performed += Shoot;
        playerInput.Game.Shoot.Enable();

        //Melee input
        playerInput.Game.Melee.performed += Melee;
        playerInput.Game.Melee.Enable();

        //Menu input
        playerInput.Game.OpenMenu.performed += OpenMenu;
        playerInput.Game.OpenMenu.Enable();
    }
    public void OnDisable()
    {
        movement.Disable();
        playerInput.Game.Shoot.Disable();
        playerInput.Game.OpenMenu.Disable();
        playerInput.Game.Melee.Disable();
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
        animator.SetTrigger("Hurt");
        cameraShake.DoCameraShake();
        if (inventory.currentBattery.GetCurrentCharge() == 0 || inventory.currentBattery == null)
        {
            Die();
        }
        else
        {
            inventory.currentBattery.UpdateCharge(amount);
            inventory.UpdateUI();
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
    private void Melee(InputAction.CallbackContext obj)
    {
        if (!canAct)
        {
            return;
        }
        gun.Melee();
    }
    private void OpenMenu(InputAction.CallbackContext obj)
    {
        inventory.OpenMenu();
    }

    public void Die()
    {
        Debug.Log("Die");
    }
}
