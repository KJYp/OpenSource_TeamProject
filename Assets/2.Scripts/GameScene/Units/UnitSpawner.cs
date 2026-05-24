using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [Header("Units")]
    public GameObject meleeUnitPrefab;
    public GameObject tankUnitPrefab;
    public GameObject rangedUnitPrefab;
    public GameObject healerUnitPrefab;
    public GameObject damageUnitPrefab;

    [Header("Spawn Point")]
    public Transform allySpawnPoint;

    public void SpawnMelee()
    {
        SpawnUnit(meleeUnitPrefab);
    }

    public void SpawnTank()
    {
        SpawnUnit(tankUnitPrefab);
    }

    public void SpawnRanged()
    {
        SpawnUnit(rangedUnitPrefab);
    }

    public void SpawnHealer()
    {
        SpawnUnit(healerUnitPrefab);
    }

    public void SpawnDamage()
    {
        SpawnUnit(damageUnitPrefab);
    }

    public void SpawnUnit(GameObject prefab)
    {
        if (prefab == null || allySpawnPoint == null)
        {
            Debug.LogWarning("Prefab 또는 SpawnPoint가 비어 있음");
            return;
        }

        GameObject unitObject = Instantiate(prefab, allySpawnPoint.position, Quaternion.identity);

        UnitStats unitStats = unitObject.GetComponent<UnitStats>();

        if (unitStats == null)
        {
            Debug.LogWarning($"{prefab.name}에 UnitStats가 없습니다.");
            return;
        }

        ApplyUpgradeStats(unitStats);
    }

    [Header("Data")]
    public UnitBalanceDatabase balanceDatabase;
    public UnitUpgradeState upgradeState;

    private void ApplyUpgradeStats(UnitStats unitStats)
    {
        if (balanceDatabase == null || upgradeState == null)
        {
            Debug.LogWarning("BalanceDatabase 또는 UpgradeState가 UnitSpawner에 연결되지 않았습니다.");
            return;
        }

        int currentGrade = upgradeState.GetGrade(unitStats.unitType);

        UnitGradeStats data = balanceDatabase.GetStats(unitStats.unitType, currentGrade);

        if (data == null)
        {
            return;
        }

        unitStats.ApplyBalanceData(data);

        UnitHealth unitHealth = unitStats.GetComponent<UnitHealth>();

        if (unitHealth != null)
        {
            unitHealth.ResetHealthToMax();
        }

        Debug.Log($"{unitStats.unitType} {currentGrade}학년 스탯 적용 완료");
    }


}