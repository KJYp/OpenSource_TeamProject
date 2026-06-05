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

    private void Start()
    {
        currentMana = startMana;
        UpdateManaUI();
    }

    private void Update()
    {
        RegenerateMana();
    }

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

    public void AddMana(int amount)
    {
        currentMana += amount;

        if (currentMana > maxMana)
            currentMana = maxMana;

        UpdateManaUI();
        Debug.Log($"[ManaManager] 마나 획득: +{amount}, 현재 마나: {currentMana}/{maxMana}");
    }

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
