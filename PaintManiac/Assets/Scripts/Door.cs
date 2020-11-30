using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject doorPlate;
    [SerializeField]
    private Puzzle doorButtonPuzzle;
    [SerializeField]
    private float moveIncrementPerSec = 0.25f;
    [SerializeField]
    private float doorOffset = 2.5f;

    private float doorStartPosition;
    private float cumulativeHeight = 0f;

    private void Start()
    {
        doorStartPosition = doorPlate.transform.position.y;
    }
    private void Update()
    {
        MoveDoor();
    }

    private void MoveDoor()
    {
        // if the puzzle is finished
        if (doorButtonPuzzle.hasFinishedPuzzle)
        {
            if (cumulativeHeight < doorOffset)
            {
                doorPlate.transform.Translate(new Vector3(0f, moveIncrementPerSec, 0f) * Time.deltaTime, Space.World);
                cumulativeHeight = doorPlate.transform.position.y - doorStartPosition;
            }
        }
    }
}
