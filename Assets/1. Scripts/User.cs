using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class User : MonoBehaviour
{
    public static User Instance;
    public UserData userData;

    public UserFurniture GetSetUpFurniture(FurniturePlace furniturePlace)
    {
        for (int i = 0; i < userData.userFurnitureList.Count; i++)
        {
            if (userData.userFurnitureList[i].setup == false)
                continue;

            FurnitureData furnitureData = FurnitureManager.Instance.GetFurnitureData(userData.userFurnitureList[i].furniturekey);

            if (furnitureData.furniturePlace == furniturePlace)
            {
                return userData.userFurnitureList[i];
            }
        }
        return null;

    }

    public UserKitchen GetSetUpKitchen(KitchenPlaceType kitchenPlaceType)
    {

        for (int i = 0; i < userData.userKitchenList.Count; i++)
        {
            if (userData.userKitchenList[i].setup == false)
            continue;
            KitchenData kitchenData = KitchenManager.Instance.GetKitchenData(userData.userKitchenList[i].kitchenkey);

            if (kitchenData.kitchenPlaceType == kitchenPlaceType)
            {
                return userData.userKitchenList[i];
            }
        }
        return null;

    }


    public void AddExp(int e)
    {
        userData.exp += e;
        if (userData.exp >= MaxExp(userData.level))
        {
            LevelUp();
        }

    }

    int MaxExp(int lv)
    {
        return 15 + lv * 3;
    }

    void LevelUp()
    {
        userData.level++;
        userData.exp = 0;
        SaveMgr.SaveData("UserData", userData);
    }


    private void Awake()
    {
        Instance = this; 
        userData = SaveMgr.LoadData<UserData>("UserData");

        if (userData == null)
        {
            userData = new UserData();
            userData.coin = 0;
            SaveMgr.SaveData<UserData>("UserData", userData);
        }


    }



    void Start()
    {
        //userData = new UserData();

        //userData.coin = PlayerPrefs.GetInt("Coin", 0);        
        //UpdateCoinText();
        //FindObjectOfType<RestaurantManager>();
       
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AddCoin(10);
        }
    }

    public void AddFurniture(string key)
    {
        FurnitureData data = FurnitureManager.Instance.GetFurnitureData(key);
        UserFurniture setupedFurniture = GetSetUpFurniture(data.furniturePlace);
        if (setupedFurniture != null)
        {
            setupedFurniture.setup = false;
        }


        UserFurniture userFurniture = GetUserFurniture(key);
        userFurniture.purchased = true;
        userFurniture.setup = true;
    }

    public void AddKitchenFurniture(string key)
    {
        KitchenData data = KitchenManager.Instance.GetKitchenData(key);
        UserKitchen setupedKitchen = GetSetUpKitchen(data.kitchenPlaceType);
        if (setupedKitchen != null)
        {
            setupedKitchen.setup = false;
        }

        UserKitchen userKitchen = GetUserKitchen(key);
        userKitchen.purchased = true;
        userKitchen.setup = true;
    }

    public void AddCustomer(string key)
    {
        UserCustomer userCustomer = GetUserCustomer(key);
        if (userCustomer == null) // 이미 등록된 손님인지 확인
        {
            userCustomer = new UserCustomer();
            userCustomer.key = key;
            userCustomer.open = true; // 또는 획득 여부를 나타내는 다른 상태로 설정
            userData.userCustomers.Add(userCustomer);
            SaveMgr.SaveData<UserData>("UserData", userData); // 데이터 저장
        }
    }

    public UserFurniture GetUserFurniture(string key)
    {
        for (int i = 0; i < userData.userFurnitureList.Count; i++)
        {
            if (userData.userFurnitureList[i].furniturekey == key)
            {
                return userData.userFurnitureList[i];
            }
        }
        UserFurniture userFurniture = new UserFurniture();
        userFurniture.furniturekey = key;
        userFurniture.purchased = false;
        userData.userFurnitureList.Add(userFurniture);
        return userFurniture;
    }

    public UserKitchen GetUserKitchen(string key)
    {
        for (int i = 0; i < userData.userKitchenList.Count; i++)
        {
            if (userData.userKitchenList[i].kitchenkey == key)
            {
                return userData.userKitchenList[i];
            }
        }
        UserKitchen userKitchen = new UserKitchen();
        userKitchen.kitchenkey = key;
        userKitchen.purchased = false;
        userData.userKitchenList.Add(userKitchen);
        return userKitchen;
    }

    public UserCustomer GetUserCustomer(string key)
    {
        for (int i = 0; i < userData.userCustomers.Count; i++)
        {
            if (userData.userCustomers[i].key == key)
            {
                return userData.userCustomers[i];
            }
        }
        return null;
    }

    public void AddCoin(int c)
    {
        userData.coin += c;
        //UpdateCoinText();
        //Debug.Log("coinText.text 값: " + coinText.text);
        SaveMgr.SaveData<UserData>("UserData", userData);

    }


}

[System.Serializable]
public class UserData
{

    public int level; // 레벨 
    public int exp; //경험치 - 메인 퀘스트를 통해서 획득
    public int coin;
    public List<UserFurniture> userFurnitureList = new List<UserFurniture>();
    public List<UserKitchen> userKitchenList = new List<UserKitchen>();
    public List<UserCustomer> userCustomers = new List<UserCustomer>();

}

[System.Serializable]
public class UserFurniture
{
    public string furniturekey;
    public bool purchased; // 초기값 false
    public bool setup;// 초기값 false
}

[System.Serializable]

public class UserKitchen
{
    public string kitchenkey;
    public bool purchased; // 초기값 false
    public bool setup;// 초기값 false


}

[System.Serializable]

public class UserCustomer
{
    public string key;
    public bool open;

}