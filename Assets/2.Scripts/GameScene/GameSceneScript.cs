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
    private int currentMana = 0;
    private int maxMana = 100;

    void Start()
    {
        resultPanel.SetActive(false);
        pausePanel.SetActive(false);
        PauseEvent(false);

        PlayerPrefs.SetInt("isMainPanel", 1);

        stageParameter = PlayerPrefs.GetInt("stageParameter", 0);

        if (stageParameter == 0)
        {
            Debug.LogError("stageParamet 에러 : 인자값이 0입니다.");
            return;
        }

        UpdateMana();

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
        
    }

    public void UpdateMana()
    {
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;

        manaText.text = currentMana + "/" + maxMana;
    }

    public void PauseEvent(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0f;
        } 
        else
        {
            Time.timeScale = 1f;
        }
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
    public void CheatBtnClick ()
    {
        pausePanel.SetActive(false);
        WinLose(true);
    }

    //게임 승리 패배 스크립트
    public void WinLose(bool isWin)
    {
        PauseEvent(true);

        int gold = 0;

        switch(stageParameter)
        {
            case 1:
                gold = isWin ? 100 : 10;
                break;

            case 2:
                gold = isWin ? 140 : 10;
                break;

            case 3:
                gold = isWin ? 190 : 20;
                break;

            case 4:
                gold = isWin ? 250 : 20;
                break;

            case 5:
                gold = isWin ? 320 : 30;
                break;

            case 6:
                gold = isWin ? 400 : 40;
                break;
        }

        resultText.text = isWin? "승리!" : "패배.";
        goldText.text = "+" + gold.ToString();

        resultPanel.SetActive(true);

        stageParameter = PlayerPrefs.GetInt("stageParameter", 0);

        PlayerPrefs.SetInt("stage" + stageParameter + "_clear", 1);
        PlayerPrefs.Save();
    }
}
