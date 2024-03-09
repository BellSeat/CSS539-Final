using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class moveBehavior : MonoBehaviour
{

    [SerializeField]private float speed = 5.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) {
            move(transform.up);
        }
        if (Input.GetKey("s"))
        {
            move(-transform.up);
        }
        if (Input.GetKey("a"))
        {
            move(-transform.right);
        }
        if (Input.GetKey("d"))
        {
            move(transform.right);
        }
    }

    public void move(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
