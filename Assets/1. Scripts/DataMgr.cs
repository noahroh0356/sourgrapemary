using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    void Start()
    {
        int coin = PlayerPrefs.GetInt("coin", 0);

        string item0 = PlayerPrefs.GetString("item0", "false");
        string item1 = PlayerPrefs.GetString("item1", "false");

        Debug.Log("coin : " + coin);
        Debug.Log("item0 : " + coin);
        Debug.Log("item1 : " + coin);

        int newnew = PlayerPrefs.GetInt("new", 20);
        Debug.Log("new : " + newnew);

        Debug.Log("데이터 불러오기 완료");
    }

    void Update()
    {
        
    }
}
