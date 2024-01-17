using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBulletHit : MonoBehaviour
{
    private HashSet<string> collidingTags = new HashSet<string>();
    // Start is called before the first frame update
    void Start()
    {
        collidingTags.Add("PlayerBullet");
        collidingTags.Add("EnemyBullet");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collidingTags.Contains(collision.gameObject.tag)){
            Destroy(collision.gameObject);
        }

    }
}
