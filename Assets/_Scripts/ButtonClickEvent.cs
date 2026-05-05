using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject CutScenePanel;
    public GameObject StagePanel;

    public void MenuBtnClick()
    {
        Debug.Log("Å¬¸¯!");

        MenuPanel.SetActive(false);
        CutScenePanel.SetActive(true);
    }
}
