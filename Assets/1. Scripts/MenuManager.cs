using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{

    public MenuData[] menuDatas;
    public static MenuManager Instance;

    void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        menuDatas = new MenuData[12];

        menuDatas[0] = new MenuData();
        menuDatas[0].menuName = "wine_0";
        menuDatas[0].price = 10;
        menuDatas[0].makingTime = 10;
        menuDatas[0].menuOrderImage = Resources.Load<Sprite>("Images/wine_0_1");
        menuDatas[0].menuImage = Resources.Load<Sprite>("Images/wine_0");

        menuDatas[1] = new MenuData();
        menuDatas[1].menuName = "wine_1";
        menuDatas[1].price = 12;
        menuDatas[1].makingTime = 12;
        menuDatas[1].menuOrderImage = Resources.Load<Sprite>("Images/wine_1_1");
        menuDatas[1].menuImage = Resources.Load<Sprite>("Images/wine_1");

        menuDatas[2] = new MenuData();
        menuDatas[2].menuName = "wine_2";
        menuDatas[2].price = 12;
        menuDatas[2].makingTime = 12;
        menuDatas[2].menuOrderImage = Resources.Load<Sprite>("Images/wine_2_1");
        menuDatas[2].menuImage = Resources.Load<Sprite>("Images/wine_2");


        menuDatas[3] = new MenuData();
        menuDatas[3].menuName = "wine_3";
        menuDatas[3].price = 12;
        menuDatas[3].makingTime = 12;
        menuDatas[3].menuOrderImage = Resources.Load<Sprite>("Images/wine_3_1");
        menuDatas[3].menuImage = Resources.Load<Sprite>("Images/wine_3");

        menuDatas[4] = new MenuData();
        menuDatas[4].menuName = "wine_4";
        menuDatas[4].price = 12;
        menuDatas[4].makingTime = 12;
        menuDatas[4].menuOrderImage = Resources.Load<Sprite>("Images/wine_4_1");
        menuDatas[4].menuImage = Resources.Load<Sprite>("Images/wine_4");

        menuDatas[5] = new MenuData();
        menuDatas[5].menuName = "wine_5";
        menuDatas[5].price = 12;
        menuDatas[5].makingTime = 12;
        menuDatas[5].menuOrderImage = Resources.Load<Sprite>("Images/wine_5_1");
        menuDatas[5].menuImage = Resources.Load<Sprite>("Images/wine_5");

        menuDatas[6] = new MenuData();
        menuDatas[6].menuName = "wine_6";
        menuDatas[6].price = 12;
        menuDatas[6].makingTime = 12;
        menuDatas[6].menuOrderImage = Resources.Load<Sprite>("Images/wine_6_1");
        menuDatas[6].menuImage = Resources.Load<Sprite>("Images/wine_6");

        menuDatas[7] = new MenuData();
        menuDatas[7].menuName = "wine_7";
        menuDatas[7].price = 12;
        menuDatas[7].makingTime = 12;
        menuDatas[7].menuOrderImage = Resources.Load<Sprite>("Images/wine_7_1");
        menuDatas[7].menuImage = Resources.Load<Sprite>("Images/wine_7");

        menuDatas[8] = new MenuData();
        menuDatas[8].menuName = "wine_8";
        menuDatas[8].price = 12;
        menuDatas[8].makingTime = 12;
        menuDatas[8].menuOrderImage = Resources.Load<Sprite>("Images/wine_8_1");
        menuDatas[8].menuImage = Resources.Load<Sprite>("Images/wine_8");

        menuDatas[9] = new MenuData();
        menuDatas[9].menuName = "wine_9";
        menuDatas[9].price = 12;
        menuDatas[9].makingTime = 12;
        menuDatas[9].menuOrderImage = Resources.Load<Sprite>("Images/wine_9_1");
        menuDatas[9].menuImage = Resources.Load<Sprite>("Images/wine_9");

        menuDatas[10] = new MenuData();
        menuDatas[10].menuName = "wine_10";
        menuDatas[10].price = 12;
        menuDatas[10].makingTime = 12;
        menuDatas[10].menuOrderImage = Resources.Load<Sprite>("Images/wine_10_1");
        menuDatas[10].menuImage = Resources.Load<Sprite>("Images/wine_10");

        menuDatas[11] = new MenuData();
        menuDatas[11].menuName = "wine_11";
        menuDatas[11].price = 12;
        menuDatas[11].makingTime = 12;
        menuDatas[11].menuOrderImage = Resources.Load<Sprite>("Images/wine_11_1");
        menuDatas[11].menuImage = Resources.Load<Sprite>("Images/wine_11");
    }

    //주문 가능한 메뉴 데이터 중에 랜덤으로 반환
    public MenuData GetRandomMenuData()
    {
        int randomIdx = Random.Range(0,menuDatas.Length);
        return menuDatas[randomIdx];
    }

}

[System.Serializable]
public class MenuData
{
    public string menuName;
    public int price;
    public float makingTime;
    public Sprite menuOrderImage;
    public Sprite menuImage;
}
