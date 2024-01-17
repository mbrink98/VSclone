using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject meeleEnemyPrefab;
    public GameObject sniperEnemyPrefab;
    private float time = 0f;
    private Vector2 pos = new Vector2(0, 0);

    public float SpawnTime = 6f;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameIsPaused)
        {
            pos = transform.position;
            GameObject player = GameManager.Instance.player;
            float mapSize = (float) MapManager.Instance.size;
            if (player != null)
            {
                time += Time.deltaTime;
                if (time >= SpawnTime)
                {
                    time = time % 1f;
                    float x = pos.x + Random.Range(-1.5f, 1.5f);
                    float y = pos.y + Random.Range(-1.5f, 1.5f);
                    Vector3 myPos = new Vector3(x, y, 0);
                    while (
                        myPos.x < -mapSize || myPos.x > mapSize ||
                        myPos.y < -mapSize || myPos.y > mapSize ||
                        (myPos - player.transform.position).magnitude < 3f
                    ){
                        myPos -= 3 * myPos.normalized;
                    }
                    int spawn = Random.Range(0, 10);
                    if (spawn < 5) {
                        Instantiate(enemyPrefab, myPos, Quaternion.identity);
                    } else if (spawn >= 5 && spawn < 8) {
                        Instantiate(meeleEnemyPrefab, myPos, Quaternion.identity);
                    } else {
                        Instantiate(sniperEnemyPrefab, myPos, Quaternion.identity);
                    }
                    
                }
            }
        }
    }
}
