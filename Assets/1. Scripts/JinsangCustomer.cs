using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JinsangCustomer : MonoBehaviour
{
    // 레스토랑에 들어옴 ,

    public float moveSpeed= 3f;

    //public Transform spawn;
    public static JinsangCustomer Instance;

    public Transform enterancePosition;


    public void Awake()
    {
        Instance = this;
    }

    //public void Spawn()

    //{
    //    Vector3 spawnPosition = spawn.position;
    //    transform.position = spawnPosition;

    //    //StartCoroutine(MoveToEnterance());

    //    //어디선가 생성될텐데 거기서부터 입구까지 들어오게 처
    //}


    public virtual void Enter()
    {
        transform.position = CustomerManager.Instance.enterance.position;
        StartCoroutine(CoPickUpCoin());
        Debug.Log("진상짓시작");
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
            Coin closestCoin = coins[0];

            while (true)
            {
                if (closestCoin == null || closestCoin.gameObject.activeSelf || closestCoin.isPicked)
                {
                    closestCoin = null;
                    break;
                }

                if (Vector2.Distance(transform.position, closestCoin.transform.position) < 0.3f)
                {
                    break;
                }
                transform.position =
                    Vector2.MoveTowards(transform.position, closestCoin.transform.position, Time.deltaTime * moveSpeed);

                yield return null;
            }   
            closestCoin.DestroyAcon();
        }


    }



    public void Exit()
    {

    }



}
