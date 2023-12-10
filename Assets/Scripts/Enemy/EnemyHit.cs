using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private GameObject expPrefab;
    private bool canBeHit = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && canBeHit) {
            canBeHit = false;
            SoundManager.Instance.playEnemyHit();
            Instantiate(expPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
