using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CityDataRecord : MonoBehaviour
{
    [SerializeField] private LFUPeople _people;
    [SerializeField] private CityDataCenter _cityDataCenter;
    [SerializeField] private int clock;
    [SerializeField] private peopleAttribute tempAtrribute;
    // Start is called before the first frame update
    void Start()
    {
        _people = new LFUPeople(150);
        _cityDataCenter = transform.GetComponent<CityDataCenter>();
        Assert.IsNotNull(_cityDataCenter);
        clock = 20;
        tempAtrribute = new peopleAttribute();
        tempAtrribute.setAttribute(0, 999, 99999, "John", "Doe");
        tempAtrribute.setAddress(new Address(99999, "We have known you", "We have known you", "We have known you"));

    }

    void Update()
    {
        
    }

    // Update is called once per frame
    public void AddPeople(peopleAttribute people) {
        if (!ifContains(people.id)) { 
            _people.put(people.id, people);
            _cityDataCenter.AddPeople(people);
            peopleAttribute tmp = _people.Get(people.id);
            return;
        }
        tempAtrribute.SetId(people.getId());
        _cityDataCenter.AddPeople(tempAtrribute);
    }

    // if the people usually went this city, then the frequency will increase, and the people color should be changed to black. 
    public bool ifContains(int id)
    {
        return _people.containsKey(id);
    }

    public int getPeopleFrequency(int id)
    {
        return _people.FrequencyList[id].Count;
    }

    
}


public class CityLogger { 
    
}

public class Data { 
    public peopleAttribute peopleAttribute { get; set; }
    public float time { get; set; }


}


public class  LFUPeople
{
    private Dictionary<int, PeopleEntity> _people;
    public Dictionary<int, LinkedList<PeopleEntity>> FrequencyList;
    private int _capacity;
    private int _minFrequency;

    public LFUPeople(int capacity)
    {
        _capacity = capacity;
        _minFrequency = 0;
        _people = new Dictionary<int, PeopleEntity>();
        FrequencyList = new Dictionary<int, LinkedList<PeopleEntity>>();
       
    }

    public peopleAttribute Get(int key) {
        
        if (!_people.ContainsKey(key)) { 
            return null;
        }
        var people = _people[key].peopleAttribute;
        return people;
    }

    public void put(int key, peopleAttribute value) {
       
        if (_capacity == 0) {
            Debug.Log("The capacity is 0");
            return;
        }
        var newPeople = new PeopleEntity() { id = key, peopleAttribute = value, Frequency = 0 };
       if (_people.ContainsKey(key))
        {
            Debug.Log("The people is already in the list");
            var oldPeople = _people[key];
            FrequencyList[oldPeople.Frequency].Remove(oldPeople);
            if (oldPeople.Frequency == _minFrequency && FrequencyList[oldPeople.Frequency].Count == 0)
            {
                _minFrequency++;
            }
            newPeople.Frequency = oldPeople.Frequency + 1;
        } else
        {
            // Debug.Log("The people is in the list");
            if (_people.Count == _capacity)
            {
                var oldPeople = FrequencyList[_minFrequency].First.Value;
                FrequencyList[_minFrequency].RemoveFirst();
                _people.Remove(oldPeople.id);
            }
            _people.Add(key, newPeople);
            _minFrequency = 0;
        }
        //printList();
    }

    public bool containsKey(int key)
    {
        return _people.ContainsKey(key);
    }

    public void printList()
    {
        foreach (var people in _people)
        {
            Debug.Log(people.Value.id);
        }
    }


}
public class PeopleEntity { 
    public int id { get; set; }
    public peopleAttribute peopleAttribute { get; set; }
    public int Frequency { get; set; }
}

