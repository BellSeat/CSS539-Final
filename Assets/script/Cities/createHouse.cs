using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createHouse : MonoBehaviour
{
    [SerializeField] private GameObject houseList;
    [SerializeField] private Sprite houseSprite;
    [SerializeField] private HashSet<Vector2> housePositiions;
    [SerializeField] private Color[] houseColors;
    [SerializeField] private int houseCount, windowsizeX, windowsizeY, houseSize;
    // Start is called before the first frame update
    void Start()
    {
        windowsizeX = 40;
        windowsizeY = 40;
        houseSize = 2;
        houseColors = new Color[3];
        houseCount = 50;
        houseColors[0] = new Color(0.0f, 0.5f, 0.0f);
        houseColors[1] = new Color(0.0f, 0.0f, 0.5f);
        houseColors[2] = new Color(0.5f, 0.0f, 0.0f);
        destoryBuilding();
        GenerateBuildings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check if the position is already taken
    bool isPositionTaken(Vector2 position)
    {
        return housePositiions.Contains(position);
    }

    // Generate a random position
    Vector2 GeneratePosition()
    {
        Vector2 position = new Vector2(Random.Range(-windowsizeX / houseSize, windowsizeX / houseSize) * houseSize, Random.Range(-windowsizeY / houseSize, windowsizeY / houseSize) * houseSize);
        while (isPositionTaken(position) && shouldPlace(position))
        {
            position = new Vector2(Random.Range(-windowsizeX / houseSize, windowsizeX / houseSize) * houseSize, Random.Range(-windowsizeY / houseSize, windowsizeY / houseSize) * houseSize);
        }
        housePositiions.Add(position);
        return position;
    }

    // if the position should be in center of the grid
    bool shouldPlace(Vector2 position)
    {
        float distance = Vector2.Distance(position, Vector2.zero);
        float probability = distance / (windowsizeX);
        return Random.Range(0.0f, 1.0f) < probability;
    }

    void GenerateBuildings() {
        housePositiions = new HashSet<Vector2>();
        destoryBuilding();
        for (int i = 0; i < houseCount; i++)
        {
            GameObject house = new GameObject("House" + i);
            house.transform.parent = houseList.transform;
            house.transform.position = GeneratePosition();
            house.transform.localScale = new Vector3(houseSize, houseSize, 0);
            house.AddComponent<SpriteRenderer>().sprite = houseSprite;
            //house.GetComponent<SpriteRenderer>().color = houseColors[Random.Range(0, houseColors.Length)];
        }
    }
    void destoryBuilding()
    {
        if (houseList.GetComponentsInChildren<Transform>() == null)
        {
            return;
        }
        foreach (Transform child in houseList.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
