using System;
using UnityEngine.InputSystem;

public class InputManager
{
    private PlayerControls playerControls;

    public float Movement => playerControls.Gameplay.Movement.ReadValue<float>();

    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnSpit;

    public InputManager()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Enable();

        playerControls.Gameplay.Jump.performed += OnJumpPerformed;
        playerControls.Gameplay.Attack.performed += OnAttackPerformed;
        playerControls.Gameplay.Spit.performed += OnSpitPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

     private void OnAttackPerformed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke();
    }

    private void OnSpitPerformed(InputAction.CallbackContext obj)
    {
        OnSpit?.Invoke();
    }

    public void DisablePlayerInput() => playerControls.Gameplay.Disable();


}
