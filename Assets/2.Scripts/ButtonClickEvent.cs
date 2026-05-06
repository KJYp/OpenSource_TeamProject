using UnityEngine;

public class ButtonClickEvent : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject TutorialPanel;
    public GameObject MainPanel;
    public GameObject EndPanel;

    public void StartBtnClick()
    {
        Debug.Log("AAAAAA");

        StartPanel.SetActive(false);
        TutorialPanel.SetActive(true);
    }
}

