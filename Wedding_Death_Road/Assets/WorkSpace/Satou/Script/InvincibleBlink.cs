using UnityEngine;

public class InvincibleBlink : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] private Stun stun = null;

    [Header("点滅設定")]
    [Tooltip("点滅させたいメッシュ（未設定なら自動で子オブジェクトから取得）")]
    [SerializeField] private Renderer[] targetRenderers = null;

    [Tooltip("1回の点滅（表示⇔非表示）にかかる秒数")]
    [SerializeField] private float blinkInterval = 0.1f;

    private float blinkTimer = 0f;
    private bool isVisible = true;
    private bool wasInvincible = false;

    private void Awake()
    {
        // 未設定なら、自身と子にあるRendererを自動取得
        if (targetRenderers == null || targetRenderers.Length == 0)
        {
            targetRenderers = GetComponentsInChildren<Renderer>();
        }
    }

    private void Update()
    {
        if (stun == null)
        {
            return;
        }
        bool isInvincible = stun.IsInvincible;

        if (isInvincible)
        {
            blinkTimer -= Time.deltaTime;
            if (blinkTimer <= 0f)
            {
                blinkTimer = blinkInterval;
                isVisible = !isVisible;
                SetVisible(isVisible);
            }
        }
        else if (wasInvincible)
        {
            // 無敵が終わった瞬間に必ず表示状態へ戻す
            isVisible = true;
            blinkTimer = 0f;
            SetVisible(true);
        }

        wasInvincible = isInvincible;
    }

    private void SetVisible(bool visible)
    {
        if (targetRenderers == null)
        {
            return;
        }
        foreach (var r in targetRenderers)
        {
            if (r != null)
            {
                r.enabled = visible;
            }
        }
    }
}