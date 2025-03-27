using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaButton : MonoBehaviour
{
    public GameObject[] panels;
    public GameObject activePanel;
    public GatchaManager gatchaManager;

    public GameObject bgmObject; // "BGM" 오브젝트
    public GameObject gatchaBgmObject; // "gatchabgm" 오브젝트

    public void OnClickButton()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        activePanel.gameObject.SetActive(true);
        gatchaManager.StartGacha();

        if (bgmObject != null)
        {
            bgmObject.SetActive(false);
        }

        if (gatchaBgmObject != null)
        {
            gatchaBgmObject.SetActive(true);
        }

    }

}

