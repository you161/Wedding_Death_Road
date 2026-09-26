using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public enum InputType
    {
        GamePad,
        WASD,
        Arrow
    }

    [SerializeField] private PlayerInput playerInput = null;

    private InputAction moveAction = null;
    private InputAction pullAction = null;
    public int PlayerIndex { get => playerInput.playerIndex; }

    public void SetInputType(InputType inputType)
    {
        if (playerInput == null)
        {
            Debug.LogError("PlayerInputがnullです");
            return;
        }

        switch (inputType)
        {
            case InputType.GamePad:
                moveAction = playerInput.actions["MoveGamePad"];
                pullAction = playerInput.actions["PullGamePad"];
                break;

            case InputType.WASD:
                moveAction = playerInput.actions["MoveWASD"];
                pullAction = playerInput.actions["PullKeyboard_0"];
                break;

            case InputType.Arrow:
                moveAction = playerInput.actions["MoveArrow"];
                pullAction = playerInput.actions["PullKeyboard_1"];
                break;
        }
    }

    public Vector2 MoveInput
    {
        get
        {
            return moveAction.ReadValue<Vector2>();
        }
    }

    public bool PullPressed
    {
        get
        {
            return pullAction.WasPressedThisFrame();
        }
    }
}