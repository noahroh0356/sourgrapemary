using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainQuestManager : MonoBehaviour
{
    public static MainQuestManager Instance;
    public MainQuestData[] mainQuestDatas; // 메인 퀘스트 데이터 특정 퀘스트 정보
    public UserMainQuest userMainQuest;
    public int curQuestIndex = 0; // 퀘스트가 차례대로 나타나도록 설정=진행 중인 퀘스트
   

    public string[] purchaseFurnitureQuestKeys;
    public string[] purchaseKitchenQuestKeys;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        userMainQuest = SaveMgr.LoadData<UserMainQuest>("UserMainQuest");
        if (userMainQuest == null)
        {
            userMainQuest = new UserMainQuest();
        }

        if (userMainQuest.processing == false)
        {

            StartQuest();
        }

        else
        {
            MainQuestData data = GetMainQuestData(userMainQuest.mainQuestType);
            FindObjectOfType<MainQuestPanel>().StartQuest(data);
        }
    }


    public void StartQuest()
    {
        //퀘스트가 차례대로 나타나도록 설
        MainQuestData data = mainQuestDatas[curQuestIndex];
        //MainQuestData data = mainQuestDatas[Random.Range(0, mainQuestDatas.Length)];

        //userMainQuest = new UserMainQuest();

        if (data.mainQuestType == MainQuestType.PurchaseFurniture)
        {
          string nextGoal = data.GetGoal();
            if (nextGoal == null)
            {
                curQuestIndex++;
                StartQuest();
                return;
            }
                }
        userMainQuest.mainQuestType = data.mainQuestType;
        FindObjectOfType<MainQuestPanel>().StartQuest(data);
    }


public void DoQuest(MainQuestType type)
    {
        if (userMainQuest.mainQuestType == type)
        {
            if (userMainQuest.process < GetMainQuestData(type).goal)
            {
                userMainQuest.process++;
                CheckClear();
                FindObjectOfType<MainQuestPanel>().UpdatePanel();
            }
        }
        //""현재 진행중인 퀘스트(=만들어야함)""가 타입과 같다면 퀘스트 진행도 1추가
    }

    public bool CheckClear() // **차례대로 클리어
    {
        MainQuestData curQuestData = GetMainQuestData(userMainQuest.mainQuestType);

        if (userMainQuest.mainQuestType == MainQuestType.PurchaseFurniture)
        {
            string furnitureKey = curQuestData.goalString;
            //string furnitureKey = curQuestData.GetGoal();
            UserFurniture userFurniture = User.Instance.GetUserFurniture(furnitureKey);
            if (userFurniture != null && userFurniture.purchased)
            {
                FindObjectOfType<MainQuestPanel>().CompleteQuest();
                return true;
            }
        }
        else if (userMainQuest.mainQuestType == MainQuestType.PurchaseKitchen)
        {
            string KitchenKey = curQuestData.goalString;
            UserKitchen userKitchenBar = User.Instance.GetUserKitchen(KitchenKey);
            {
                FindObjectOfType<MainQuestPanel>().CompleteQuest();
                return true;
            }

        }

        else
        {
            if (userMainQuest.process >= curQuestData.goal)
            {
                Debug.Log($"퀘스트 완료! {userMainQuest.mainQuestType}");
                FindObjectOfType<MainQuestPanel>().CompleteQuest();
                return true;
            }
        }

        //else
        //{ 
        //    if (curQuestData != null && userMainQuest.process >= curQuestData.goal)
        //    {
        //        Debug.Log("퀘스트 완료!");
        //        FindObjectOfType<MainQuestPanel>().CompleteQuest();
        //        curQuestIndex++;  // 다음 퀘스트로 인덱스 증가
        //        User.Instance.AddGatchaCoin(1);
        //        StartQuest();         // 다음 퀘스트 시작
        //        return true;
        //    }
        //}

        return false;
    }

    public void CompleteCurrentQuest()
    {
        Debug.Log("퀘스트 완료!");


        curQuestIndex++;  // 다음 퀘스트로 인덱스 증가
        userMainQuest.processing = false; // 퀘스트 진행 상태 초기화
        User.Instance.AddGatchaCoin(1); // 가챠코인 지급

        //SaveMgr.SaveData("UserMainQuest", userMainQuest); // 데이터 저장

        //다음 퀘스트 시작
        StartQuest();
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
public class UserMainQuest // 현재 유저가 진행 중인 퀘스트에 대한 정보
{
    public MainQuestType mainQuestType;
    public int process;

    public bool processing;
    public int clearPurchaseFurnitureCount;
    public int clearPurchaseKitchenCount;
}


[System.Serializable]
public class MainQuestData
{
    public MainQuestType mainQuestType;
    public int goal;
    public string goalString;
    public string title;
    //public int exp;
    public string GetGoal()
    {
        if (mainQuestType == MainQuestType.PurchaseFurniture)
        {
            int idx = MainQuestManager.Instance.userMainQuest.clearPurchaseFurnitureCount;

            if (idx >= MainQuestManager.Instance.purchaseFurnitureQuestKeys.Length)
                return null;

            return MainQuestManager.Instance.purchaseFurnitureQuestKeys[idx];
        }

        else if (mainQuestType == MainQuestType.PurchaseKitchen)
        {
            int idx = MainQuestManager.Instance.userMainQuest.clearPurchaseKitchenCount;

            if (idx >= MainQuestManager.Instance.purchaseKitchenQuestKeys.Length)
                return null;

            return MainQuestManager.Instance.purchaseKitchenQuestKeys[idx];
        }

        return null;
    }

}

public enum MainQuestType
{
CallCustomer,
TakeOrder, // 설정
PickUpAcon, //설정 
PurchaseFurniture,
PurchaseKitchen,
PlayGatcha, // 설정
}
