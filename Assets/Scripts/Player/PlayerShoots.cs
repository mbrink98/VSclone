

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoots : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
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
                StartCoroutine(Shoot());
            }


            elapsed += Time.deltaTime;
            if (elapsed >= GameManager.Instance.playerReloadSpeed)
            {
                elapsed = elapsed % 1f;
                if (GameManager.Instance.playerAmmo < GameManager.Instance.playerMaxAmmo) {
                    Reload();
                }
            }
        }
    }

    IEnumerator Shoot()
    {
        //get vector to mouse position in world context
        Vector2 mousePositionScreen = Input.mousePosition;
        Vector2 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);
        GameManager.Instance.playerAmmo -= 1;
        yield return new WaitForSeconds(GameManager.Instance.playerAttackDelay);

        Vector2 vectorToMouse = mousePositionWorld - (Vector2)transform.position;

        //spawn bullet to travel along vector
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(vectorToMouse.normalized * GameManager.Instance.playerBulletSpeed, ForceMode2D.Impulse);
        Destroy(bullet, 5f);         //destroy bullet after 5s
    }

    void Reload()
    {
        GameManager.Instance.playerAmmo += 1;
    }
}
