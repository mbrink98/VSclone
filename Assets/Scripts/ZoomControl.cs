using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomControl : MonoBehaviour
{
    [SerializeField] private float zoomsize = 8f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel")> 0){
            zoomsize -= 1;
        }



        if(Input.GetAxis("Mouse ScrollWheel")< 0){
            zoomsize += 1;
        }
        GetComponent<Camera>().orthographicSize = zoomsize;
    }
}
