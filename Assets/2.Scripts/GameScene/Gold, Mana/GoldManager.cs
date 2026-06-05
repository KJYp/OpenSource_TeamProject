using System;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    private const string GOLD_KEY = "CurrentGold";

    [Header("골드 UI")]
    [SerializeField] private TMP_Text goldText;

    [Header("현재 골드")]
    [SerializeField] private int currentGold = 0;

    public int CurrentGold => currentGold;

    // 골드 변경 시 다른 UI나 시스템이 구독해서 사용할 수 있음
    public event Action<int> OnGoldChanged;

    public enum StageType
    {
        Tutorial,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
    }

    [Serializable]
    public class StageGoldReward
    {
        public StageType stageType;
        public int clearGold;
        public int firstClearBonusGold;
    }

    [Header("스테이지별 골드 보상")]
    [SerializeField]
    private StageGoldReward[] stageRewards =
    {
        new StageGoldReward { stageType = StageType.Tutorial, clearGold = 100, firstClearBonusGold = 50 },
        new StageGoldReward { stageType = StageType.Stage1, clearGold = 140, firstClearBonusGold = 80 },
        new StageGoldReward { stageType = StageType.Stage2, clearGold = 190, firstClearBonusGold = 100 },
        new StageGoldReward { stageType = StageType.Stage3, clearGold = 250, firstClearBonusGold = 130 },
        new StageGoldReward { stageType = StageType.Stage4, clearGold = 320, firstClearBonusGold = 160 },
        new StageGoldReward { stageType = StageType.Stage5, clearGold = 400, firstClearBonusGold = 200 }
    };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadGold();
    }

    private void Start()
    {
        UpdateGoldUI();
        OnGoldChanged?.Invoke(currentGold);
    }

    /// <summary>
    /// 일반 골드 획득
    /// </summary>
    public void AddGold(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning($"잘못된 골드 지급 요청: {amount}");
            return;
        }

        currentGold += amount;
        SaveGold();
        NotifyGoldChanged();

        Debug.Log($"골드 획득: +{amount}, 현재 골드: {currentGold}");
    }

    /// <summary>
    /// 업그레이드 등에서 골드 사용
    /// </summary>
    public bool UseGold(int amount)
    {
        if (amount <= 0)
        {
            Debug.LogWarning($"잘못된 골드 사용 요청: {amount}");
            return false;
        }

        if (currentGold < amount)
        {
            Debug.Log($"골드 부족! 필요: {amount}, 현재: {currentGold}");
            return false;
        }

        currentGold -= amount;
        SaveGold();
        NotifyGoldChanged();

        Debug.Log($"골드 사용: -{amount}, 현재 골드: {currentGold}");
        return true;
    }

    /// <summary>
    /// 스테이지 클리어 시 보상 지급
    /// 첫 클리어면 추가 보상까지 지급
    /// </summary>
    public int RewardStageClear(StageType stageType)
    {
        StageGoldReward rewardData = GetStageReward(stageType);

        if (rewardData == null)
        {
            Debug.LogWarning($"{stageType} 보상 데이터가 없습니다.");
            return 0;
        }

        int reward = rewardData.clearGold;
        bool isFirstClear = !IsStageCleared(stageType);

        if (isFirstClear)
        {
            reward += rewardData.firstClearBonusGold;
            SetStageCleared(stageType, true);
        }

        AddGold(reward);

        Debug.Log(
            isFirstClear
                ? $"{stageType} 첫 클리어! 기본 보상 {rewardData.clearGold} + 첫 클리어 보너스 {rewardData.firstClearBonusGold} = 총 {reward}"
                : $"{stageType} 클리어! 기본 보상 {rewardData.clearGold} 지급"
        );

        return reward;
    }

    /// <summary>
    /// 해당 스테이지의 기본/첫클리어 보상 데이터 조회
    /// </summary>
    public StageGoldReward GetStageReward(StageType stageType)
    {
        foreach (var reward in stageRewards)
        {
            if (reward.stageType == stageType)
                return reward;
        }

        return null;
    }

    /// <summary>
    /// 첫 클리어 여부 확인
    /// </summary>
    public bool IsStageCleared(StageType stageType)
    {
        return PlayerPrefs.GetInt(GetStageClearKey(stageType), 0) == 1;
    }

    /// <summary>
    /// 특정 스테이지 클리어 여부 저장
    /// </summary>
    private void SetStageCleared(StageType stageType, bool cleared)
    {
        PlayerPrefs.SetInt(GetStageClearKey(stageType), cleared ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// UI 텍스트 동적 연결
    /// </summary>
    public void SetGoldText(TMP_Text targetText)
    {
        goldText = targetText;
        UpdateGoldUI();
    }

    public void UpdateGoldUI()
    {
        if (goldText == null)
            return;

        goldText.text = currentGold.ToString();
    }

    private void NotifyGoldChanged()
    {
        UpdateGoldUI();
        OnGoldChanged?.Invoke(currentGold);
    }

    private void SaveGold()
    {
        PlayerPrefs.SetInt(GOLD_KEY, currentGold);
        PlayerPrefs.Save();
    }

    private void LoadGold()
    {
        currentGold = PlayerPrefs.GetInt(GOLD_KEY, 0);
    }

    private string GetStageClearKey(StageType stageType)
    {
        return $"StageCleared_{stageType}";
    }

    /// <summary>
    /// 테스트용: 골드 초기화
    /// </summary>
    [ContextMenu("Reset Gold")]
    public void ResetGold()
    {
        currentGold = 0;
        SaveGold();
        NotifyGoldChanged();
        Debug.Log("골드가 0으로 초기화되었습니다.");
    }

    /// <summary>
    /// 테스트용: 스테이지 첫 클리어 기록 초기화
    /// </summary>
    [ContextMenu("Reset Stage Clear Data")]
    public void ResetStageClearData()
    {
        foreach (StageType stageType in Enum.GetValues(typeof(StageType)))
        {
            PlayerPrefs.DeleteKey(GetStageClearKey(stageType));
        }

        PlayerPrefs.Save();
        Debug.Log("스테이지 클리어 기록이 초기화되었습니다.");
    }
}
