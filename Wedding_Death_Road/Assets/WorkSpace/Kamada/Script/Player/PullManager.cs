using UnityEngine;

public class PullManager : MonoBehaviour
{
    private PullState currentState = PullState.None;

    private PlayerInputManager initiator;
    private PlayerInputManager responder;

    public PullState CurrentState { get => currentState; }

    public PlayerInputManager Initiator { get => initiator; }
    public PlayerInputManager Responder { get => responder; }

    private void Start()
    {
        ChangeState(PullState.None);
    }

    public void ChangeState(PullState newState)
    {
        currentState = newState;
    }

    public void SetInitiator(PlayerInputManager player)
    {
        initiator = player;
    }

    public void SetResponder(PlayerInputManager player)
    {
        responder = player;
    }

    public void ResetPull()
    {
        initiator = null;
        responder = null;
        currentState = PullState.None;
    }
}