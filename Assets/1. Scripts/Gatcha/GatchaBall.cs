using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaBall : MonoBehaviour
{
    public Rigidbody2D rgdy;
    public GatchaType gatchaType;
    public string key;

    public virtual void SetGatchaBall(string key)
    {
        this.key = key;
    }

    public void ReceiveReward()
    {
        if (gatchaType == GatchaType.Customer)
        {
            //key에 해당하는 손님 획득
            User.Instance.AddCustomer(key); // **

        }

        else if (gatchaType == GatchaType.Acon)
        {
            int coinReward = 5 + User.Instance.userData.level * 2;
            User.Instance.AddCoin(coinReward);
            //도토리 숫자 정의
            //MainQuestManager.Instance.curQuestIndex
            //*손님인형과 도토리가 인형뽑기 안에 쌓이게 하기 숫자대, 위치 지정
        }


        //else if (gatchaType == GatchaType.Wine)
        //{
        //    User.Instance.AddWine(key);
        //    //도토리 숫자 정의
        //    //MainQuestManager.Instance.curQuestIndex
        //    //*손님인형과 도토리가 인형뽑기 안에 쌓이게 하기 숫자대, 위치 지정
        //} ** 유저에 와인 추가하기 
    }

}


public enum GatchaType
{
Wine,
Customer,
Acon,
//Wine,
}