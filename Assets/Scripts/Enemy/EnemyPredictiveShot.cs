using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyPredictiveShot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed = 10f;

    public float attackSpeed = 0.5f; // 2 attack every 1 second
    private float time = 0f;
    [SerializeField] private float timeAhead = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameIsPaused)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                time += Time.deltaTime;
                Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
                Vector2 playerVel = (Vector2) playerRB.velocity;            //funktioniert nicht,da playermovement über transform geregelt wird
                //Debug.Log(playerVel);
                Vector2 futurePlayerPos = (Vector2)player.transform.position + playerVel * timeAhead;
                Vector2 vectorToFuturePlayerPos = futurePlayerPos - (Vector2)transform.position;


                if (attackSpeed < time)
                {
                    time -= attackSpeed;
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(vectorToFuturePlayerPos.normalized * bulletSpeed, ForceMode2D.Impulse);

                    Destroy(bullet, 10f);         //destroy bullet after 10s  //add image that shows cannonballs impact in water
                }
                
            }
        }
    }
}