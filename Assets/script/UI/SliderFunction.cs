using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class SliderFunction : MonoBehaviour
{
    [SerializeField] private worldSpeed worldSpeed;
    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(worldSpeed);
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { setWorldSpeed(); });
    }

    // Update is called once per frame

    public void setWorldSpeed()
    {
        worldSpeed.setClockSpeed(slider.value);
    }
    
}
