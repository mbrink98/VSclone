using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hit : MonoBehaviour
{
    Animator cameraAnimator;
    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy") {
            m_Animator.SetTrigger("red");
            SoundManager.Instance.playEnemyHit();
            GameManager.Instance.playerHealth--;
            if (GameManager.Instance.playerHealth == 0) {
                Destroy(gameObject);
                GameManager.Instance.gameOver();
            }
        }
    }

}
