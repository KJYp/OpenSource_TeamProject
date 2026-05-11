using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeSceneScript : MonoBehaviour
{

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
}
