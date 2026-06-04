using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class MainSceneScript : MonoBehaviour
{
    public MainSceneBGMScript BGMManger;

    public GameObject startPanel;
    public GameObject storyPanel;
    public GameObject mainPanel;
    public GameObject tutorialPanel;
    public GameObject endPanel;
    public GameObject popupPanel;

    public Image storyImage;
    public Sprite[] storySprite;

    public Image tutorialImage;
    public Sprite[] tutorialSprite;

    public Image endingImage;
    public Sprite[] endingSprite;

    public TMP_Text stageTitleText;
    public TMP_Text stageDescriptionText;

    private bool storyPlaying = false;
    private int storyIndex = 0;

    private bool tutorialPlaying = false;
    private int tutorialIndex = 0;

    private bool endingPlaying = false;
    private int endingIndex = 0;

    private int stageParameter = 0;

    void Start()
    {
        int isMainPanel = PlayerPrefs.GetInt("isMainPanel", 0);

        if (isMainPanel == 0)
        {
            startPanel.SetActive(true);
            storyPanel.SetActive(false);
            mainPanel.SetActive(false);
            tutorialPanel.SetActive(false);
            endPanel.SetActive(false);

            BGMManger.PlayStartStoryBGM();
        }
        else
        {
            if (IsClearGame())
            {
                startPanel.SetActive(false);
                storyPanel.SetActive(false);
                mainPanel.SetActive(false);
                tutorialPanel.SetActive(false);
                endPanel.SetActive(true);

                BGMManger.PlayEndBGM();

                endingIndex = 0;
                endingImage.sprite = endingSprite[0];
                endingPlaying = true;
            }
            else
            {
                startPanel.SetActive(false);
                storyPanel.SetActive(false);
                mainPanel.SetActive(true);
                tutorialPanel.SetActive(false);
                endPanel.SetActive(false);

                BGMManger.PlayTutorialMainBGM(true);
            }
        }

        popupPanel.SetActive(false);
        
    }

    void Update()
    {
        if (storyPlaying && Mouse.current.leftButton.wasPressedThisFrame)
        {
            StoryNext();
        }

        if (tutorialPlaying && Mouse.current.leftButton.wasPressedThisFrame)
        {
            TutorialNext();
        }

        if (endingPlaying && Mouse.current.leftButton.wasPressedThisFrame)
        {
            EndingNext();
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
        bool stageClear_6 = PlayerPrefs.GetInt("stage6_clear", 0) == 1;

        return stageClear_1 && stageClear_2 && stageClear_3 && stageClear_4 && stageClear_5 && stageClear_6;
    }

    //startPanel 시작하기 버튼
    public void StartBtnClick()
    {
        startPanel.SetActive(false);
        storyPanel.SetActive(true);

        storyIndex = 0;
        storyImage.sprite = storySprite[0];
        storyPlaying = true;
    }

    //storyPanel 장면 넘기기
    public void StoryNext()
    {
        storyIndex++;

        if (storyIndex < storySprite.Length)
        {
            storyImage.sprite = storySprite[storyIndex];
        }
        else
        {
            storyPanel.SetActive(false);
            mainPanel.SetActive(true);

            storyPlaying = false;
            BGMManger.PlayTutorialMainBGM(false);
        }
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
            BGMManger.SaveMainBGMTime();
            tutorialPlaying = false;
            SceneManager.LoadScene("GameScene");
        }
    }

    //endPanel 장면 넘기기
    public void EndingNext()
    {
        endingIndex++;

        if (endingIndex < endingSprite.Length)
        {
            endingImage.sprite = endingSprite[endingIndex];
        } 
        else
        {
            endingPlaying = false;
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
                stageDescriptionText.text = "\n신입생들의 첫 전장이 되는 기숙사 스테이지입니다. \n입시보다 기숙사 입주가 더 어렵다는 소문이 있습니다. \n\n전투의 기초를 배울 수 있는 스테이지입니다. \n(난이도☆☆☆☆☆)";
                break;

            case 2:
                stageTitleText.text = "백년관";
                stageDescriptionText.text = "\n기후변화융합학과 학생들의 본진인 백년관입니다. \n기상과 환경을 탐구하는 학생들의 열정이 가득한 곳입니다. \n\n우산이 없어서 비를 맞는 학생들이 많긴 하지만요. \n\n(난이도★☆☆☆☆)";
                break;

            case 3:
                stageTitleText.text = "자연과학관";
                stageDescriptionText.text = "\n화학과 학생들의 본진인 자연과학관입니다. \n실험과 연구가 끊이지 않는 과학의 요람입니다. \n\n(난이도★★☆☆☆)";
                break;

            case 4:
                stageTitleText.text = "공학관";
                stageDescriptionText.text = "\n컴퓨터공학과 학생들의 본진인 공학관입니다. \n수많은 아이디어와 코드가 탄생하는 개발의 중심지 입니다. \n\n가끔 버그를 못잡는 본인이 버그인지 헷갈리기도 합니다. \n\n(난이도★★★☆☆)";
                break;

            case 5:
                stageTitleText.text = "교양관";
                stageDescriptionText.text = "\n글스산학과 학생들의 본진인 교양관입니다. \n한국 스포츠의 미래를 준비하는 학생들이 모여 있습니다. \n\n(난이도★★★★☆)";
                break;

            case 6:
                stageTitleText.text = "어문학관";
                stageDescriptionText.text = "\n통번역과 학생들의 본진인 어문학관입니다. \n다양한 언어와 문화가 교류하는 소통의 공간입니다. \n\n비속어로 말이죠... \n\n(난이도★★★★★)";
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

        if (stageParameter == 1)
        {
            mainPanel.SetActive(false);
            tutorialPanel.SetActive(true);

            tutorialIndex = 0;
            tutorialImage.sprite = tutorialSprite[0];
            tutorialPlaying = true;
        }
        else
        {
            BGMManger.SaveMainBGMTime();
            SceneManager.LoadScene("GameScene");
        }
    }

    //mainPanel 업그레이드 버튼
    public void UpgradeBtnClick()
    {
        BGMManger.SaveMainBGMTime();
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

