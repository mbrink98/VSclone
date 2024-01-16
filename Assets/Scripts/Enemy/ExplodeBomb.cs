using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodeBomb : MonoBehaviour
{
    // Start is called before the first frame update
    private float time = 0f;
    public float fuseTime = 3.0f;
    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float ExplosionForceMulti = 5000;
    [SerializeField] private float ExplosionRadius = 5250;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
            if (time >= fuseTime){
                Explode();
                Debug.Log("exploded");
            }
    }
    private void OnDrawGizmos(){

        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);

    }
    void Explode(){

     inExplosionRadius = Physics2D.OverlapCircleAll(transform.position,ExplosionRadius);
                
                foreach(Collider2D o in inExplosionRadius){
                    Rigidbody2D o_rigidbody = o.GetComponent<Rigidbody2D>();
                    if(o_rigidbody != null){
                            Vector2 distance = o.transform.position-transform.position;
                            if(distance.magnitude > 0){
                                float explosionForce = ExplosionForceMulti /distance.magnitude;
                                o_rigidbody.AddForce(distance.normalized * explosionForce, ForceMode2D.Impulse);

                                if(o.gameObject.tag == "Player"){
                                    //hit player
                                }
                            }
                    } 
                
                }
                //animation
                Destroy(gameObject);
    }
}
