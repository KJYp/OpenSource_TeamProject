using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneScript : MonoBehaviour
{
    public StageSpawnScript stageSpawnScript;

    public GameObject pausePanel;
    public GameObject resultPanel;

    public SpriteRenderer backgroundRenderer;
    public SpriteRenderer enemyBaseRenderer;

    public Sprite stage1_background;
    public Sprite stage1_enemyBase;
    public Sprite stage2_background;
    public Sprite stage2_enemyBase;
    public Sprite stage3_background;
    public Sprite stage3_enemyBase;
    public Sprite stage4_background;
    public Sprite stage4_enemyBase;
    public Sprite stage5_background;
    public Sprite stage5_enemyBase;
    public Sprite stage6_background;
    public Sprite stage6_enemyBase;

    public Slider manaSlider;
    public TMP_Text manaText;
    public TMP_Text stageText;

    public TMP_Text resultText;
    public TMP_Text goldText;

    public int stageParameter = 0;

    // [마나 관련 변수]
    [Header("마나 시스템")]
    public float currentMana = 0; // 초당 회복을 위해 float으로 관리
    private int maxMana = 100; //   최대 마나
    private int manaRegenRate = 0; // 초당 마나 회복량
    private float manaRegenTimer = 0f; // 마나 회복 타이머

    void Start()
    {
        resultPanel.SetActive(false);
        pausePanel.SetActive(false);
        PauseEvent(false);

        PlayerPrefs.SetInt("isMainPanel", 1);
        stageParameter = PlayerPrefs.GetInt("stageParameter", 0);

        if (stageParameter == 0)
        {
            Debug.LogError("stageParameter 에러 : 인자값이 0입니다.");
            return;
        }

        // 스테이지별 마나 세팅
        SetStageMana(stageParameter);

        switch (stageParameter)
        {
            case 1:
                stageText.text = "기숙사 스테이지";
                backgroundRenderer.sprite = stage1_background;
                enemyBaseRenderer.sprite = stage1_enemyBase;
                break;
            case 2:
                stageText.text = "백년관 스테이지";
                backgroundRenderer.sprite = stage2_background;
                enemyBaseRenderer.sprite = stage2_enemyBase;
                break;
            case 3:
                stageText.text = "자연과학관 스테이지";
                backgroundRenderer.sprite = stage3_background;
                enemyBaseRenderer.sprite = stage3_enemyBase;
                break;
            case 4:
                stageText.text = "공학관 스테이지";
                backgroundRenderer.sprite = stage4_background;
                enemyBaseRenderer.sprite = stage4_enemyBase;
                break;
            case 5:
                stageText.text = "교양관 스테이지";
                backgroundRenderer.sprite = stage5_background;
                enemyBaseRenderer.sprite = stage5_enemyBase;
                break;
            case 6:
                stageText.text = "어문학관 스테이지";
                backgroundRenderer.sprite = stage6_background;
                enemyBaseRenderer.sprite = stage6_enemyBase;
                break;
        }

        stageSpawnScript.SpawnStageUnit(stageParameter);
    }

    void Update()
    {
        // 결과 패널이나 일시정지가 아닐 때만 마나 회복
        if (Time.timeScale > 0 && currentMana < maxMana)
        {
            // 2. 1초마다 마나 회복
            manaRegenTimer += Time.deltaTime;
            if (manaRegenTimer >= 1f)
            {
                currentMana += manaRegenRate;
                if (currentMana > maxMana) currentMana = maxMana; // 최대치 초과 방지

                manaRegenTimer = 0f;
                UpdateMana(); // UI 갱신
            }
        }
    }

    // 스테이지별 시작 마나, 최대 마나, 초당 회복량 설정 함수
    private void SetStageMana(int stageParam)
    {
        switch (stageParam)
        {
            case 1: // 튜토리얼(기숙사)
                currentMana = 300; maxMana = 600; manaRegenRate = 25; break;
            case 2: // 1단계(백년관)
                currentMana = 350; maxMana = 700; manaRegenRate = 29; break;
            case 3: // 2단계(자연과학관)
                currentMana = 400; maxMana = 800; manaRegenRate = 33; break;
            case 4: // 3단계(공학관)
                currentMana = 450; maxMana = 900; manaRegenRate = 37; break;
            case 5: // 4단계(교양관)
                currentMana = 500; maxMana = 1000; manaRegenRate = 41; break;
            case 6: // 5단계(어문관)
                currentMana = 550; maxMana = 1100; manaRegenRate = 45; break;
        }
        UpdateMana();
    }

    // 유닛 소환시 마나 소모
    public bool UseMana(int cost)
    {
        if (currentMana >= cost)
        {
            currentMana -= cost;
            UpdateMana();
            return true; // 마나 소비 성공 (유닛 소환)
        }
        return false; // 마나 부족 (소환 실패)
    }

    // 적 처치 시 마나 획득
    public void AddMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana) currentMana = maxMana;
        UpdateMana();
    }

    public void UpdateMana()
    {
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
        manaText.text = (int)currentMana + " / " + maxMana; // float이므로 int로 형변환해서 깔끔하게 표시
    }

    public void PauseEvent(bool isPause)
    {
        Time.timeScale = isPause ? 0f : 1f;
    }

    //일시정지 버튼
    public void PauseBtnClick()
    {
        PauseEvent(true);
        pausePanel.SetActive(true);
    }

    //일시정지 계속버튼
    public void CloseBtnClick()
    {
        PauseEvent(false);
        pausePanel.SetActive(false);
    }

    //일시정지 나가기버튼
    public void EndBtnClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    //일시정지 승리치트버튼
    public void CheatBtnClick()
    {
        pausePanel.SetActive(false);
        WinLose(true);
    }

    //게임 승리 패배 스크립트 (이전과 동일)
    public void WinLose(bool isWin)
    {
        PauseEvent(true);

        int gold = 0;
        int baseGold = 0;
        int firstClearBonus = 0;
        int loseGold = 0;

        switch (stageParameter)
        {
            case 1: baseGold = 100; firstClearBonus = 50; loseGold = 10; break;
            case 2: baseGold = 140; firstClearBonus = 80; loseGold = 10; break;
            case 3: baseGold = 190; firstClearBonus = 100; loseGold = 20; break;
            case 4: baseGold = 250; firstClearBonus = 130; loseGold = 20; break;
            case 5: baseGold = 320; firstClearBonus = 160; loseGold = 30; break;
            case 6: baseGold = 400; firstClearBonus = 200; loseGold = 40; break;
        }

        if (isWin)
        {
            int isCleared = PlayerPrefs.GetInt("stage" + stageParameter + "_clear", 0);

            if (isCleared == 0)
            {
                gold = baseGold + firstClearBonus;
                PlayerPrefs.SetInt("stage" + stageParameter + "_clear", 1);
                PlayerPrefs.Save();
            }
            else
            {
                gold = baseGold;
            }
        }
        else
        {
            gold = loseGold;
        }

        resultText.text = isWin ? "승리!" : "패배.";
        goldText.text = "+" + gold.ToString();

        resultPanel.SetActive(true);

        if (GoldManager.Instance != null)
        {
            GoldManager.Instance.AddGold(gold);
        }
    }
}
