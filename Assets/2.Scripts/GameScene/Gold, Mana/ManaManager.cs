using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [Header("마나 설정")]
    public int currentMana = 0;
    public int maxMana = 100;
    public int regenPerSecond = 1;

    private float regenTimer = 0f;

    private void Update()
    {
        RegenerateMana();
    }

    private void RegenerateMana()
    {
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
        Debug.Log($"[ManaManager] 마나 사용: -{amount}, 현재 마나: {currentMana}/{maxMana}");
        return true;
    }
}
