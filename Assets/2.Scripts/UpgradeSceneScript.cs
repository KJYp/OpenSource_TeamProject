using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeSceneScript : MonoBehaviour
{
    public UpgradeSceneUnitScript unitScript;

    public GameObject hidePanel;
    public GameObject[] unitPrefabs;
    public bool isHealUnit = false;

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
    public TMP_Text UnitUpgradeGoldText;

    public Image beforeAPImage;
    public Image afterAPImage;
    public Sprite[] APSprite;

    public Sprite[] emptyImage;
    public Sprite[] meleeUnitSprites;
    public Sprite[] tankUnitSprites;
    public Sprite[] rangedUnitSprites;
    public Sprite[] damageUnitSprites;
    public Sprite[] healerUnitSprites;
    void Start()
    {
        PlayerPrefs.SetInt("isMainPanel", 1);
        PlayerPrefs.Save();

        beforeAPImage.sprite = APSprite[0];
        afterAPImage.sprite = APSprite[0];

        beforeUpgradeHPText.text = "0";
        beforeUpgradeAPText.text = "0";
        beforeUpgradeACText.text = "0";
        beforeUpgradeMSText.text = "0";
        beforeUpgradeMCText.text = "0";
        beforeUpgradeSCText.text = "0";

        afterUpgradeHPText.text = "0";
        afterUpgradeAPText.text = "0";
        afterUpgradeACText.text = "0";
        afterUpgradeMSText.text = "0";
        afterUpgradeMCText.text = "0";
        afterUpgradeSCText.text = "0";

        UnitUpgradeGoldText.text = "0";
        unitScript.ChangeAnimation(emptyImage);

        hidePanel.SetActive(true);
    }

    void Update()
    {
        
    }

    //뒤로가기 버튼
    public void BackBtnClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SetUnitStat(int prefabParameter, int imageParameter)
    {
        hidePanel.SetActive(false);

        if (imageParameter == 0)
        {
            beforeAPImage.sprite = APSprite[0];
            afterAPImage.sprite = APSprite[0];
            isHealUnit = false;
        }
        if (imageParameter == 1)
        {
            beforeAPImage.sprite = APSprite[1];
            afterAPImage.sprite = APSprite[1];
            isHealUnit = true;
        }

        UnitStats stat = unitPrefabs[prefabParameter].GetComponent<UnitStats>();

        beforeUpgradeHPText.text = stat.maxHp.ToString();
        beforeUpgradeAPText.text = isHealUnit ? stat.attackPower.ToString() : stat.healPower.ToString();
        beforeUpgradeACText.text = stat.attackCooldown.ToString();
        beforeUpgradeMSText.text = stat.moveSpeed.ToString();
        beforeUpgradeMCText.text = stat.maxHp.ToString();
        beforeUpgradeSCText.text = stat.maxHp.ToString();

        //미리 업그레이드 텍스트 같은걸 저장해서 불러와야 할듯
        afterUpgradeHPText.text = (float.Parse(beforeUpgradeHPText.text) + 15).ToString();
        afterUpgradeAPText.text = (float.Parse(beforeUpgradeAPText.text) + 5).ToString();
        afterUpgradeACText.text = (float.Parse(beforeUpgradeACText.text) + 1).ToString();
        afterUpgradeMSText.text = (float.Parse(beforeUpgradeMSText.text) + 0).ToString();
        afterUpgradeMCText.text = (float.Parse(beforeUpgradeMCText.text) + 0).ToString();
        afterUpgradeSCText.text = (float.Parse(beforeUpgradeSCText.text) + 0).ToString();

        UnitUpgradeGoldText.text = 4000.ToString();
    }

    public void UpgradeBtnClick()
    {
        Debug.Log("업글버튼 클릭");
    }

    //컴공과 유닛 선택
    public void MeleeUnitBtnClick()
    {
        UnitDescriptionText.text = "컴공과 유닛입니다.";
        unitScript.ChangeAnimation(meleeUnitSprites);
        SetUnitStat(0, 0);
    }

    //글스산 유닛 선택
    public void TankUnitBtnClick()
    {
        UnitDescriptionText.text = "글스산 유닛입니다.";
        unitScript.ChangeAnimation(tankUnitSprites);
        SetUnitStat(1, 0);
    }

    //기후학과 유닛 선택
    public void RangedUnitBtnClick()
    {
        UnitDescriptionText.text = "기후학과 유닛입니다.";
        unitScript.ChangeAnimation(rangedUnitSprites);
        SetUnitStat(2, 0);
    }

    //화학과 유닛 선택
    public void DamageUnitBtnClick()
    {
        UnitDescriptionText.text = "화학과 유닛입니다.";
        unitScript.ChangeAnimation(damageUnitSprites);
        SetUnitStat(3, 0);
    }

    //통번역과 유닛 선택
    public void HealerUnitBtnClick()
    {
        UnitDescriptionText.text = "통번역과 유닛입니다.";
        unitScript.ChangeAnimation(healerUnitSprites);
        SetUnitStat(4, 1);
    }
}
