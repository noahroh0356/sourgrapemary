using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MainQuestPanel : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text processText;
    MainQuestData mainQuestdata;
    public Button questButton; // 버튼을 수동으로 할당할 변수


    private bool isQuestCompleted = false;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnPanelClicked);
    }


    public void CompleteQuest()
    {
        titleText.text = "퀘스트 완료! (클릭하세요)";
        isQuestCompleted = true;
        Debug.Log("퀘스트 완료!");
    }

    public void OnPanelClicked()
    {
        if (isQuestCompleted)
        {
            Debug.Log("가챠코인 지급!");
            MainQuestManager.Instance.CompleteCurrentQuest();
            //User.Instance.AddGatchaCoin(1); // 가챠코인 지급
            //MainQuestManager.Instance.StartQuest(); // 다음 퀘스트 시작
        }
    }



    public void StartQuest(MainQuestData data)
    {
        mainQuestdata = data;
        titleText.text = data.title.ToString();
        isQuestCompleted = false;
        processText.text = MainQuestManager.Instance.userMainQuest.process + "/" + data.goal;
    }

    public void UpdatePanel()
    {
        processText.text = MainQuestManager.Instance.userMainQuest.process + "/" + mainQuestdata.goal;
    }


}
