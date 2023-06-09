using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    [SerializeField]
    private float speed = 0.05f;

    // Start is called before the first frame update


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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item(Demo)"))
        {
            SoundManager_SE.Instance.Play(1);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager_SE.Instance.Play(2);
        }
        if (other.gameObject.CompareTag("Enemy(Demo)"))
        {
            SoundManager_SE.Instance.Play(3);
        }
        if (other.gameObject.CompareTag("Coin(Test)"))
        {
            SoundManager_SE.Instance.Play(4);
        }

    }
}
