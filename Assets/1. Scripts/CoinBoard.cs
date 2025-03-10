using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinBoard : MonoBehaviour
{

    public TMP_Text coinText;
    //public UserData userData;

    private void Update()
    {
       UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = User.Instance.userData.coin.ToString();
        }
        else
        {
            Debug.LogError("coinText가 null입니다.");
        }
    }

}
