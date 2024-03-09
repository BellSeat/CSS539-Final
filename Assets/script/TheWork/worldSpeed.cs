using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class worldSpeed : MonoBehaviour
{

    [SerializeField] private float clockSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getClockSpeed()
    {
        return clockSpeed;
    }

    public void setClockSpeed(float value)
    {
        clockSpeed = value;
    }
}
