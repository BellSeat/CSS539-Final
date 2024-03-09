using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;

public class CityDataCenter : MonoBehaviour
{

    [SerializeField] private LinkedList<peopleAttribute> peopleList, tempPeopleList;
    [SerializeField] private float clock, clockFreshTime;
    [SerializeField] private StreamWriter writer, hitWriter;
    [SerializeField] private string recordPath, hitPath;
    // Start is called before the first frame update
    void Start()
    {
        peopleList = new LinkedList<peopleAttribute>();
        tempPeopleList = new LinkedList<peopleAttribute>();

        recordPath = "./Assets/Resources/CSV/PeopleData_" + gameObject.name + ".csv";
        hitPath = "./Assets/Resources/CSV/Hit" + gameObject.name + "Rate.csv";
        if (File.Exists(recordPath)) { 
            File.Delete(recordPath);
        }

        if (File.Exists(hitPath))
        {
            File.Delete(hitPath);
        }
        writer = File.CreateText(recordPath) ;
        hitWriter = File.CreateText(hitPath);
        writer.Flush();
        hitWriter.Flush();
        writer.WriteLine("id,firstName,lastName,age");
        hitWriter.WriteLine("hitRate");
        writer.Flush();
        hitWriter.Flush();
        writer.Close();
        hitWriter.Close();
        clock = 10;
        clockFreshTime = 3;
    }

    // Update is called once per frame
    void Update()
    {
        clock -= Time.deltaTime;
        if (clock <= 0) { 
            Debug.Log("city_"+gameObject.name+": Write to CSV");
            clock = clockFreshTime;
            if (tempPeopleList != null && tempPeopleList.Count > 0) { 
                // write the tempPeopeList to peopleList.vcs then clear  the tempPeopleList
                writeToCSV();
            }
            float hitRate = calculateHitRate(); 
            Debug.Log("city_" + gameObject.name + ": hitRate: " + hitRate);
            hitWriter = File.AppendText(hitPath);
            hitWriter.WriteLine(hitRate);
            hitWriter.Flush();
            hitWriter.Close();
            tempPeopleList.Clear();
            tempPeopleList = new LinkedList<peopleAttribute>(peopleList);
        }
    }

    public void AddPeople(peopleAttribute people)
    {
        peopleList.AddLast(people);
        Assert.IsNotNull(tempPeopleList);
        tempPeopleList.AddLast(people);
    }

    public void printList() { 
        foreach (peopleAttribute people in peopleList)
        {
            Debug.Log(people.id);
        }
    }

    public List<peopleAttribute> getPeopleList()
    {
        List<peopleAttribute> peopleList = new List<peopleAttribute>();
        foreach (peopleAttribute people in this.peopleList)
        {
            peopleList.Add(people);
        }
        return peopleList;
    }




    public void writeToCSV()
    {
        writer = File.AppendText(recordPath);
        foreach (peopleAttribute people in tempPeopleList)
        {
            writer.WriteLine(people.id + "," + people.getFistName() + "," +people.getLastName()+","+ people.getAge() + ",");
        }
        writer.Flush();
        writer.Close();
    }

    public float calculateHitRate() { 
        float hit = 0;
        float size = 0;
        if (peopleList.Count <= 0) {
            return 0.0f;
        }
        foreach (peopleAttribute people in peopleList)
        {
            size++;
            if (people.getFistName() != "John" && people.getLastName() != "Doe") {
                hit++;
                
            }
        }
        return (float) (hit / size);
    }
}
