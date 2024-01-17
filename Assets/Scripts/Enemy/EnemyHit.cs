using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private GameObject expPrefab;
    private bool canBeHit = true;
    private int hp;
    private int maxHp;
    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = GameManager.Instance.enemyMaxHealth;
        hp = maxHp;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "PlayerBullet" || collision.gameObject.tag == "Player") && canBeHit) {
            canBeHit = false;
            m_Animator.SetTrigger("red");
            SoundManager.Instance.playEnemyHit();
            hp-=1;
            if (hp == 0) {
                Vector2 startPos = transform.position;
                for (int i = 0; i < maxHp; i++) 
                    {
                        Instantiate(expPrefab, new Vector2(startPos.x + Random.Range(-0.2f, 0.2f), startPos.y + Random.Range(-0.2f, 0.2f)), Quaternion.identity);
                    }
                Destroy(gameObject);
            }
        }
        canBeHit = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && canBeHit) {
            canBeHit = false;
            m_Animator.SetTrigger("red");
            SoundManager.Instance.playEnemyHit();
            hp-=1;
            if (hp == 0) {
                Vector2 startPos = transform.position;
                for (int i = 0; i < maxHp; i++) 
                    {
                        Instantiate(expPrefab, new Vector2(startPos.x + Random.Range(-0.2f, 0.2f), startPos.y + Random.Range(-0.2f, 0.2f)), Quaternion.identity);
                    }
                Destroy(gameObject);
            }
        }
        canBeHit = true;
    }
}
