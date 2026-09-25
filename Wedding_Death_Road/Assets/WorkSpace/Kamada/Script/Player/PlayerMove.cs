using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private PlayerData playerData = null;
    [SerializeField] private PlayerInputManager playerInputManager = null;

    private Vector3 moveDirection = Vector3.zero;

    private bool isPressed = false;
    private bool isMove = false;

    public bool IsMove { get => isMove; }


    private void Start()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        moveDirection = Vector3.zero;

        isPressed = false;
        isMove = false;
    }

    private void Update()
    {
        MoveInput();
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void MoveInput()
    {
        moveDirection = Vector3.zero;
        isPressed = false;

        Vector2 input = playerInputManager.MoveInput;

        //入力があるか
        if (input.sqrMagnitude > 0.01f)
        {
            isPressed = true;
            moveDirection = new Vector3(input.x,0.0f,input.y);
            moveDirection.Normalize();
        }
    }

    private void Move()
    {
        Vector3 velocity = rb.linearVelocity;

        if (isPressed)
        {
            velocity.x = moveDirection.x * playerData.MoveSpeed;
            velocity.z = moveDirection.z * playerData.MoveSpeed;

            isMove = true;
        }
        else
        {
            velocity.x = 0.0f;
            velocity.z = 0.0f;

            isMove = false;
        }

        rb.linearVelocity = velocity;
    }

    private void Rotate()
    {
        float rotationSpeed = playerData.RotationSpeed;

        if (!isPressed)
        {
            return;
        }

        if (moveDirection.sqrMagnitude <= 0.01f)
        {
            return;
        }

        //移動方向に向ける
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

        ////現在の回転と目標回転の角度
        //float angle = Quaternion.Angle(rb.rotation,targetRotation);

        ////大きく方向転換する場合回転速度を倍に
        //if (angle >= 150.0f)
        //{
        //    rotationSpeed *= 2.0f;
        //}

        //目標方向へ回転
        rb.rotation = Quaternion.RotateTowards(
            rb.rotation,
            targetRotation,
            rotationSpeed *
            Time.fixedDeltaTime
        );
    }
}