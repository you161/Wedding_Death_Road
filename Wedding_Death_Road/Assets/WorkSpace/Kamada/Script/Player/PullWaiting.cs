using UnityEngine;

public class PullWaiting : MonoBehaviour
{
    [SerializeField] private PullManager pullManager = null;
    [SerializeField] private PlayerInputManager[] playerInputManager = null;
    [SerializeField] private Rigidbody[] playerRb = null;
    [SerializeField] private PlayerData playerData = null;
    private float timer = 0.0f;

    private void Update()
    {
        //ステートがNoneなら
        if (pullManager.CurrentState == PullState.None)
        {
            for(int i = 0; i < playerInputManager.Length; i++)
            {
                if (playerInputManager[i].PullPressed)
                {
                    SetWaiting(i);
                }
            }
        }

        //待機時間カウント
        CountWaitingTime();
    }

    private void SetWaiting(int playerNumber)
    {
        if (pullManager.CurrentState != PullState.None)
        {
            return;
        }

        float distance = Vector3.Distance(playerRb[0].position, playerRb[1].position);
        Debug.Log(distance);

        if(distance < playerData.pullableDistance)
        {
            return;
        }

        pullManager.ChangeState(PullState.Ready);
        pullManager.SetInitiator(playerInputManager[playerNumber]);
        timer = 0;
    }

    private void CountWaitingTime()
    {
        if (pullManager.CurrentState != PullState.Ready)
        {
            return;
        }

        timer += Time.deltaTime;
        
        if(timer >= playerData.maxWaitingTime)
        {
            timer = 0;
            pullManager.ResetPull();
        }
    }
}