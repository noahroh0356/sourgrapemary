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
        menuDatas = new MenuData[2];

        menuDatas[0] = new MenuData();
        menuDatas[0].menuName = "wine_0";
        menuDatas[0].price = 10;
        menuDatas[0].makingTime = 10;
        menuDatas[0].menuImage = Resources.Load<Sprite>("Images/wine_0");

        menuDatas[1] = new MenuData();
        menuDatas[1].menuName = "wine_1";
        menuDatas[1].price = 12;
        menuDatas[1].makingTime = 12;
        menuDatas[1].menuImage = Resources.Load<Sprite>("Images/wine_1");
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
    public Sprite menuImage;
}
