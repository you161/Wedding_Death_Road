using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("移動")]
    [Header("移動速度")]
    public float MoveSpeed = 0;
    [Header("回転速度")]
    public float RotationSpeed = 0;
}