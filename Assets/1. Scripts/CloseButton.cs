using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{

    public GameObject shopCanvas;
    public void OnClickButton()
    {
        shopCanvas.SetActive(false);

    }


}
