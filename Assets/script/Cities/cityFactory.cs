using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityFactory : MonoBehaviour
{
    [SerializeField] private GameObject cityList, houseList;
    [SerializeField] private Sprite citySprite, houseSprite;
    [SerializeField] private HashSet<Position> positions;
    [SerializeField] private Color[] colors;
    [SerializeField] private int cityCount, houseCount, windowsizeX, windowsizeY, citySize, houseSize;

    private struct Position{
        public float x, y;
        public Position(Vector2 vector2) { 
            x = vector2.x;
            y = vector2.y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position position &&
                   x == position.x &&
                   y == position.y;
        }

        public override int GetHashCode()
        {
            return (x, y).GetHashCode();
        }

        public static implicit operator Position(Vector2 v)
        {
            return new Position(v);
        }

        public static implicit operator Vector2(Position p)
        {
            return new Vector2(p.x, p.y);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        windowsizeX = 40;
        windowsizeY = 40;
        citySize = 4; 
        houseSize = 3;
        colors = new Color[3];
        cityCount = 40;
        houseCount = 80;
        colors[0] = new Color(0.0f, 0.5f, 0.0f);
        colors[1] = new Color(0.0f, 0.0f, 0.5f);
        colors[2] = new Color(0.5f, 0.0f, 0.0f);
        destoryBuilding();
        GenerateBuilding();
        peopleFactor peopleFactor = transform.GetComponent<peopleFactor>();
        peopleFactor.generatePeople();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 GeneratePosition(int buildingType) { 
        Vector2 position = new Vector2(Random.Range(-windowsizeX / citySize, windowsizeX / citySize) *citySize, Random.Range(-windowsizeY/ citySize, windowsizeY / citySize) * citySize);
        while (positions.Contains(position) && shouldPlace(position, buildingType))
        {
            position = new Vector2(Random.Range(-windowsizeX / citySize, windowsizeX / citySize) * citySize, Random.Range(-windowsizeY / citySize, windowsizeY / citySize) * citySize);
        }
        positions.Add(position);
        return position;
    }

    void GenerateBuilding() {
        positions = new HashSet<Position>();
        destoryBuilding();
        int buildingType = 0;
        for (int i = 0; i < cityCount; i++)
        {
            GameObject city = new GameObject("City" + i);
            city.transform.parent = cityList.transform;
            city.transform.position = GeneratePosition(buildingType);
            city.transform.localScale = new Vector3(citySize, citySize, 0);
            city.AddComponent<SpriteRenderer>().sprite = citySprite;
            city.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
            city.AddComponent<BoxCollider2D>();
            city.AddComponent<CityDataRecord>();
            city.AddComponent<CityDataCenter>(); 
        }
        buildingType = 1;
        for (int i = 0; i < houseCount; i++)
        {
            GameObject house = new GameObject("House" + i);
            house.transform.parent = houseList.transform;
            house.transform.position = GeneratePosition(buildingType);
            house.transform.localScale = new Vector3(houseSize, houseSize, 0);
            house.AddComponent<SpriteRenderer>().sprite = houseSprite;
            house.GetComponent<SpriteRenderer>().sortingOrder = 2;
            //house.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];
        }
    }

    bool shouldPlace(Vector2 position, int buildingType)
    {
        float distance = Vector2.Distance(position, Vector2.zero);
        float probability = (float) (distance  / (windowsizeX * 2));
        if (buildingType == 1) {
            // Debug.Log("distance: " + distance + "  |window size:" + windowsizeX);
            return distance > windowsizeX/2;
        }
        bool  isCity = Random.Range(0.0f, 1.0f) > probability;
        // Debug.Log("probability: " + probability + "  ---  " + buildingType);
        return buildingType == 0 ? isCity : !isCity;
    }
    void destoryBuilding()
    {
        if (cityList.GetComponentsInChildren<Transform>() == null) {
            return;
        }
        foreach (Transform child in cityList.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
