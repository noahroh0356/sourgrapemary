using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour
{
    public static KitchenManager Instance;
    public KitchenBarPlace[] kitchenBarPlaces;
    public Button kitchenButton;
    public Transform center;

    public KitchenData[] kitchenData;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        UpdateKitchen();
    }


    public void StartArea()
    {
        kitchenButton.gameObject.SetActive(false);
    }


    public void EndArea()
    {
        kitchenButton.gameObject.SetActive(true);
    }

    public void Order(MenuData orderMenu, Customer customer)
    {
        waitingOrderMenus.Add(orderMenu);
        waitingCustomers.Add(customer);

        MatchOrder();
    }


    public KitchenBarPlace GetAvailableKitchenBarPlace()

    {
        KitchenBarPlace kitchenBarPlace = null;

        for (int i = 0; i < kitchenBarPlaces.Length; i++)
        {
            if (kitchenBarPlaces[i].curKitchenBar !=null && kitchenBarPlaces[i].making == false)
            {
                kitchenBarPlace = kitchenBarPlaces[i];
                break;
            }
        }

        return kitchenBarPlace;
    }

    List<MenuData> waitingOrderMenus = new List<MenuData>();
    List<Customer> waitingCustomers = new List<Customer>();


    public void MatchOrder()
    {
        if (waitingOrderMenus.Count <= 0)
            return;

        KitchenBarPlace barPlace = GetAvailableKitchenBarPlace();
        if (barPlace == null)
            return;

        MainQuestManager.Instance.DoQuest(MainQuestType.TakeOrder);//** 퀘스트
        barPlace.StartMake(waitingOrderMenus[0], waitingCustomers[0]);
        waitingOrderMenus.RemoveAt(0);
        waitingCustomers.RemoveAt(0);

        MatchOrder();
    }


    //주방에 대한 시설 업데이트 필요시 호출 
    public void UpdateKitchen()
    {
        //모든 가구를 업데이트
        for (int i = 0; i < kitchenBarPlaces.Length; i++)
        {
            kitchenBarPlaces[i].UpdateKitchenBarPlace();
            Debug.Log("키친바업데이트");
                }
        // 테이블이 추가될때 settarget
    }



    public KitchenData GetKitchenData(string key)
    {
        for (int i = 0; i < kitchenData.Length; i++)

        {
            if (kitchenData[i].key == key)
            {
                return kitchenData[i];
            }
        }
        return null;

    }



}

[System.Serializable]
public class KitchenData
{
    public string key;
    public string nextProductKey;
    public KitchenPlaceType kitchenPlaceType;
    public int price;
    public float reduceMakingTime=0.2f;

}

public enum KitchenPlaceType
{
    KitchenBar_0,
    KitchenBar_1,
    KitchenBar_2,
    KitchenBar_3,
    KitchenBar_4,
    KitchenBar_5,
}
