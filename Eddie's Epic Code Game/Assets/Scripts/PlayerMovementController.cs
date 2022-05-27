using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Grid grid;

    private float stepWidth;
    private float stepHeight;

    void Start()
    {
        stepWidth = grid.cellSize.x;
        stepHeight = grid.cellSize.y;

        Debug.Log(stepWidth);
    }

    void moveTopLeft()
    {

    }

    void moveTopRight()
    {

    }

    void moveBottomLeft()
    {

    }

    void moveBottomRight()
    {

    }
}
