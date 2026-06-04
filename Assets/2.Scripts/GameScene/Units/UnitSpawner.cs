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
        TryConnectData();

        if (balanceDatabase == null || upgradeState == null)
        {
            Debug.LogWarning("BalanceDatabase 또는 UpgradeState가 UnitSpawner에 연결되지 않았습니다.");
            return;
        }

        int currentGrade = upgradeState.GetGrade(unitStats.unitType);

        UnitGradeStats data = balanceDatabase.GetStats(unitStats.unitType, currentGrade);

        if (data == null)
        {
            Debug.LogWarning($"{unitStats.unitType} {currentGrade}학년 스탯 데이터를 찾지 못했습니다.");
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

    private void TryConnectData()
    {
        if (balanceDatabase == null)
        {
            balanceDatabase = UnitBalanceDatabase.Instance;
        }

        if (upgradeState == null)
        {
            upgradeState = UnitUpgradeState.Instance;
        }

        if (balanceDatabase == null)
        {
            Debug.LogWarning("UnitSpawner가 UnitBalanceDatabase를 찾지 못했습니다.");
        }

        if (upgradeState == null)
        {
            Debug.LogWarning("UnitSpawner가 UnitUpgradeState를 찾지 못했습니다.");
        }
    }

    private void Awake()
    {
        TryConnectData();
    }

    private void Start()
    {
        TryConnectData();
    }

}