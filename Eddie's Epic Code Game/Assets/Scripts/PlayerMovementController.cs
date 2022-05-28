using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public Grid grid;

    Animator animator;

    public int direction = 0;

    private string[] directions = { "br", "bl", "tl", "tr" };
    private float stepWidth;
    private float stepHeight;

    void Start()
    {
        stepWidth = grid.cellSize.x;
        stepHeight = grid.cellSize.y;
        animator = GetComponent<Animator>();
    }

    void moveTopLeft()
    {
        transform.Translate(-stepWidth, stepHeight, 0);
    }

    void moveTopRight()
    {
        transform.Translate(stepWidth, stepHeight, 0);
    }

    void moveBottomLeft()
    {
        transform.Translate(-stepWidth, -stepHeight, 0);
    }

    void moveBottomRight()
    {
        transform.Translate(stepWidth, -stepHeight, 0);
    }

    void turnRight()
    {
        direction = (direction + 1) % directions.Length;

        animator.SetBool("Turn Right", true);
    }

    void turnLeft()
    {
        direction = direction - 1;
        if (direction < 0)
        {
            direction = directions.Length - 1;
        }

        animator.SetBool("Turn Left", true);
    }

    void moveForward()
    {
        switch (directions[direction])
        {
            case "br":
                moveBottomRight();
                break;
            case "bl":
                moveBottomLeft();
                break;
            case "tl":
                moveTopLeft();
                break;
            case "tr":
                moveTopRight();
                break;
         }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            turnRight();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            turnLeft();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveForward();
        }
    }
}
