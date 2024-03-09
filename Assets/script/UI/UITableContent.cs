using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UITableContent : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text Id, firstName, lastName, zipCode, State, City,Street, Age, Money, Health;
    void Start()
    {
        Assert.IsNotNull(Id);
        Assert.IsNotNull(firstName);
        Assert.IsNotNull(lastName);
        Assert.IsNotNull(zipCode);
        Assert.IsNotNull(State);
        Assert.IsNotNull(City);
        Assert.IsNotNull(Street);
        Assert.IsNotNull(Age);
        Assert.IsNotNull(Money);
        Assert.IsNotNull(Health);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mapping(peopleAttribute data) {

        Id.text = data.id.ToString();
        firstName.text = data.getFistName();
        lastName.text = data.getLastName();
        Address address = data.getAddress();
        zipCode.text = address.getZipCode().ToString();
        State.text = address.getState();
        City.text = address.getCity();
        Street.text = address.getStreet();
        Age.text = data.getAge().ToString();
        Money.text = data.getMoney().ToString();
        Health.text = data.getHealth().ToString();
    }

    public void changeColor(int model) { 
        // if model == 1, the content should be black
        // if model == 2, then content should be red.
        transform.GetComponent<Image>().color = model == 1 ? Color.black : Color.red;
    }
}
