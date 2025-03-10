using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FurnitureManager : MonoBehaviour
{

    public List<FurnitureData> furnitures = new List<FurnitureData>();
    public TablePlace[] tablePlaces; // 테이블 위치
    public Customer customer;
 

    public static FurnitureManager Instance;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        tablePlaces = FindObjectsOfType<TablePlace>();
        
    }

    void Update()
    {

    }

    public void UpdateFurniture()
    {
        //모든 가구를 업데이트
        for (int i = 0; i < tablePlaces.Length; i++)
        {
            tablePlaces[i].UpdateTablePlace();
        }
        // 테이블이 추가될때 settarget
    }

    public FurnitureData GetFurnitureData(string key)
    {
        for (int i = 0; i < furnitures.Count; i++)

        {
            if (furnitures[i].key == key)
            {
                return furnitures[i];
            }
        }
        return null;

    }

}



[System.Serializable]
public class FurnitureData
{
    public string key;
    public string nextfurniturekey;
    public int price;
    public FurniturePlace furniturePlace;
    public bool purchased;

}

public enum FurniturePlace
{
    table0,
    table1,
    table2,
    table3,
    table4,
    table5
} 