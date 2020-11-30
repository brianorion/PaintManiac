using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool updatedCheckPoint = false;
    private void OnTriggerEnter(Collider other)
    {
        // todo: have a simple star particle system when player collides onto check point

        // when the player collides onto the check points, it will
        // notify the game manager of the new checkpoint
        if (other.tag == "Player" && !updatedCheckPoint)
        {
            GameManager.spawnLocation = transform.position;
            updatedCheckPoint = true;
            Debug.Log("Updated Check Point");

        }
    }
}
