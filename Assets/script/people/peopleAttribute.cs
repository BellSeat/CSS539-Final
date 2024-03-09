using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Address
{
    private int zipCode;
    private string city;
    private string street;
    private string state;

    public Address(int zipCode, string city, string street, string state)
    {
        this.zipCode = zipCode;
        this.city = city;
        this.street = street;
        this.state = state;

    }

    public int getZipCode()
    {
        return zipCode;
    }
    public string getCity()
    {
        return city;
    }
    public string getStreet()
    {
        return street;
    }
    public string getState()
    {
        return state;
    }


    public string getAddress()
    {
        return zipCode + " " + city + " " + street + " " + state ;
    }

}
public class peopleAttribute : MonoBehaviour
{
    [SerializeField] private GameObject home;
    [SerializeField] private GameObject[] highFeqPosition, middleFeqPosition, lowFeqPosition;
    [SerializeField] private float highFeq, middleFeq, lowFeq;
    [SerializeField] private int age;
    [SerializeField] private int health;
    [SerializeField] private int money;
    [SerializeField] private string fistName, lastName;
    [SerializeField] public int id;
    [SerializeField] private Address address;
    // Start is called before the first frame update

    
    void Start()
    {
        highFeq = 0.8f;
        middleFeq = 0.15f;
        lowFeq = 0.05f;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHome(GameObject home)
    {
        this.home = home;
    }

    public void walking() {
        int feqAarry = hit();
        if (feqAarry == 2)
        {
            int index = Random.Range(0, lowFeqPosition.Length);
            transform.position = lowFeqPosition[index].transform.position;
        }
        else if (feqAarry == 1)
        {
            int index = Random.Range(0, middleFeqPosition.Length);
            transform.position = middleFeqPosition[index].transform.position;
        }
        else
        {
            int index = Random.Range(0, highFeqPosition.Length);
            transform.position = highFeqPosition[index].transform.position;
        }
    }


    //low freq represents 0, middle freq represents 1, high freq represents 2
    public int hit() { 
        float random = Random.Range(0.0f, 1.0f);
        if (random < highFeq) { 
            return 2;
        }
        else if (random < highFeq + middleFeq)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void setAttribute(int age, int health, int money, string fistName, string lastName)
    {
        this.age = age;
        this.health = health;
        this.money = money;
        this.fistName = fistName;
        this.lastName = lastName;
    }

    public void setDestination(GameObject[]highFeqB, GameObject[]midFeqB, GameObject[]lowFegB) { 
        highFeqPosition = highFeqB;
        middleFeqPosition = midFeqB;
        lowFeqPosition = lowFegB;
    }

    public GameObject getHome()
    {
        return home;
    }

    public GameObject getARandomCity() {
        GameObject theTarget = null;
        while (theTarget == null)
        {
            int feqAarry = hit();
            if (feqAarry == 2)
            {
                int index = Random.Range(0, lowFeqPosition.Length);
                theTarget = lowFeqPosition[index];
            }
            else if (feqAarry == 1)
            {
                int index = Random.Range(0, middleFeqPosition.Length);
                theTarget = middleFeqPosition[index];
            }
            else
            {
                int index = Random.Range(0, highFeqPosition.Length);
                theTarget = highFeqPosition[index];
            }
        }
        return theTarget;
    }

    public void SetId(int id) { 
        this.id = id;
    }

    public int getId()
    {
        return id;
    }
    public int getAge() { return age; }
    public int getHealth() { return health; }
    public int getMoney() { return money; }
    public string getFistName() { return fistName; }
    public string getLastName() { return lastName; }

    public Address getAddress()
    {
        return address;
    }

    public void setAddress(Address address)
    {
        this.address = address;
    }

}
