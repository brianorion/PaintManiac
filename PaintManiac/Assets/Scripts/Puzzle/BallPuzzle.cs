using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPuzzle : Puzzle
{
    [SerializeField]
    private GameObject[] platforms = new GameObject[4];
    [SerializeField]
    private float maximumDepth;
    [SerializeField]
    private float depthIncrement;
    [SerializeField]
    private GameObject door;
    
    private bool ballAtBase;
    private bool hasLowered = false;
    private float counter;
    private void Start()
    {
        counter = platforms[0].transform.position.y;
    }

    private void Update()
    {
        SplitPlatforms();
        if (GameManager.sphereHasBeenDestroyed)
        {
            hasFinishedPuzzle = true;
            BoxCollider doorCollider = door.GetComponent<BoxCollider>();
            doorCollider.enabled = false;
        }
    }

    private void SplitPlatforms()
    {
        if (ballAtBase && !hasLowered)
        {
            // this is when the platform is still lowering itself
            if (counter > -maximumDepth)
            {
                counter -= depthIncrement * Time.deltaTime;
                foreach (GameObject platform in platforms)
                {
                    Vector3 downwardVector = new Vector3(platform.transform.position.x, counter, platform.transform.position.z);
                    platform.transform.position = downwardVector;
                    BoxCollider platformCollider = platform.GetComponent<BoxCollider>();
                    platformCollider.enabled = false;
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Sphere")
        {
            ballAtBase = true;
        }
    }
}
