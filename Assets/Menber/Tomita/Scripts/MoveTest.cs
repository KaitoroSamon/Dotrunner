using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class MoveTest : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        if (x > 0)
        {
            rigidBody.AddForce(transform.right * 1.0f);
        }
        else if (x < 0)
        {
            rigidBody.AddForce(-transform.right * 1.0f);
        }
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
