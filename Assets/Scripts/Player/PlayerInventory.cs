using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> keys;
    public Text keyCount;
    public GameObject keyCountDisplay;
    public static PlayerInventory Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (keys.Count > 0)
        {
            keyCountDisplay.SetActive(true);
            keyCount.text = keys.Count.ToString();
        }
        else
        {
            keyCountDisplay.SetActive(false);
        }
    }
}
