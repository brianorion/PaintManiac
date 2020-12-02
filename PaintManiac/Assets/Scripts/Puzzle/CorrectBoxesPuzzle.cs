using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectBoxesPuzzle : Puzzle
{
    [SerializeField]
    private List<BoxHolder> boxHolders = new List<BoxHolder>();

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
    }

    private void Update()
    {
        // if all boxes have been positioned
        if (CheckAllBoxes())
        {
            hasFinishedPuzzle = true;
            boxCollider.enabled = false;
        }
    }
    // this checks if all the boxes are in position
    private bool CheckAllBoxes()
    {
        foreach (BoxHolder boxHolder in boxHolders)
        {
            if (!boxHolder.hasBeenPositioned)
            {
                return false;
            }
        }
        return true;
    }
}
