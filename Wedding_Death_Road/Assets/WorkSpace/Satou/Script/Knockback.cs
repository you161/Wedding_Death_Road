using UnityEngine;
using UnityEngine.InputSystem;

public class KnockBack : MonoBehaviour
{
    [Header("ノックバック設定")]
    [SerializeField] private float knockbackPower = 10f;

    [Header("判定設定")]
    [SerializeField] private string[] aoeTags = new string[0];
    [SerializeField] private Rigidbody rb = null;

    [Header("スタン設定")]
    [SerializeField] private Stun stun = null;

    public bool IsKnockback { get; private set; } = false;

    private void OnTriggerEnter(Collider other)
    {
        TryApplyKnockback(other.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        TryApplyKnockback(collision.gameObject);
    }

    private void TryApplyKnockback(GameObject other)
    {
        if (other == null || other == gameObject)
        {
            return;
        }

        // スタン中（無敵中）は新たなノックバックを受け付けない
        if (stun != null && stun.IsInvincible)
        {
            return;
        }

        if (!IsMatchTag(other.tag))
        {
            return;
        }
        ApplyKnockback(other.transform.position, knockbackPower);
    }

    private bool IsMatchTag(string tag)
    {
        foreach (var t in aoeTags)
        {
            if (!string.IsNullOrEmpty(t) && tag == t) return true;
        }
        return false;
    }

    // ノックバック処理（Y軸無視、Rigidbody.linearVelocityで直接制御）
    public void ApplyKnockback(Vector3 sourcePosition, float power)
    {
        if (rb == null)
        {
            return;
        }

        Vector3 dir = transform.position - sourcePosition;
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f)
        {
            return;
        }
        dir.Normalize();

        rb.linearVelocity = new Vector3(dir.x * power, rb.linearVelocity.y, dir.z * power);
        transform.rotation = Quaternion.LookRotation(-dir);

        IsKnockback = true;

        if (stun != null)
        {
            stun.ApplyStun();
        }
    }

    public void ResetKnockback()
    {
        IsKnockback = false;
    }
}