using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{

    [SerializeField]
    private float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Move()
    {
        Vector2 position = transform.position;

        if (Input.GetKey("left"))
        {
            position.x -= speed;
        }
        else if (Input.GetKey("right"))
        {
            position.x += speed;
        }
        else if (Input.GetKey("up"))
        {
            position.y += speed;
        }
        else if (Input.GetKey("down"))
        {
            position.y -= speed;
        }

        transform.position = position;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
