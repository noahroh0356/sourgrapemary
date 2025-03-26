using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FoxProductPanel : MonoBehaviour
{
    public string key;
    public TextMeshProUGUI PorUtext;
    public TextMeshProUGUI Pricetext;
    public Image foxImage;
    FoxData foxData;


    public void Start()
    {
        FoxManager mgr = FindObjectOfType<FoxManager>();
        foxData = mgr.GetFoxData(key);
        Pricetext.text = foxData.price.ToString();
    }

    public void OnClickPurchased()
    {

        Debug.Log(key + "구매시도");
        if (User.Instance.userData.coin < foxData.price)
        {
            Debug.Log(key + "재화부족");
            return;
        }

        else
        {
            User.Instance.AddFox(key);
            FoxManager.Instance.AddFox(key);
            User.Instance.userData.coin -= foxData.price;
            //User.Instance.UpdateCoinText();
            //FoxManager.Instance.UpdateFox();
            //PorUtext.text = "업그레이드";

        }
    }
}
