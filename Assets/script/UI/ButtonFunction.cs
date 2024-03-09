using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject ChartUI;
    [SerializeField] private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
       startButton = gameObject.GetComponent<Button>();
       startButton.onClick.AddListener(startButtonOnClick);
        startButtonOnClick();
        ChartUI.SetActive(false);
    }

    // Update is called once per frame


    void startButtonOnClick()
    {
        flag = !flag;
        ChartUI.SetActive(flag); 
    }
}
