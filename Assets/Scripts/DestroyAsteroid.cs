using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAsteroid : MonoBehaviour
{
    [SerializeField] private GameObject expPrefab;
    private bool canBeHit = true;
    private int hp;
    Animator m_Animator;
    Collider2D m_Collider;
    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        m_Animator = gameObject.GetComponent<Animator>();
        m_Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && canBeHit) {
            canBeHit = false;
            hp-=1;
            if (hp == 0) {
                m_Animator.SetTrigger("explode");
                m_Collider.enabled = !m_Collider.enabled;
            }

        }
        canBeHit = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet" && canBeHit) {
            canBeHit = false;
            hp-=1;
            if (hp == 0) {
                m_Animator.SetTrigger("explode");
                m_Collider.enabled = !m_Collider.enabled;
            }
        }
        canBeHit = true;
    }

    public void Destroy()
    {
        SoundManager.Instance.playExplosion();
        Vector2 startPos = transform.position;
        for (int i = 0; i < 3; i++) 
            {
                Instantiate(expPrefab, new Vector2(startPos.x + Random.Range(-0.3f, 0.3f), startPos.y + Random.Range(-0.3f, 0.3f)), Quaternion.identity);
            }
        Destroy(gameObject);
    }
}
