using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<bool> OnFireEvent;
    public event Action<bool> OnBoomEvent;
    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }
    public void CallFireEvent(bool isPressed)
    {
        OnFireEvent?.Invoke(isPressed);
    }
    public void CallBoomEvent(bool isPressed)
    {
        OnBoomEvent?.Invoke(isPressed);
    }
}
