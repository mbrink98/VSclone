
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{

     public GameObject Bullet; // Reference to your prefab
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get vector to mouse position in world context
        Vector3 mousePositionScreen = Input.mousePosition;
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        Vector3 vectorToMouse = mousePositionWorld - transform.position;
        //spawn bullet to travel along vector

         if (Input.GetKeyDown(KeyCode.Space))
        
        {
            // Spawn the prefab at a specific position
        //pass den passenden vektor und lass die bullet traveln mit scritp auf bulletside
            //Instantiate(Bullet, transform, Quaternion.identity);
        }
    
        
    }
}
