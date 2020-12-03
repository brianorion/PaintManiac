using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : Door
{
    [SerializeField]
    private Puzzle puzzle1;
    [SerializeField]
    private Puzzle puzzle2;
    [SerializeField]
    private Puzzle puzzle3;

    private void Update()
    {
        MoveDoor();
    }

    private new void MoveDoor()
    {
        // if the puzzle is finished
        if (puzzle1.hasFinishedPuzzle && puzzle2.hasFinishedPuzzle && puzzle3.hasFinishedPuzzle)
        {
            if (cumulativeHeight < doorOffset)
            {
                doorPlate.transform.Translate(new Vector3(0f, moveIncrementPerSec, 0f) * Time.deltaTime, Space.World);
                cumulativeHeight = doorPlate.transform.position.y - doorStartPosition;
            }
        }
    }
}
