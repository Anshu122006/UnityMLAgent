using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private PlayerInputActions inputActions;

    private void Awake() {
        Instance = this;
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public bool IsMovePressed()
    {
        return inputActions.Player.Move.ReadValue<float>() > 0;
    }

    public bool IsSprintPressed()
    {
        return inputActions.Player.Sprint.ReadValue<float>() > 0;
    }

    public Vector2 GetMouseDelta()
    {
        Vector2 turn = inputActions.Player.MouseDelta.ReadValue<Vector2>();
        if (turn != Vector2.zero)
            return turn.normalized;

        if (inputActions.Player.LeftTurn.ReadValue<float>() > 0)
            return Vector2.left;
        else if (inputActions.Player.RightTurn.ReadValue<float>() > 0)
            return Vector2.right;
        else
            return Vector2.zero;
    }
}
