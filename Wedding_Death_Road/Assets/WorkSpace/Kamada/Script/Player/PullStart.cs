using UnityEngine;

public class PullStart : MonoBehaviour
{
    [SerializeField] private PullManager pullManager;
    [SerializeField] private PlayerInputManager[] playerInputManager = null;

    private void Update()
    {
        if(pullManager.CurrentState == PullState.Ready && pullManager.Initiator != null)
        {
            for (int i = 0; i < playerInputManager.Length; i++)
            {
                if(pullManager.Initiator == playerInputManager[i])
                {
                    continue;
                }

                if (playerInputManager[i].PullPressed)
                {
                    StartPull(i);
                }
            }
        }
    }

    private void StartPull(int playerNumber)
    {
        pullManager.ChangeState(PullState.Pulling);
        pullManager.SetResponder(playerInputManager[playerNumber]);
    }
}