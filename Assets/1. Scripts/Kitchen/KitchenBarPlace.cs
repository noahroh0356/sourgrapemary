using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenBarPlace : MonoBehaviour
{
    public KitchenBar[] kitchenBars;
    public KitchenBar curKitchenBar; // 설치된 키친바
    public KitchenPlaceType kichenPlaceType;

    public bool making = false;
    public void StartMake(MenuData menuData, Customer customer)
    {
        curKitchenBar.wineProgressBar.fillAmount = 1;        
        making = true;
        Debug.Log("StartMake");
        StartCoroutine(Cocomplete(menuData, customer));
    }

    IEnumerator Cocomplete(MenuData menuData, Customer customer)
    {
        float foxAbilityValue = FoxManager.Instance.GetFoxAbility(FoxAbilityType.KitchenSpeedUp)+1; // 빠르게 해줘야 해서 1더함 아니면 0.n이라 오히려 감소하게 
        KitchenData data = KitchenManager.Instance.GetKitchenData(curKitchenBar.key);
        float time = menuData.makingTime; //* (1 - data.reduceMakingTime);
        float timer = time;
        Debug.Log("StartMake1");

        //timer에 담긴 시간만큼 반복되는 코드
        while (true)
        {
            Debug.Log("StartMake2");
            if (timer <= 0)
                break;
            yield return null; // 한 프레임 시간 만큼 대기
            timer -= Time.deltaTime * (1 + data.reduceMakingTime) * foxAbilityValue; ; //한 프레임 간 시간 간격, 제작 속도 조
            curKitchenBar.wineProgressBar.fillAmount = timer/time;
        }

        //yield return new WaitForSeconds(timer);


        curKitchenBar.wineProgressBar.fillAmount = 0;
        customer.ReceiveMenu();

        making = false;

        KitchenManager.Instance.MatchOrder();
    }


    private void Awake()
    {
        kitchenBars = GetComponentsInChildren<KitchenBar>();
        curKitchenBar = kitchenBars[0];
    }

    public void UpdateKitchenBarPlace()
    {
        UserKitchen userKitchen = User.Instance.GetSetUpKitchen(kichenPlaceType);
        // 장소에 맞는 키친바를 활성화

        for (int i = 0; i < kitchenBars.Length; i++)
        {
            if (userKitchen != null && kitchenBars[i].key == userKitchen.kitchenkey)
            {
                curKitchenBar = kitchenBars[i];
                kitchenBars[i].gameObject.SetActive(true);
            }
            else
            {
                kitchenBars[i].gameObject.SetActive(false);
            }
        }

    }

}
