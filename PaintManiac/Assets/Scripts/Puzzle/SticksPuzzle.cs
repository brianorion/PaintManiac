using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticksPuzzle : Puzzle
{
    [SerializeField]
    private List<GameObject> sticks = new List<GameObject>();
    [SerializeField]
    private Stick.StickState direction;
    [SerializeField]
    private GameObject floatingPlatform;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float increment;

    private float initialPositionUp, initialPositionDown;
    private float counter, upCounter;
    private void Start()
    {
        initialPositionUp = floatingPlatform.transform.position.y;
        initialPositionDown = floatingPlatform.transform.position.y - distance;
    }

    private void Update()
    {
        if (AllSticksStateValid())
        {
            hasFinishedPuzzle = true;
        }
        else
        {
            hasFinishedPuzzle = false;
        }
        if (hasFinishedPuzzle)
        {
            // this is going down
            if (counter < distance)
            {
                counter += increment * Time.deltaTime;
                Vector3 vertical = new Vector3(0f, initialPositionUp - counter, 0f);
                floatingPlatform.transform.position = vertical;
                upCounter = 0;
            }
            // this is going up
            else
            {
                if (upCounter < distance)
                {
                    upCounter += increment * Time.deltaTime;
                    Vector3 vertical = new Vector3(0f, initialPositionDown + upCounter, 0f);
                    floatingPlatform.transform.position = vertical;
                }
                else
                {
                    counter = 0;
                }
            }
        }
    }

    private bool AllSticksStateValid()
    {
        List<Stick.StickState> stickStates = new List<Stick.StickState>();
        foreach (GameObject stick in sticks)
        {
            Stick stickScript = stick.GetComponent<Stick>();
            if (stickScript.stickState != direction)
            {
                return false;
            }
        }
        return true;
    }
}
