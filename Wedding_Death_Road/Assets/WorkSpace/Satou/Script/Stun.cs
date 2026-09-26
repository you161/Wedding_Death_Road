using UnityEngine;

public class Stun : MonoBehaviour
{
    [Header("スタン設定")]
    [SerializeField] private float stunDuration = 1.0f;

    [Header("無敵設定")]
    [Tooltip("スタン終了後、さらに無敵を継続する秒数")]
    [SerializeField] private float extraInvincibleDuration = 0.5f;

    [Header("スタン中に無効化するスクリプト")]
    [Tooltip("プレイヤーの移動・入力スクリプトなどをここに登録")]
    [SerializeField] private MonoBehaviour[] disableWhileStunned = null;

    public bool IsStunned { get; private set; } = false;

    // スタン中 or スタン後の余韻無敵中はtrue
    public bool IsInvincible { get; private set; } = false;

    private float stunTimer = 0f;
    private float invincibleTimer = 0f;

    private void Update()
    {
        // スタン処理
        if (IsStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                EndStun();
            }
        }

        // 無敵処理（スタン終了後の余韻を含む）
        if (IsInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0f)
            {
                IsInvincible = false;
            }
        }
    }

    // 外部（Knockbackなど）から呼び出してスタン開始
    public void ApplyStun(float duration)
    {
        stunTimer = duration;

        // スタン中は無敵タイマーも常に更新
        invincibleTimer = duration + extraInvincibleDuration;
        IsInvincible = true;

        if (!IsStunned)
        {
            IsStunned = true;
            SetScriptsEnabled(false);
        }
    }

    // デフォルトの秒数でスタン開始したい場合用
    public void ApplyStun()
    {
        ApplyStun(stunDuration);
    }

    private void EndStun()
    {
        IsStunned = false;
        SetScriptsEnabled(true);
    }

    private void SetScriptsEnabled(bool enabled)
    {
        if (disableWhileStunned == null) return;

        foreach (var script in disableWhileStunned)
        {
            if (script != null)
            {
                script.enabled = enabled;
            }
        }
    }
}