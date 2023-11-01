using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float rotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movementDir = new Vector2(horizontalInput,verticalInput);
        float inputMagnitute = Mathf.Clamp01(movementDir.magnitude);
        movementDir.Normalize();

        transform.Translate(movementDir * movementSpeed * inputMagnitute * Time.deltaTime, Space.World);

        if (movementDir != Vector2.zero) {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }


    }
}
