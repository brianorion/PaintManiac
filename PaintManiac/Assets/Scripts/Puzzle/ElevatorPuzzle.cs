using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPuzzle : Puzzle
{
    [SerializeField]
    private float downwardIncrement = 1f;
    [SerializeField]
    private float maximumDepth = 4f;

    private float counter;

    private void Start()
    {
        counter = transform.position.y;
    }
    private void Update()
    {
        if (hasFinishedPuzzle)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        if (counter > maximumDepth)
        {
            counter -= downwardIncrement * Time.deltaTime;
            Vector3 downwardVector = new Vector3(transform.position.x, counter, transform.position.z);
            transform.position = downwardVector;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Crate")
        {
            hasFinishedPuzzle = true;
        }
    }
}
