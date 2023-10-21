using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float movementSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))  
        {  
            transform.Translate(0.0f, movementSpeed * 0.001f, 0.0f);    
        }  
         
        if (Input.GetKey(KeyCode.A))  
        {  
            transform.Translate(movementSpeed *-0.001f, 0f, 0.0f);   
        }  
         
        if (Input.GetKey(KeyCode.S))  
        {  
            transform.Translate(0.0f, movementSpeed *-0.001f,0.0f);  
        }  
        
        if (Input.GetKey(KeyCode.D))  
        {  
            transform.Translate(movementSpeed *0.001f, 0f, 0.0f);  
        }  
    }
}
