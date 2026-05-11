using UnityEngine;

public class UnitHpBar : MonoBehaviour
{
    [SerializeField] private UnitHealth targetHealth;
    [SerializeField] private RectTransform hpFillRect;

    private void Awake()
    {
        if (targetHealth == null)
        {
            targetHealth = GetComponentInParent<UnitHealth>();
        }
    }

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