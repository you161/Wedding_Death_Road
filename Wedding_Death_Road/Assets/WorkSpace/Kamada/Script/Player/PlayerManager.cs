using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [Header("シーンに配置してあるプレイヤー")]
    [SerializeField] private int maxPlayerCount = 2;
    [SerializeField] private PlayerInput[] playerObjects = null;
    [SerializeField] private PlayerMove[] playerMoves = null;

    private PlayerInput[] players = null;

    private void Start()
    {
        int playerCount = Mathf.Min(maxPlayerCount, playerObjects.Length);
        players = new PlayerInput[playerCount];

        SetupPlayers(playerCount);
    }

    private void SetupPlayers(int playerCount)
    {
        //接続されているゲームパッドの数
        int gamepadCount = Gamepad.all.Count;

        for (int i = 0; i < playerCount; i++)
        {
            PlayerInput playerInput = playerObjects[i];

            if (playerInput == null)
            {
                continue;
            }

            //プレイヤーを保存
            players[i] = playerInput;

            if (playerMoves[i] != null)
            {
                playerMoves[i].SetPlayerIndex(i);
            }

            //入力設定
            if (playerInput.TryGetComponent<PlayerInputManager>(out var controllerInput))
            {
                if (i < gamepadCount)
                {
                    //ゲームパッドがある場合
                    controllerInput.SetInputType(PlayerInputManager.InputType.GamePad);
                }
                else
                {
                    //ゲームパッドがない場合
                    if (i == 0)
                    {
                        //P1→WASD
                        controllerInput.SetInputType(PlayerInputManager.InputType.WASD);
                    }
                    else
                    {
                        //P2→矢印キー
                        controllerInput.SetInputType(PlayerInputManager.InputType.Arrow);
                    }
                }
            }
        }
    }
}