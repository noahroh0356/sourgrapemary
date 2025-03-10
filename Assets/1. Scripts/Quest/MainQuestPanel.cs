using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainQuestPanel : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text processText;
    MainQuestData mainQuestdata;



    public void StartQuest(MainQuestData data)
    {
        mainQuestdata = data;
        titleText.text = data.title.ToString();
        processText.text = MainQuestManager.Instance.userMainQuest.process + "/" + data.goal;
    }

    public void UpdatePanel()
    {
        processText.text = MainQuestManager.Instance.userMainQuest.process + "/" + mainQuestdata.goal;
    }

}
