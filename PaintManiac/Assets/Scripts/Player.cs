﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    [SerializeField]
    private float itemPickUpDistance = 5f;
    [SerializeField]
    private float force = 3;
    [SerializeField]
    private Transform itemCollectionLocation;

    private Rigidbody rb;
    private Rigidbody itemRB;
    private GameObject item;
    private JumpPuzzle jumpPuzzle;
    private bool hasJumped = true;
    private int jumpCount = 0;
    private bool obtainedJumpy = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (GameManager.canMove)
        {
            Move();
        }
        ItemCollection();
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
        if (!obtainedJumpy)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
            {
                rb.AddForce(Vector3.up * force, ForceMode.Impulse);
                hasJumped = true;
                jumpCount = 1;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpPuzzle.numberOfJumps)
            {
                rb.AddForce(Vector3.up * force, ForceMode.Impulse);
                hasJumped = true;
                jumpCount++;
            }
            else
            {
                // we need to reset this because after jumping a set amount we lost the jumpy ability.
                obtainedJumpy = false;
            }
        }
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

    private void ItemCollection()
    {
        // for now I am goig to leave this here for something else later if i wanted to have sort of an item collection puzzle. 
        RaycastHit hit;

        LayerMask mask = LayerMask.GetMask("Items");
        // when the player is close enough to select an item, the player will click D to collect
        // TODO: make an UI where there will be a small pop up text to hint the player to click D to collect item
        if (Physics.Raycast(transform.position, cam.transform.forward, out hit, itemPickUpDistance, mask) && Input.GetMouseButtonDown(0))
        {
            //Debug.Log($"{hit.collider.tag} has been hit");
            item = hit.collider.gameObject;
            item.transform.SetParent(itemCollectionLocation);
            itemRB = item.GetComponent<Rigidbody>();
            if (itemRB != null)
            {
                itemRB.isKinematic = true;
            }
            Debug.Log(item.name);
            // this part deals with the sticks puzzle
            Stick stick = item.GetComponent<Stick>();
            if (stick != null)
            {
                // call this function that changes the internal state of the sticks
                stick.ChangeStickState();
                stick = null;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (item != null)
            {
                item.transform.parent = null;
                item = null;
            }
            if (itemRB != null)
            {
                itemRB.isKinematic = false;
                itemRB = null;
            }
        }

        if (item != null)
        {
            // if we have the smallbox selected that has the Jumpy script 
            jumpPuzzle = item.GetComponent<JumpPuzzle>();

            if (jumpPuzzle != null)
            {
                obtainedJumpy = true;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            hasJumped = false;
            jumpCount = 0;
        }
    }
}
