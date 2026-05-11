using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneScript : MonoBehaviour
{
    public GameObject pausePanel;

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

    public Slider manaSlider;
    public TMP_Text manaText;
    public TMP_Text stageText;

    private int currentMana = 0;
    private int maxMana = 100;
    void Start()
    {
        pausePanel.SetActive(false);
        PlayerPrefs.SetInt("isMainPanel", 1);

        int stageParameter = PlayerPrefs.GetInt("stageParameter", 0);

        if (stageParameter == 0)
        {
            Debug.LogError("stageParamet 에러 : 인자값이 0입니다.");
            return;
        }

        UpdateStage(stageParameter);
        UpdateMana();

        switch (stageParameter)
        {
            
            case 1:
                backgroundRenderer.sprite = stage1_background;
                enemyBaseRenderer.sprite = stage1_enemyBase;
                break;

            case 2:
                backgroundRenderer.sprite = stage2_background;
                enemyBaseRenderer.sprite = stage2_enemyBase;
                break;

            case 3:
                backgroundRenderer.sprite = stage3_background;
                enemyBaseRenderer.sprite = stage3_enemyBase;
                break;

            case 4:
                backgroundRenderer.sprite = stage4_background;
                enemyBaseRenderer.sprite = stage4_enemyBase;
                break;

            case 5:
                backgroundRenderer.sprite = stage5_background;
                enemyBaseRenderer.sprite = stage5_enemyBase;
                break;
        }
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

    public void UpdateStage(int stage)
    {
        switch (stage)
        {
            case 1:
                stageText.text = "스테이지 1. 기숙사";
                break;

            case 2:
                stageText.text = "스테이지 2. 백년관";
                break;

            case 3:
                stageText.text = "스테이지 3. 자연과학관";
                break;

            case 4:
                stageText.text = "스테이지 4. 공학관";
                break;

            case 5:
                stageText.text = "스테이지 5. 어문학관";
                break;
        }
    }

    //일시정지 버튼
    public void PauseBtnClick()
    {
        pausePanel.SetActive(true);
    }

    //일시정지 계속버튼
    public void CloseBtnClick()
     {
        pausePanel.SetActive(false);
     }

    //일시정지 나가기버튼
    public void EndBtnClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    //일시정지 승리치트버튼
    public void VictoryBtnClick ()
    {
        int stageParameter = PlayerPrefs.GetInt("stageParameter", 0);

        PlayerPrefs.SetInt("stage" + stageParameter + "_clear", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainScene");
    }
}
