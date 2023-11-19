using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        GameManager.Instance.playerMaxHealth = 7;
        GameManager.Instance.playerHealth = 7;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet") {
            GameManager.Instance.playerHealth--;
            if (GameManager.Instance.playerHealth == 0) {
                Destroy(gameObject);
            }
        }
    }

}
