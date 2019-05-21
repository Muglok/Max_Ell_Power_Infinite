using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableController : MonoBehaviour
{
    public GameObject uiInfo;

    public void SetActive()
    {
        uiInfo.SetActive(true);
    }

    public void SetUnactive()
    {
        uiInfo.SetActive(false);
    }
}
