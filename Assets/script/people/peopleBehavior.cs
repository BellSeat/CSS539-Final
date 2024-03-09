using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;


// each second refers to 0.5 hour
public class peopleBehavior : MonoBehaviour
{
    [SerializeField] private peopleAttribute myAtrribute;
    // action mode: 1: move to a random city, 2: stay in place, 3: move to home
    [SerializeField] private int actionMode;
    [SerializeField] private float duration = 1, timeSpeed = 4f, timeCount = 0, maximumR = 9f;
    [SerializeField] private bool movingToCity = false, movingToHome = false, staying = false;
    [SerializeField] private GameObject currentPlace,targetPlace,clock;

    // Start is called before the first frame update
    void Start()
    {
        myAtrribute = transform.GetComponent<peopleAttribute>();
        Assert.IsNotNull(myAtrribute);
        currentPlace = gameObject;
        targetPlace = currentPlace;
        timeCount = -1;
        duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (clock != null)
        {
            timeSpeed = clock.GetComponent<worldSpeed>().getClockSpeed();
        }
        System.Random random = new System.Random();

        // if not staying, do the action
        if (timeCount <= 0)
        {
            staying = false;
            movingToCity = false;
            movingToHome = false;
            
            currentPlace = targetPlace;
            actionMode = (int)random.Next(1, 4);
            // Debug.Log("actionMode: " + actionMode);
            duration = random.Next(10, 15);
            timeCount = duration;
            if (actionMode == 1)
            {
                movingToCity = true;
                movingToHome = false;
                targetPlace = myAtrribute.getARandomCity();
                while (targetPlace == currentPlace || targetPlace == null)
                {
                    targetPlace = myAtrribute.getARandomCity();
                }
                return;
            }
            if (actionMode == 2)
            {
                staying = true;
                movingToCity = false;
                movingToHome = false;
                return;
            }
            if (actionMode == 3)
            {
                movingToHome = true;
                movingToCity = false;
                targetPlace = myAtrribute.getHome();
                return;
            }

        }
        // Debug.Log("timeCount: " + timeCount);
        // if staying, do nothing
        if (staying && timeCount >= 0)
        {
            timeCount -= timeSpeed * Time.deltaTime * timeSpeed;
            return;
        }

        // if moving to city, do the action

        if (movingToCity && timeCount >= 0) { 
            if (duration <= 0) { 
                movingToCity = false;
                return;
            }
            if (targetPlace == null) { 
                Debug.Log("targetPlace is null");
            }
            if (currentPlace == null)
            {
                Debug.Log("currentPlace is null");
            }
            float progress = (duration - timeCount) / duration;


            timeCount -= timeSpeed * Time.deltaTime * timeSpeed;
            transform.position = (targetPlace.transform.position - currentPlace.transform.position) * ( (duration - timeCount) / duration ) + currentPlace.transform.position;
            float messUpRadius = (1 - (float)Mathf.Cos((duration - timeCount) / duration * Mathf.PI)) * maximumR;
            if (timeCount <= 0) { 
                CityDataRecord cityDataRecord = targetPlace.GetComponent<CityDataRecord>();
                Assert.IsNotNull(cityDataRecord);
                cityDataRecord.AddPeople(myAtrribute);
            }
            transform.position += new Vector3((float)Random.Range(-messUpRadius, messUpRadius), (float)Random.Range(-messUpRadius, messUpRadius), 0) * Time.deltaTime;
            return;
        }

        // if moving to home, do the action
        if (movingToHome && timeCount >= 0)
        {
            timeCount -= timeSpeed * Time.deltaTime * timeSpeed;
            transform.position = (targetPlace.transform.position - currentPlace.transform.position) * ((duration - timeCount) / duration) + currentPlace.transform.position;
            float messUpRadius = (1 - (float)Mathf.Cos((duration - timeCount) / duration * Mathf.PI)) * maximumR;
            transform.position += new Vector3((float)Random.Range(-messUpRadius, messUpRadius), (float)Random.Range(-messUpRadius, messUpRadius), 0) * Time.deltaTime;
            return;
        }
    }

    public void setClock(GameObject clock)
    {
        this.clock = clock;
    }

}
