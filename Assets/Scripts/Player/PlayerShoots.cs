

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
        GameManager.Instance.playerBulletSpeed = 10f;
        GameManager.Instance.playerAttackDelay = 0.3f;
        GameManager.Instance.playerAmmo = 5f;
        GameManager.Instance.playerMaxAmmo = 3f;
        GameManager.Instance.playerReloadSpeed = 2f;
    }

    float elapsed = 0f;
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameIsPaused)
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.playerAmmo > 0)
            {
                StartCoroutine(Shotgun());
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
        // yield return new WaitForSeconds(GameManager.Instance.playerAttackDelay); // for charge shot
        yield return new WaitForSeconds(0);

        Vector2 vectorToMouse = mousePositionWorld - (Vector2)transform.position;

        //spawn bullet to travel along vector
        SoundManager.Instance.playPlayerShoot("Gun");
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
        int bulletCount = 3;
        SoundManager.Instance.playPlayerShoot("Shotgun");

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
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        GameManager.Instance.playerAmmo -= 1;
        yield return new WaitForSeconds(0);

        Vector2 vectorToMouse = mousePositionWorld - (Vector2)transform.position;
        SoundManager.Instance.playPlayerShoot("Laser");

        GameObject bullet = Instantiate(laserPrefab, transform.position , new Quaternion());
        bullet.transform.GetChild(0).transform.position = (Vector2)transform.position;
    }

    void Reload()
    {
        GameManager.Instance.playerAmmo += 1;
    }
}
