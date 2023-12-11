using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFadeOut : MonoBehaviour
{
    private Vector3 spawnLocation;
    [SerializeField] private float maxDist = 30f;
    private float nextActionTime = 1.0f;
    public float period = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            if (Vector3.Distance(spawnLocation, transform.position) > maxDist)
            {
                Destroy(gameObject);
            }
        }
    }
}
