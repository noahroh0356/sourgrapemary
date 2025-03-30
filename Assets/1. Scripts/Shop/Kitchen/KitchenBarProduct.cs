using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KitchenBarProduct : MonoBehaviour
{

    public string key;
    KitchenData kitchenData;
    public TMP_Text priceText;

    public void Start()
    {
        KitchenManager mgr = FindObjectOfType<KitchenManager>();
        kitchenData = mgr.GetKitchenData(key);
        priceText.text = kitchenData.price.ToString();
    }

    //키값에 해당하는 주방 가구를 구매 시도하는 함수
    public void OnClickPurchase()
    {
        Debug.Log(key + "구매시도");
        if (User.Instance.userData.coin < kitchenData.price)
        {
            Debug.Log(key + "재화부족");
            return;
        }

        else
        {
            User.Instance.AddKitchenFurniture(key);
            KitchenManager.Instance.UpdateKitchen();
            MainQuestManager.Instance.DoQuest(MainQuestType.PurchaseKitchen);
            GetComponentInParent<KitchenBarPlaceProduct>().UpdateKitchenBarPlace(); 
            User.Instance.userData.coin -= kitchenData.price;
            //User.Instance.UpdateCoinText();


        }

    }

}
