using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("移動")]
    [Header("移動速度")]
    public float MoveSpeed = 0;
    [Header("回転速度")]
    public float RotationSpeed = 0;

    [Header("引っ張り")]
    [Header("引っ張り可能距離")]
    public float pullableDistance = 0.0f;
    [Header("最大待機時間")]
    public float maxWaitingTime = 0.0f;
    [Header("引っ張り中の移動速度")]
    public float PullSpeed = 0;
    [Header("引っ張り停止距離")]
    public float StopDistance = 0;
}