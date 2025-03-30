using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatchaCoinBoard : MonoBehaviour
{

    public TMP_Text gatchaCoinText;
    //public UserData userData;

    private void Update()
    {
        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        if (gatchaCoinText != null)
        {
            gatchaCoinText.text = User.Instance.userData.gatchaCoin.ToString();
        }
        else
        {
            Debug.LogError("gatchaCoin가 null입니다.");
        }
    }

}
