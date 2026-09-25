using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("シーンに配置してあるプレイヤー")]
    [SerializeField] private PlayerInput[] playerObjects = null;

    [Header("スポーン位置")]
    [SerializeField] private Transform[] spawnPoints = null;

    [SerializeField] private int maxPlayerCount = 2;

    private PlayerInput[] players;

    private void Start()
    {
        int playerCount = Mathf.Min(
            maxPlayerCount,
            playerObjects.Length,
            spawnPoints.Length
        );

        players = new PlayerInput[playerCount];

        SetupPlayers(playerCount);
    }

    private void SetupPlayers(int playerCount)
    {
        for (int i = 0; i < playerCount; i++)
        {
            PlayerInput playerInput = playerObjects[i];

            if (playerInput == null)
            {
                continue;
            }

            //プレイヤーを保存
            players[i] = playerInput;

            //プレイヤー番号を設定
            PlayerInputManager controllerInput =
                playerInput.GetComponent<PlayerInputManager>();

            if (controllerInput != null)
            {
                if (i == 0)
                {
                    //P1
                    controllerInput.SetInputType(PlayerInputManager.InputType.WASD);
                }
                else
                {
                    //P2
                    controllerInput.SetInputType(PlayerInputManager.InputType.Arrow);
                }
            }

            //スポーン位置に移動
            playerInput.transform.position = spawnPoints[i].position;
            playerInput.transform.rotation = spawnPoints[i].rotation;
        }
    }
}