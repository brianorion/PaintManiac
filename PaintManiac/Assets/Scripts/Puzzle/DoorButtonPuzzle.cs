using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonPuzzle : Puzzle
{
    // this script will be placed on top of a button game object


    /* when an gameobject of type Crate has collided onto the button, the button will notify
     * us that the puzzle has been solved. 
     */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Crate")
        {
            hasFinishedPuzzle = true;
        }
    }
}
