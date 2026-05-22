using UnityEngine;

public class BaseHpBar : MonoBehaviour
{
    [SerializeField] private baseScript targetBase;
    [SerializeField] private RectTransform hpFillRect;

    private void Awake()
    {
        if (targetBase == null)
        {
            targetBase = GetComponentInParent<baseScript>();
        }
    }

    private void Update()
    {
        if (targetBase == null || hpFillRect == null)
        {
            return;
        }

        float hpRatio =
            targetBase.currentHp /
            targetBase.maxHp;

        hpFillRect.localScale =
            new Vector3(hpRatio, 1f, 1f);
    }
}