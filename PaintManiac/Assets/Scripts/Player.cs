using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float rotationSensitivity = 1f;
    [SerializeField]
    private float maximumCameraRotation = 90f;
    [SerializeField]
    private Camera cam;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // this ensures that the player will face towards and walk towards the forward or right direction of the camera. 
        Vector3 camF = cam.transform.forward;
        Vector3 camR = cam.transform.right;

        camF.y = 0;
        camR.y = 0f;
        camF = camF.normalized;
        camR = camR.normalized;

        Vector3 velocity = camF * Input.GetAxis("Vertical") + camR * Input.GetAxis("Horizontal");
        velocity = velocity.normalized * speed;

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        PlayerRotate();
    }

    private void PlayerRotate()
    {
        float yRot = Input.GetAxis("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * rotationSensitivity;

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        CameraRotate();
    }

    private void CameraRotate()
    {

        float xRot = Input.GetAxis("Mouse Y");

        Vector3 rotation = new Vector3(xRot, 0f, 0f) * rotationSensitivity;

        cam.transform.Rotate(-rotation);
        float currentX = cam.transform.localEulerAngles.x;
        currentX = Mathf.Clamp(currentX, -270f, maximumCameraRotation);

        cam.transform.localEulerAngles = new Vector3(currentX, 0f, 0f);
    }

}
