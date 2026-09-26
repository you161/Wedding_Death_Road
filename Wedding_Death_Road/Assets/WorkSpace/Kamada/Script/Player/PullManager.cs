using UnityEngine;

public class PullManager : MonoBehaviour
{
    private PullState currentState = PullState.None;
    private PlayerInputManager initiator = null;
    private PlayerInputManager responder = null;
    private bool[] playerLocked = new bool[2];

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

        for(int i = 0; i < playerLocked.Length; i++)
        {
            SetPlayerLocked(i, false);
        }
    }

    public void SetPlayerLocked(int playerIndex, bool value)
    {
        playerLocked[playerIndex] = value;
    }

    public bool IsPlayerLocked(int playerIndex)
    {
        return playerLocked[playerIndex];
    }
}