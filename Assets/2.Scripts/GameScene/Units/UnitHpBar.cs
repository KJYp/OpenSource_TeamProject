using UnityEngine;

public class UnitHpBar : MonoBehaviour
{
    [SerializeField] private UnitHealth targetHealth;
    [SerializeField] private RectTransform hpFillRect;

    // UnitHealth 컴포넌트를 자동으로 찾아서 할당

    private void Awake()
    {
        if (targetHealth == null)
        {
            targetHealth = GetComponentInParent<UnitHealth>();
        }
    }

    // 매 프레임마다 체력 비율에 따라 HP 바의 크기를 업데이트
    private void Update()
    {
        if (targetHealth == null || hpFillRect == null)
        {
            return;
        }

        float hpRatio = targetHealth.GetHpRatio();

        hpFillRect.localScale = new Vector3(hpRatio, 1f, 1f);
    }
}