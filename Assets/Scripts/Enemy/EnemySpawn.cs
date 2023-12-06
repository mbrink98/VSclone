using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float time = 0f;
    private Vector2 pos = new Vector2(0, 0);

    public float SpawnTime = 5f;
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
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                time += Time.deltaTime;
                if (time >= SpawnTime)
                {
                    time = time % 1f;
                    Instantiate(enemyPrefab, new Vector2(pos.x + Random.Range(-1.5f, 1.5f), pos.y + Random.Range(-1.5f, 1.5f)), Quaternion.identity);
                }
            }
        }
    }
}
