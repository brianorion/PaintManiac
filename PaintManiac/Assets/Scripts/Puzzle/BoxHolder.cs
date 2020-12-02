using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHolder : MonoBehaviour
{
    [SerializeField]
    private GameObject boxHolder;

    public bool hasBeenPositioned = false;

    private void Start()
    {
        Debug.Log(boxHolder.tag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);

        if (collision.collider.tag == boxHolder.tag)
        {
            Debug.Log("I have touched the box holder");

            hasBeenPositioned = true;
        }
    }
}
