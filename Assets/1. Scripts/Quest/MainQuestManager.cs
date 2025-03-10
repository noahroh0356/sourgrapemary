using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestManager : MonoBehaviour
{
    public static MainQuestManager Instance;
    public MainQuestData[] mainQuestDatas; // 메인 퀘스트 데이터 특정 퀘스트 정보
    public UserMainQuest userMainQuest;
    public int curQuestIndex = 0; // 퀘스트가 차례대로 나타나도록 설정=진행 중인 퀘스트

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartQuest();
    }


    public void StartQuest()
    {
        //퀘스트가 차례대로 나타나도록 설
        MainQuestData data = mainQuestDatas[curQuestIndex];
        //MainQuestData data = mainQuestDatas[Random.Range(0, mainQuestDatas.Length)];

        userMainQuest = new UserMainQuest();
        userMainQuest.mainQuestType = data.mainQuestType;
        FindObjectOfType<MainQuestPanel>().StartQuest(data);
    }


public void DoQuest(MainQuestType type)
    {
        if (userMainQuest.mainQuestType == type)
        {
            userMainQuest.process++;
            FindObjectOfType<MainQuestPanel>().UpdatePanel();
        }
        //""현재 진행중인 퀘스트(=만들어야함)""가 타입과 같다면 퀘스트 진행도 1추가
    }

    public bool CheckClear() // **차례대로 클리어
    {
        MainQuestData curQuestData = GetMainQuestData(userMainQuest.mainQuestType);

        if (curQuestData != null && userMainQuest.process >= curQuestData.goal)
        {
            Debug.Log("퀘스트 완료!");
            curQuestIndex++;  // 다음 퀘스트로 인덱스 증가
            User.Instance.AddExp(1);
            StartQuest();         // 다음 퀘스트 시작
            return true;
        }

        return false;
    }
    //public bool CheckClear()
    //{
    //    MainQuestData curQuestData = null;

    //    for (int i = 0; i < mainQuestDatas.Length; i++)
    //    {
    //        if (mainQuestDatas[i].mainQuestType == userMainQuest.mainQuestType)
    //        {
    //            curQuestData = mainQuestDatas[i];
    //        }
    //    }

    //    if (userMainQuest.process >= curQuestData.goal)
    //    {
    //        return true;
    //    }

    //    return false;

    //    //현재 진행중인 퀘스트 데이
    //    //현재 진행 중인 퀘스트를 완료할 수 있으면 true아니면 false

public MainQuestData GetMainQuestData(MainQuestType type)

    {
        for (int i = 0; i < mainQuestDatas.Length; i++)
        {
            if (mainQuestDatas[i].mainQuestType == type)
            {
                return mainQuestDatas[i];
            }
        }
        return null;

    }
        

}

[System.Serializable]
public class UserMainQuest
{
    public MainQuestType mainQuestType;
    public int process;
}


[System.Serializable]
public class MainQuestData
{
    public MainQuestType mainQuestType;
    public int goal;
    public string title;
    //public int exp;

}

public enum MainQuestType
{
CallCustomer,
TakeOrder,
PickUpAcon,
PurchaseFurniture,
PurchaseKitchen,
PlayGatcha,
}
