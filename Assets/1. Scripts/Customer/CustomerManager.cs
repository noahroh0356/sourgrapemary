using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public Transform enterance; // 입구 위치
    public CustomerData[] customerDatas;
    public List<Customer> customerPrefabs = new List<Customer>(); // 손님의 종 리스트
    public List<Customer> waitingCustomers = new List<Customer>(); //대기 손님 
    public RestaurantManager restaurantManager;
    public static CustomerManager Instance;

    public Image inviteGageBar;

    //public Customer normalCustomerPrefab;
    //public Customer obCustomerPrefab;
    

    private void Awake()
    {
        Instance = this; //this 자신의 객체 = User 스크립
    }


    //public void JinsangCustomer()

    //{
    //    Debug.Log("진상손님소환");
    //    Customer newJinsangObject = Instantiate(obCustomerPrefab, enterance.position, Quaternion.identity);
    //    obCustomerPrefab.Enter();
    //}



    public void EnterCustomer()
    {
        if (waitingCustomers.Count > 5)
        {
            return;
        }
        MainQuestManager.Instance.DoQuest(MainQuestType.CallCustomer);
        int randomIndex = Random.Range(0, customerPrefabs.Count);
        Customer newCustomerObject = Instantiate(customerPrefabs[randomIndex], enterance.position, Quaternion.identity);


        Customer newCustomer = newCustomerObject.GetComponent<Customer>();
        newCustomer.Enter();
        waitingCustomers.Add(newCustomer);
        SortCustomer();
        AssignCustomerToTable();
    }

    private void Update()
    {
    }

    // 빈 테이블 찾기 (RestaurantManager와 연동)
    private Table GetRandomAvailableTable()
    {
        return restaurantManager.GetRandomAvailableTable();
    }

    public void AssignCustomerToTable() 
    {
        if (waitingCustomers.Count > 0)
        {
            Table table = GetRandomAvailableTable();
            if (table != null)
            {
                Customer customer = waitingCustomers[0];
                customer.SetTarget(table);
                waitingCustomers.RemoveAt(0);
            }
        }
    }

    void SortCustomer()
    {
        for (int i = 0; i < waitingCustomers.Count; i++)
        {
            float randomX = Random.Range(-0.3f, 0.3f);
            float plusY = i*0.6f;
            waitingCustomers[i].transform.position = (Vector2)enterance.position + new Vector2(randomX, plusY);
            // **기존 애들 포지션도 바뀌는데 왜 그?
        }

    }
}

[System.Serializable]
public class CustomerData
{
    public string key;
    public int order;
    public Sprite thum;


}
