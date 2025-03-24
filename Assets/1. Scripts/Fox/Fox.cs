using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public string key;
    public GameObject[] foxObjects; // 활성화/비활성화할 Fox 객체 목록 (Inspector에서 할당)



    public virtual void Enter()
    {
        List<UserFox> userFoxes = User.Instance.GetSetUpFox();
        if (userFoxes != null && foxObjects != null)
        {
            for (int i = 0; i < foxObjects.Length; i++)
            {
                bool isActive = false;

                for (int j = 0; j < userFoxes.Count; j++)
                {
                    if (this.key == userFoxes[j].key) // 현재 Fox 객체의 key와 UserFox의 key를 비교
                    {
                        isActive = true;
                        break;
                    }
                }
                foxObjects[i].SetActive(isActive);
            }
        }
    }


}

