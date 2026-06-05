using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    private const string GOLD_KEY = "CurrentGold";

    [Header("현재 보유 골드")]
    [SerializeField] private int currentGold = 0;

    // 외부(UpgradeSceneScript 등)에서 현재 골드를 확인할 때 쓰는 프로퍼티
    public int CurrentGold => currentGold;

    private void Awake()
    {
        // 싱글톤 유지 및 씬 전환 시 파괴 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGold(); // 게임 시작 시 저장된 골드 불러오기
    }

    /// <summary>
    /// 골드 획득 (GameSceneScript에서 승리 시 호출)
    /// </summary>
    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        currentGold += amount;
        SaveGold(); // 획득할 때마다 즉시 저장

        Debug.Log($"골드 획득: +{amount}, 현재 골드: {currentGold}");
    }

    /// <summary>
    /// 골드 사용 (UpgradeSceneScript에서 업그레이드 시 호출)
    /// </summary>
    public bool UseGold(int amount)
    {
        if (amount <= 0) return false;

        // 골드가 충분한지 검사
        if (currentGold < amount)
        {
            Debug.Log($"골드 부족! 필요: {amount}, 현재: {currentGold}");
            return false;
        }

        // 골드 차감 및 저장
        currentGold -= amount;
        SaveGold();

        Debug.Log($"골드 사용: -{amount}, 남은 골드: {currentGold}");
        return true; // 차감 성공 반환
    }

    // 내부 저장소에 골드 기록
    private void SaveGold()
    {
        PlayerPrefs.SetInt(GOLD_KEY, currentGold);
        PlayerPrefs.Save();
    }

    // 내부 저장소에서 골드 불러오기
    private void LoadGold()
    {
        currentGold = PlayerPrefs.GetInt(GOLD_KEY, 0);
    }

    /// <summary>
    /// 테스트용: 인스펙터 우클릭으로 골드 초기화 가능
    /// </summary>
    [ContextMenu("Reset Gold")]
    public void ResetGold()
    {
        currentGold = 0;
        SaveGold();
        Debug.Log("골드가 0으로 초기화되었습니다.");
    }
}
