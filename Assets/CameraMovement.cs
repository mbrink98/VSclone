using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraTension;

    [SerializeField] private float cameraDampening;

    private Vector3 cameraVelocity;

    // Start is called before the first frame update
    void Start()
    {
        cameraVelocity = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameIsPaused){
            Vector3 playerPosition = GameManager.Instance.player.transform.position;
            cameraVelocity.x += (playerPosition.x - transform.position.x) * cameraTension * Time.deltaTime;
            cameraVelocity.y += (playerPosition.y - transform.position.y) * cameraTension * Time.deltaTime;
            Vector3 movementVector = Time.deltaTime * cameraVelocity;
            transform.Translate(movementVector);
            MapManager.Instance.movePlanets(movementVector);
            cameraVelocity *= Mathf.Pow(cameraDampening, Time.deltaTime);
        }
    }
}
