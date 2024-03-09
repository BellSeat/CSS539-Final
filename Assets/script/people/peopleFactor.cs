using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peopleFactor : MonoBehaviour
{
    [SerializeField] private GameObject cityListParent,homeListParent,Clock;
    [SerializeField] private float highFeqN, middleFeqN, lowFeqN;
    [SerializeField] private int humanCount;
    // Start is called before the first frame update
    void Start()
    {
        highFeqN = 0.6f;
        middleFeqN = 0.3f;
        lowFeqN = 0.1f;
        humanCount = 150;

        generatePeople();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject setHome() {
        GameObject home;
        home = homeListParent.transform.GetChild(Random.Range(0, homeListParent.transform.childCount)).gameObject;
        return home;
    }

    public void setDestination(GameObject[] highFeq, GameObject[] midFeq, GameObject[] lowFeq) {

        highFeq = new GameObject[3];
        midFeq = new GameObject[2];
        lowFeq = new GameObject[15];
        int hight = 0;
        int mid = 0;
        int low = 0;

        for (int i = 1; i < cityListParent.transform.childCount; i++) { 
            GameObject city = cityListParent.transform.GetChild(i).gameObject;
            float feq = Random.Range(0.0f, 1.0f);
            if (feq <= highFeqN) {
                if (hight < 5) {
                    highFeq[hight++] = city;
                    continue;
                }
            }else if (feq <= highFeqN + middleFeqN || hight >= 5 )
            {
                if (mid < 6)
                {
                    midFeq[mid++] = city;
                    continue;
                }
            }else if (low < 10)
            {
                lowFeq[low++] = city;
            }
            if (hight >= 5 && mid >= 6 && low >= 10)
            {
                if (feq > 0.5) {
                    int index = (int)Random.Range(1, 4);
                    if (index == 1) {
                        highFeq[hight++ % highFeq.Length] = city;
                        continue;
                    }if (index == 2) {
                        midFeq[mid++ % midFeq.Length] = city;
                        continue;   
                    }if (index == 3) {
                        lowFeq[low++ % lowFeq.Length] = city;
                    }
                }
            }
        }
    }

    public Address generateARandomAddress()
    {
        //Generate a random zip code in USA 
        System.Random random = new System.Random();
        int zipCode = random.Next(10000, 99999);

        //Generate a random street name
        string streetNumber = random.Next(1, 1000).ToString();
        string[] streetNames = { "Main St", "Elm St", "Oak St", "Pine St", "Cedar St", "Maple St", "Chestnut St", "Walnut St", "Washington St", "Lake St" };
        string street = streetNumber + " " + streetNames[random.Next(0, streetNames.Length)];

        

        //Generate a random city name
        string[] cityNames = { "New York", "Los Angeles", "Chicago", "Houston", "Phoenix", "Philadelphia", "San Antonio", "San Diego", "Dallas", "San Jose" };
        string city = cityNames[random.Next(0, cityNames.Length)];

        //Generate a random state name
        string[] stateNames = { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia" };
        string state = stateNames[random.Next(0, stateNames.Length)];


        Address address = new Address(zipCode,city,street,state);
        return address;
    }



    public void generatePeople()
    {
        for (int i = 0; i < humanCount; i++)
        {
            GameObject people = new GameObject("People" + i);
            
            Address address = generateARandomAddress();
            peopleAttribute peopleAttribute1= people.AddComponent<peopleAttribute>();
            GameObject Home = setHome();
            peopleAttribute1.setHome(Home);
            GameObject[] highFeq = new GameObject[5];
            GameObject[] midFeq = new GameObject[6];
            GameObject[] lowFeq = new GameObject[10];

            // set the destination for the people
            int hight = 0;
            int mid = 0;
            int low = 0;

            for (int j = 1; j < cityListParent.transform.childCount; j++)
            {
                Debug.Log("hight: " + hight + " mid: " + mid + " low: " + low + " j: " + j);
                GameObject city = cityListParent.transform.GetChild(j).gameObject;
                float feq = Random.Range(0.0f, 1.0f);
                if (feq <= highFeqN)
                {
                    if (hight < 5)
                    {
                        highFeq[hight++] = city;
                        continue;
                    }
                }
                if (feq <= highFeqN + middleFeqN || hight >= 5)
                {
                    if (mid < 6)
                    {
                        midFeq[mid++] = city;
                        continue;
                    }
                }
                if (low < 10)
                {
                    lowFeq[low++] = city;
                    continue;
                }
                if (hight >= 5 && mid >= 6 && low >= 10)
                {
                    break;
                }
            }


            peopleAttribute1.setDestination(highFeq, midFeq, lowFeq);
            peopleAttribute1.setAddress(address);
            
            System.Random random = new System.Random();
            int age = random.Next(1, 100);
            int health = random.Next(1, 100);
            //Generate a random fist name
            string[] firstNames = { "John", "Jane", "Bob", "Alice", "Tom", "Jerry", "Mickey", "Minnie", "Donald", "Daisy" };
            string firstName = firstNames[random.Next(0, firstNames.Length)];

            //Generate a random last name
            string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor" };
            string lastName = lastNames[random.Next(0, lastNames.Length)];
            int money = random.Next(1000, 500000);
            peopleAttribute1.setAttribute(age, health, money, firstName, lastName);
            peopleAttribute1.SetId(i);
            SpriteRenderer spriteRenderer = people.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("human");
            people.transform.parent = Home.transform;
            people.transform.position = Home.transform.position;
            people.transform.localScale = new Vector3(0.5f, 0.5f, 0);
            spriteRenderer.color = Color.black;
            spriteRenderer.sortingOrder = 9;
            peopleBehavior temp = people.AddComponent<peopleBehavior>();
            temp.setClock(Clock);

        }
    }
}
