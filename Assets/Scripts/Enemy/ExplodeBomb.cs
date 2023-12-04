using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBomb : MonoBehaviour
{
    // Start is called before the first frame update
    private float time = 0f;
    public float FuseTime = 6.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

            if (time >= FuseTime){

                



            }
    }
}
