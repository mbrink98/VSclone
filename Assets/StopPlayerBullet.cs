using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlayerBullet : MonoBehaviour
{
    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        velocity = GetComponent<Rigidbody2D>().velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameIsPaused)
        {
            GetComponent<Rigidbody2D>().velocity = velocity;
        } else {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
