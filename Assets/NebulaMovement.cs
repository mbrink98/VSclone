using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebulaMovement : MonoBehaviour
{
    [SerializeField] private float velocity;

    [SerializeField] private float wiggleVelocity;

    private Vector3 accuratePosition;

    private Vector3[] wiggleMovements;

    private Vector3 currentWiggle;

    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Awake(){
        accuratePosition = transform.position;
        wiggleMovements = new Vector3[16]{
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            new Vector3(1,1,0),
            new Vector3(1,-1,0),
            new Vector3(-1,1,0),
            new Vector3(-1,-1,0),
            new Vector3(.414f,1,0),
            new Vector3(-.414f,1,0),
            new Vector3(.414f,-1,0),
            new Vector3(-.414f,-1,0),
            new Vector3(1,.414f,0),
            new Vector3(-1,.414f,0),
            new Vector3(1,-.414f,0),
            new Vector3(-1,-.414f,0)
            };
        for (int i = 0; i < 16; i++){
            wiggleMovements[i] = wiggleVelocity * wiggleMovements[i].normalized;
        }
        currentWiggle = wiggleMovements[Random.Range(0,16)];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 accurateMovement = Time.deltaTime * velocity * Vector3.right;
        if (renderer.isVisible){
            Vector3 wiggle = Time.deltaTime * currentWiggle;
            if (!((transform.position + wiggle - accuratePosition).magnitude <= wiggleVelocity)){
                List<Vector3> possibleWiggles = new List<Vector3>();
                for (int i = 0; i < 16; i++){
                    if ((transform.position + wiggleMovements[i] - accuratePosition).magnitude <= wiggleVelocity){
                        possibleWiggles.Add(wiggleMovements[i]);
                    }
                }
                currentWiggle = possibleWiggles[Random.Range(0,possibleWiggles.Count)];
                wiggle = Time.deltaTime * currentWiggle;
            }
            transform.Translate(accurateMovement + wiggle);
        } else {
            transform.Translate(accurateMovement);
        }
        accuratePosition += accurateMovement;
        
        if (transform.position.x > MapManager.Instance.nebulaMaxX){
            Vector3 skipVec = MapManager.Instance.nebulaSkipBackAmount * Vector3.left;
            transform.Translate(skipVec);
            accuratePosition += skipVec;
        }
    }
}
