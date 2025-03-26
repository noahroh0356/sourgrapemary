using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuButton : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject activePanel;
    public bool first;

    private void Start()
    {
        if (first)
        {
            OnClickButton();
        }
    }

    public void OnClickButton()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        activePanel.gameObject.SetActive(true);

    }
}
