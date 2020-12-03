using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public enum StickState
    {
        North = 0,
        East = 90,
        South = 180,
        West = 270

    }
    public StickState stickState;
    private int index;
    
    private void Update()
    {
        PositionOfSticks();  
    }
    private void PositionOfSticks()
    {
        if (stickState == StickState.North)
        {
            transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        }
        else if (stickState == StickState.East)
        {
            transform.rotation = Quaternion.Euler(-90f, 90f, 0f);
        }
        else if (stickState == StickState.South)
        {
            transform.rotation = Quaternion.Euler(-90f, 180f, 0f);
        }
        else if (stickState == StickState.West)
        {
            transform.rotation = Quaternion.Euler(-90f, 270f, 0f);
        }
    }
    private void Start()
    {
        if (stickState == StickState.North)
        {
            index = 0;
        }
        else if (stickState == StickState.East)
        {
            index = 1;
        }
        else if (stickState == StickState.South)
        {
            index = 2;
        }
        else if(stickState == StickState.West)
        {
            index = 3;
        }
        PositionOfSticks();
    } 
    public void ChangeStickState()
    {
        index += 1;
        if (index > 3)
            index = index % 4;
        if (index == 0)
        {
            stickState = StickState.North;
        }
        else if (index == 1)
        {
            stickState = StickState.East;
        }
        else if (index == 2)
        {
            stickState = StickState.South;
        }
        else if (index == 3)
        {
            stickState = StickState.West;
        }
        Debug.Log(stickState);
    }
}
