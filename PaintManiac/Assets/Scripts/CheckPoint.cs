using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private bool needSmallBox;
    [SerializeField]
    private GameObject smallBox;

    private GameObject instantiatedBox;
    private bool updatedCheckPoint = false;
    private int boxCount = 1;

    private void Update()
    {
        SpawnBox();
    }

    private void SpawnBox()
    {
        // if the player has died and this portal requires a box to be spawned.
        if (GameManager.playerHasDied && needSmallBox && boxCount <= 1)
        {
            instantiatedBox = Instantiate(smallBox, transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 0f)));
            boxCount++;
        }
    }

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
