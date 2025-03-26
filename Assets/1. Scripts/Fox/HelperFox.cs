using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperFox : Fox
{
    //줍기 + More Tip
    public float moveSpeed;
    //Enter()에 레스토랑에 등장 + 동전을 줍기 시작
    public override void Enter()
    {
        base.Enter(); // base는 this의 부모객체 버전 
        transform.position = new Vector3(5, 0);
        StartCoroutine(CoPickUpCoin());
    }

    IEnumerator CoPickUpCoin()
    {

        while (true)
        {
            Coin[] coins = FindObjectsOfType<Coin>();
            if (coins.Length <= 0)
            {
                yield return new WaitForSeconds(1f);
                continue;
            }
            //가장 가까운 코인 찾기
            Coin closetCoin = coins[0];

            while (true)
            {
                if (closetCoin == null || closetCoin.gameObject.activeSelf || closetCoin.isPicked)
                {
                    closetCoin = null;
                    break;
                }

                if (Vector2.Distance(transform.position, closetCoin.transform.position) < 0.3f)
                {
                    break;
                }
                transform.position =
                    Vector2.MoveTowards(transform.position, closetCoin.transform.position, Time.deltaTime * moveSpeed);

                yield return null;
            }
            closetCoin.Picked();
        }


    }

}
