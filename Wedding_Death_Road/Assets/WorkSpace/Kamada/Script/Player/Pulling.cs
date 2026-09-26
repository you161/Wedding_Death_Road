using UnityEngine;

public class Pulling : MonoBehaviour
{
    [SerializeField] private PullManager pullManager = null;
    [SerializeField] private Rigidbody[] playerRb = null;
    [SerializeField] private PlayerData playerData = null;

    private void FixedUpdate()
    {
        UpdatePullMovement();
    }

    private void UpdatePullMovement()
    {
        if (pullManager.CurrentState != PullState.Pulling)
        {
            return;
        }

        if (pullManager.Initiator == null || pullManager.Responder == null)
        {
            return;
        }

        int initiator = pullManager.Initiator.PlayerIndex;
        int responder = pullManager.Responder.PlayerIndex;

        Vector3 direction = playerRb[responder].position - playerRb[initiator].position;
        direction.Normalize();

        Vector3 linerVelocity = playerRb[initiator].linearVelocity;
        linerVelocity.x = playerData.PullSpeed * direction.x;
        linerVelocity.z = playerData.PullSpeed * direction.z;

        float distance = Vector3.Distance(playerRb[initiator].position, playerRb[responder].position);

        if (distance <= playerData.StopDistance)
        {
            linerVelocity.x = 0.0f;
            linerVelocity.z = 0.0f;

            pullManager.ResetPull();
        }

        playerRb[initiator].linearVelocity = linerVelocity;
    }
}