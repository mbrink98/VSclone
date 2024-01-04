using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Awake()
    {

    }

    float elapsed = 0f;
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameIsPaused)
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.playerAmmo > 0)
            {
                switch (GameManager.Instance.playerGun)
                {
                    case "Gun":
                        StartCoroutine(Shoot());
                        break;
                    case "Shotgun":
                        StartCoroutine(Shotgun());
                        break;
                    case "Laser":
                        StartCoroutine(Laser());
                        break;
                }
            }   


            elapsed += Time.deltaTime;
            if (elapsed >= GameManager.Instance.playerReloadSpeed)
            {
                elapsed = elapsed % 1f;
                if (GameManager.Instance.playerAmmo < GameManager.Instance.playerMaxAmmo)
                {
                    Reload();
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        GameManager.Instance.playerAmmo -= 1;
        yield return new WaitForSeconds(0);

        Vector2 vectorToMouse = mousePositionWorld - (Vector2)transform.position;

        //spawn bullet to travel along vector
        StartCoroutine(SoundManager.Instance.PlayPlayerShoot("Gun"));
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(vectorToMouse.normalized * GameManager.Instance.playerBulletSpeed, ForceMode2D.Impulse);
        // Destroy(bullet, 5f);         //destroy bullet after 5s
    }

    IEnumerator Shotgun()
    {
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        GameManager.Instance.playerAmmo -= 1;
        yield return new WaitForSeconds(0);

        Vector2 vectorToMouse = mousePositionWorld - (Vector2)transform.position;
        int bulletCount = 5;
        StartCoroutine(SoundManager.Instance.PlayPlayerShoot("Shotgun"));

        for (int i = 0; i < bulletCount; i++)
        {
            float spreadAngle = Random.Range(15,30);
            float rotateAngle = spreadAngle + (Mathf.Atan2(vectorToMouse.y, vectorToMouse.x) * Mathf.Rad2Deg);
            Vector2 MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad),
            Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(MovementDirection.normalized * GameManager.Instance.playerBulletSpeed, ForceMode2D.Impulse);
        }
    }

    IEnumerator Laser() // not working
    {
        GameManager.Instance.playerAmmo -= 1;
        StartCoroutine(SoundManager.Instance.PlayPlayerShoot("LaserCharge"));
        yield return new WaitForSeconds(GameManager.Instance.playerAttackDelay); // for charge shot
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector2 vectorToMouse = mousePositionWorld - (Vector2)transform.position;
        StartCoroutine(SoundManager.Instance.PlayPlayerShoot("Laser"));

        GameObject bullet = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(vectorToMouse.normalized * GameManager.Instance.playerBulletSpeed, ForceMode2D.Impulse);
        bullet.transform.right = vectorToMouse.normalized;

        // GameObject bullet = Instantiate(laserPrefab, transform.position , new Quaternion());
        // bullet.transform.GetChild(0).transform.position = (Vector2)transform.position;
    }


    void Reload()
    {
        GameManager.Instance.playerAmmo += 1;
    }
}
