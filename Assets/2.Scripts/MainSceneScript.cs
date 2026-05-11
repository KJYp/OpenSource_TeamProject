using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneScript : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject tutorialPanel;
    public GameObject mainPanel;
    public GameObject endPanel;
    public GameObject popupPanel;

    public Image tutorialImage;
    public Sprite[] tutorialSprite;

    public TMP_Text stageTitleText;
    public TMP_Text stageDescriptionText;

    private bool tutorialPlaying = false;
    private int tutorialIndex = 0;
    private int stageParameter = 0;

    void Start()
    {
        int isMainPanel = PlayerPrefs.GetInt("isMainPanel", 0);

        if (isMainPanel == 0)
        {
            startPanel.SetActive(true);
            tutorialPanel.SetActive(false);
            mainPanel.SetActive(false);
            endPanel.SetActive(false);
        }
        else
        {
            if (IsClearGame())
            {
                startPanel.SetActive(false);
                tutorialPanel.SetActive(false);
                mainPanel.SetActive(false);
                endPanel.SetActive(true);
            }
            else
            {
                startPanel.SetActive(false);
                tutorialPanel.SetActive(false);
                mainPanel.SetActive(true);
                endPanel.SetActive(false);
            }
        }

        popupPanel.SetActive(false);
    }

    void Update()
    {
        if (tutorialPlaying && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TutorialNext();
        }
    }

    //게임 엔딩조건 확인
    public bool IsClearGame()
    {
        bool stageClear_1 = PlayerPrefs.GetInt("stage1_clear", 0) == 1;
        bool stageClear_2 = PlayerPrefs.GetInt("stage2_clear", 0) == 1;
        bool stageClear_3 = PlayerPrefs.GetInt("stage3_clear", 0) == 1;
        bool stageClear_4 = PlayerPrefs.GetInt("stage4_clear", 0) == 1;
        bool stageClear_5 = PlayerPrefs.GetInt("stage5_clear", 0) == 1;

        return stageClear_1 && stageClear_2 && stageClear_3 && stageClear_4 && stageClear_5;
    }

    //startPanel 시작하기 버튼
    public void StartBtnClick()
    {
        startPanel.SetActive(false);
        tutorialPanel.SetActive(true);

        tutorialIndex = 0;
        tutorialImage.sprite = tutorialSprite[0];
        tutorialPlaying = true;
    }

    //tutorialPanel 장면 넘기기
    public void TutorialNext()
    {
        tutorialIndex++;

        if (tutorialIndex < tutorialSprite.Length)
        {
            tutorialImage.sprite = tutorialSprite[tutorialIndex];
        }
        else
        {
            tutorialPanel.SetActive(false);
            mainPanel.SetActive(true);

            tutorialPlaying = false;
        }
    }

    //mainPanel 건물 버튼
    public void StageBtnClick(int n)
    {
        stageParameter = n;
        popupPanel.SetActive(true);

        switch(n)
        {
            case 1:
                stageTitleText.text = "기숙사";
                stageDescriptionText.text = "기숙사입니다.기숙사입니다.기숙사입니다.기숙사입니다.기숙사입니다.";

                break;

            case 2:
                stageTitleText.text = "백년관";
                stageDescriptionText.text = "백년관입니다.백년관입니다.백년관입니다.백년관입니다.백년관입니다.";
                break;

            case 3:
                stageTitleText.text = "자연과학관";
                stageDescriptionText.text = "자연과학관입니다.자연과학관입니다.자연과학관입니다.자연과학관입니다.자연과학관입니다.";
                break;

            case 4:
                stageTitleText.text = "공학관";
                stageDescriptionText.text = "공학관입니다.공학관입니다.공학관입니다.공학관입니다.공학관입니다.공학관입니다.";
                break;

            case 5:
                stageTitleText.text = "어문학관";
                stageDescriptionText.text = "어문학관입니다.어문학관입니다.어문학관입니다.어문학관입니다.어문학관입니다.어문학관입니다.어문학관입니다.어문학관입니다.어문학관입니다.어문학관입니다.";
                break;
        }
    }

    //mainPanel 스테이지 팝업 닫기
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    //mainPanel 스테이지 전투
    public void StartBattle()
    {
        popupPanel.SetActive(false);

        PlayerPrefs.SetInt("stageParameter", stageParameter);
        PlayerPrefs.Save();

        SceneManager.LoadScene("GameScene");
    }

    //mainPanel 업그레이드 버튼
    public void UpgradeBtnClick()
    {
        SceneManager.LoadScene("UpgradeScene");
    }

    //endPanel 다시하기 버튼
    public void RestartBtnClick()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainScene");
    }
}

