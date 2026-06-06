using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    // ★ 추가된 부분: 인스펙터에서 적군 생성기인지 체크할 수 있는 스위치!
    [Header("Spawner Settings")]
    public bool isEnemySpawner = false;

    [Header("Units")]
    public GameObject meleeUnitPrefab;
    public GameObject tankUnitPrefab;
    public GameObject rangedUnitPrefab;
    public GameObject healerUnitPrefab;
    public GameObject damageUnitPrefab;

    [Header("Spawn Point")]
    public Transform allySpawnPoint;

    [Header("Game Scene Script")]
    public GameSceneScript gameSceneScript;

    public void SpawnMelee() // 컴공과 (120)
    {
        if (gameSceneScript != null && !gameSceneScript.UseMana(120)) return;
        SpawnUnit(meleeUnitPrefab);
    }

    public void SpawnTank() // 글스산 (180)
    {
        if (gameSceneScript != null && !gameSceneScript.UseMana(180)) return;
        SpawnUnit(tankUnitPrefab);
    }

    public void SpawnRanged() // 기후학과 (140)
    {
        if (gameSceneScript != null && !gameSceneScript.UseMana(140)) return;
        SpawnUnit(rangedUnitPrefab);
    }

    public void SpawnHealer() // 통번역학과 (170)
    {
        if (gameSceneScript != null && !gameSceneScript.UseMana(170)) return;
        SpawnUnit(healerUnitPrefab);
    }

    public void SpawnDamage() // 화학과 (155)
    {
        if (gameSceneScript != null && !gameSceneScript.UseMana(155)) return;
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

    public void SpawnUnit(GameObject prefab, int grade)
    {
        if (prefab == null || allySpawnPoint == null) return;

        GameObject unitObject = Instantiate(prefab, allySpawnPoint.position, Quaternion.identity);
        UnitStats unitStats = unitObject.GetComponent<UnitStats>();

        if (unitStats == null) return;

        ApplyGradeStats(unitStats, grade);
    }

    [Header("Data")]
    public UnitBalanceDatabase balanceDatabase;
    public UnitUpgradeState upgradeState;

    private void ApplyUpgradeStats(UnitStats unitStats)
    {
        TryConnectData();

        if (balanceDatabase == null || upgradeState == null) return;

        int currentGrade = upgradeState.GetGrade(unitStats.unitType);

        // ★ 적군 생성기(isEnemySpawner가 체크됨)일 때만 빨간색 로그 출력!
        if (isEnemySpawner)
        {
            Debug.Log($"<color=red>▶ {unitStats.unitType} 유닛 생성 (학년 : {currentGrade})</color>");
        }

        UnitGradeStats data = balanceDatabase.GetStats(unitStats.unitType, currentGrade);

        if (data == null) return;

        unitStats.ApplyBalanceData(data);

        UnitHealth unitHealth = unitStats.GetComponent<UnitHealth>();
        if (unitHealth != null) unitHealth.ResetHealthToMax();
    }

    private void ApplyGradeStats(UnitStats unitStats, int grade)
    {
        TryConnectData();

        if (balanceDatabase == null) return;

        // ★ 적군 생성기(isEnemySpawner가 체크됨)일 때만 빨간색 로그 출력!
        if (isEnemySpawner)
        {
            Debug.Log($"<color=red>▶ {unitStats.unitType} 유닛 생성 (학년 : {grade})</color>");
        }

        UnitGradeStats data = balanceDatabase.GetStats(unitStats.unitType, grade);

        if (data == null) return;

        unitStats.ApplyBalanceData(data);

        UnitHealth unitHealth = unitStats.GetComponent<UnitHealth>();
        if (unitHealth != null) unitHealth.ResetHealthToMax();
    }

    private void TryConnectData()
    {
        if (balanceDatabase == null) balanceDatabase = UnitBalanceDatabase.Instance;
        if (upgradeState == null) upgradeState = UnitUpgradeState.Instance;
    }

    private void Awake()
    {
        TryConnectData();
        if (gameSceneScript == null) gameSceneScript = FindAnyObjectByType<GameSceneScript>();
    }

    private void Start()
    {
        TryConnectData();
    }
}
