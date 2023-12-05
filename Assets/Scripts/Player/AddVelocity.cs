using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddVelocity : MonoBehaviour
{
    [SerializeField]
    Vector2 v2force;
    [SerializeField]
    float Speed;
    [SerializeField]
    KeyCode KeyForward;
    [SerializeField]
    KeyCode KeyBackwards;
    [SerializeField]
    KeyCode KeyLeft;
    [SerializeField]
    KeyCode KeyRight;
    [SerializeField]
    float turnspeed;
    Vector2 v2inv;
    public Transform transform;
    public Rigidbody2D rb;
    void Start() {
           rb = GetComponent<Rigidbody2D>();
           transform = GetComponent<Transform>(); 
        }

    void Update()
    {
        
        if(Input.GetKey(KeyForward)){
            //this.transform.position += transform.right * Speed;   Time.deltaTime
            rb.AddForce(transform.forward * Speed* Time.deltaTime);   //speed sollte eigentlich durch momentane velocity bestimm werden / addforce
        }
        if(Input.GetKey(KeyBackwards)){
            Vector2 back = -transform.forward.normalized;
            Debug.Log(back);
            Debug.Log("ayayay");
            //Vector2 backyback =new Vector2(transform.up.x,transform.up.y);
           rb.AddForce(back * Speed * 10* Time.deltaTime,ForceMode2D.Impulse); 
            Debug.Log("alles ok bruder");
        }
        if(Input.GetKey(KeyRight)){
            this.transform.Rotate(0,0,-turnspeed);
            Debug.Log("asdasd");
        }
        if(Input.GetKey(KeyLeft)){
           this.transform.Rotate(0,0,turnspeed);

        }
    }
}
