using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : Puzzle
{
    [SerializeField]
    private GameObject movingWall;
    [SerializeField]
    private GameObject nonWalkablePath;
    [SerializeField]
    private float amplitude = 0.5f;
    [SerializeField]
    private float translatedValue;
    [SerializeField]
    private float period = 0.5f;
    [SerializeField]
    private float increment = 1f;

    private BoxCollider pathCollider;
    private bool stoppedWall;
    private float counter = 0f;

    private void Start()
    {
        pathCollider = nonWalkablePath.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!stoppedWall)
        {
            WallOscilation();
            pathCollider.enabled = true;
        }
        else
        {
            pathCollider.enabled = false;
        }
    }

    private void WallOscilation()
    {
        counter += increment * Time.deltaTime;
        float oscilationValue = amplitude * Mathf.Sin(counter * period) + translatedValue;
        Vector3 oscilationVector = new Vector3(oscilationValue, movingWall.transform.position.y, movingWall.transform.position.z);
        movingWall.transform.position = oscilationVector;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the button has collided onto a crate and the wall has not stopped
        if (collision.collider.tag == "Crate" && !stoppedWall)
        {
            stoppedWall = true;
        }
        else if (collision.collider.tag == "Crate" && stoppedWall)
        {
            stoppedWall = false;
        }
    }
}
