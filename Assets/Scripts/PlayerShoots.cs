

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float bulletSpeed = 10f;
    public float attackDelay = 1;
    public int ammo = 3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            StartCoroutine(Shoot());
        }


    }

    IEnumerator Shoot()
    {
        //get vector to mouse position in world context
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        ammo -=1;
        yield return new WaitForSeconds(attackDelay);

        Vector2 vectorToMouse = mousePositionWorld - (Vector2)transform.position;

        //spawn bullet to travel along vector
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(vectorToMouse.normalized * bulletSpeed, ForceMode2D.Impulse);
        ammo +=1;
        Destroy(bullet, 5f);         //destroy bullet after 5s
    }
}
