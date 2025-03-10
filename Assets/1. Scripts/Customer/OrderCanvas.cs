using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderCanvas : MonoBehaviour //말풍선 안에 주문 이미지
{

    public Image menuImage;

    public void SetOrderMenu(MenuData data)
    {
        gameObject.SetActive(true);
        menuImage.sprite = data.menuImage;
    }

}
