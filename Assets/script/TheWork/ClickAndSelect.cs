using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;


public class ClickAndSelect : MonoBehaviour
{
    [SerializeField] private GameObject target,tableContentParent,content;
    [SerializeField] private Stack<GameObject> contents;
    [SerializeField] private TMP_Text TMPro;

    void Start()
    {
        contents = new Stack<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // click and select the object
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                target = hit.collider.gameObject;
                generateContent();

            }
        }
    }

    public void generateContent() {
        if (target != null) { 
            CityDataCenter targetCityDataCenter = target.GetComponent<CityDataCenter>();
            while (contents.Count > 0)
            {
                Destroy(contents.Pop());
            }

            foreach (peopleAttribute people in targetCityDataCenter.getPeopleList())
            {
                CityDataRecord cityDataRecord = target.GetComponent<CityDataRecord>();

                Assert.IsNotNull(cityDataRecord);
                GameObject tempContent = Instantiate(content, tableContentParent.transform);
                GameObject peopleObject = new GameObject();
                peopleObject.AddComponent<peopleAttribute>();
                contents.Push(peopleObject);
                tempContent.GetComponent<UITableContent>().mapping(people);
                if (ifJohnDoe(people))
                {
                    tempContent.GetComponent<UITableContent>().changeColor(1);
                }
                else
                {
                    tempContent.GetComponent<UITableContent>().changeColor(2);
                }
                contents.Push(tempContent);
            }
        }
    }

    private bool ifJohnDoe(peopleAttribute people) {
        return people.getFistName() == "John" && people.getLastName() == "Doe";
        
    }
}
