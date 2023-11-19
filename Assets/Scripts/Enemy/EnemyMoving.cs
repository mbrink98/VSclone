using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    public float movementSpeed = 1.0f;
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
                var step = movementSpeed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
                transform.up = -(player.transform.position - transform.position);
            }
        }
    }
}
