using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : InputController
{
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }
    public void OnFire(InputValue value)
    {
        CallFireEvent(value.isPressed);
    }
    public void OnBoom(InputValue value)
    {
        CallBoomEvent(true);
    }
}
