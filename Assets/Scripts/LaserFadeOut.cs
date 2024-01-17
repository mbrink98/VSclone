using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFadeOut : MonoBehaviour
{

    [SerializeField] private float laserTime = 2.0f;
    private float period = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > laserTime)
        {
            Destroy(gameObject);
        }
    }
}
