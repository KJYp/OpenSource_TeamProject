using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [Header("마나 설정")]
    public int startMana = 0;
    public int currentMana = 0;
    public int maxMana = 100;
    public int regenPerSecond = 1;

    [Header("UI")]
    public TMP_Text manaText;
    public Slider manaSlider;

    private float regenTimer = 0f;

    // 마나 초기화
    private void Start()
    {
        currentMana = startMana;
        UpdateManaUI();
    }

    // 매 프레임마다 마나 회복 처리
    private void Update()
    {
        RegenerateMana();
    }

    // 마나 회복 로직: 초당 regenPerSecond 만큼 회복
    private void RegenerateMana()
    {
        if (currentMana >= maxMana)
        {
            regenTimer = 0f;
            return;
        }

        regenTimer += Time.deltaTime;

        if (regenTimer >= 1f)
        {
            int tick = Mathf.FloorToInt(regenTimer);
            AddMana(regenPerSecond * tick);
            regenTimer -= tick;
        }
    }

    // 마나 획득 함수: amount만큼 마나를 추가하고 UI 업데이트
    public void AddMana(int amount)
    {
        currentMana += amount;

        if (currentMana > maxMana)
            currentMana = maxMana;

        UpdateManaUI();
        Debug.Log($"[ManaManager] 마나 획득: +{amount}, 현재 마나: {currentMana}/{maxMana}");
    }

    // 마나 사용 함수: amount만큼 마나를 사용하고 UI 업데이트, 마나 부족 시 false 반환
    public bool UseMana(int amount)
    {
        if (currentMana < amount)
        {
            Debug.Log($"[ManaManager] 마나 부족! 필요={amount}, 현재={currentMana}");
            return false;
        }

        currentMana -= amount;
        UpdateManaUI();
        Debug.Log($"[ManaManager] 마나 사용: -{amount}, 현재 마나: {currentMana}/{maxMana}");
        return true;
    }

    // 마나 직접 설정 함수: amount로 마나를 설정하고 UI 업데이트
    public void SetMana(int amount)
    {
        currentMana = Mathf.Clamp(amount, 0, maxMana);
        UpdateManaUI();
    }

    private void UpdateManaUI()
    {
        if (manaText != null)
        {
            manaText.text = $"{currentMana} / {maxMana}";
        }

        if (manaSlider != null)
        {
            manaSlider.maxValue = maxMana;
            manaSlider.value = currentMana;
        }
    }
}
