using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeSceneScript : MonoBehaviour
{
    public TMP_Text beforeUpgradeHPText;
    public TMP_Text beforeUpgradeAPText;
    public TMP_Text beforeUpgradeACText;
    public TMP_Text beforeUpgradeMSText;
    public TMP_Text beforeUpgradeMCText;
    public TMP_Text beforeUpgradeSCText;

    public TMP_Text afterUpgradeHPText;
    public TMP_Text afterUpgradeAPText;
    public TMP_Text afterUpgradeACText;
    public TMP_Text afterUpgradeMSText;
    public TMP_Text afterUpgradeMCText;
    public TMP_Text afterUpgradeSCText;

    public TMP_Text UnitDescriptionText;

    public Image beforeAPImage;
    public Image afterAPImage;
    public Sprite[] APSprite;

    void Start()
    {
        PlayerPrefs.SetInt("isMainPanel", 1);
        PlayerPrefs.Save();
    }

    void Update()
    {
        
    }

    //뒤로가기 버튼
    public void BackBtnClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetUnitStat(int n)
    {
        if (n == 0)
        {
            beforeAPImage.sprite = APSprite[0];
            afterAPImage.sprite = APSprite[0];
        } 
        if (n == 1)
        {
            beforeAPImage.sprite = APSprite[1];
            afterAPImage.sprite = APSprite[1];
        }
    }

    public void MeleeUnitBtnClick()
    {
        UnitDescriptionText.text = "컴공과 유닛입니다.";
        SetUnitStat(0);
    }

    public void DamageUnitBtnClick()
    {
        UnitDescriptionText.text = "화학과 유닛입니다.";
        SetUnitStat(0);
    }

    public void TankUnitBtnClick()
    {
        UnitDescriptionText.text = "글스산 유닛입니다.";
        SetUnitStat(0);
    }

    public void RangedUnitBtnClick()
    {
        UnitDescriptionText.text = "기후학과 유닛입니다.";
        SetUnitStat(0);
    }
    
    public void HealerUnitBtnClick()
    {
        UnitDescriptionText.text = "통번역과 유닛입니다.";
        SetUnitStat(1);
    }
}
