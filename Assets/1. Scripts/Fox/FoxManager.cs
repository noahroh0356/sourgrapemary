using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FoxManager : MonoBehaviour
{

    public List<FoxData> foxes = new List<FoxData>();
    public Customer customer;


    public static FoxManager Instance;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateFox();
    }

    void Update()
    {

    }

    public FoxData GetFoxData(string key)
    {
        for (int i = 0; i < foxes.Count; i++)

        {
            if (foxes[i].key == key)
            {
                return foxes[i];
            }
        }
        return null;

    }

    public void UpdateFox()
    {
        if (User.Instance != null && User.Instance.userData != null && User.Instance.userData.userFoxes != null && foxes != null)
        {
            for (int i = 0; i < foxes.Count; i++)
            {
                if (foxes[i] != null) // null 체크 추가
                {
                    FoxData foxData = foxes[i].GetComponent<FoxData>();

                    if (foxData != null)
                    {
                        bool purchased = false;
                        for (int j = 0; j < User.Instance.userData.userFoxes.Count; j++)
                        {
                            if (foxData.key == User.Instance.userData.userFoxes[j].key && User.Instance.userData.userFoxes[j].open == true)
                            {
                                purchased = true;
                                break;
                            }
                        }
                        foxes[i].gameObject.SetActive(purchased);
                    }
                }
            }
            //if (userFoxes != null && foxes[i].key == userfoxes.key)
            //{
            //    foxes[i].gameObject.SetActive(true);
            //}
            //else
            //{
            //    foxes[i].gameObject.SetActive(false);
            //}
        }


    }



    public class FoxData : MonoBehaviour
    {
        public string key;
        public int price;
        public bool purchased;


    }
}

