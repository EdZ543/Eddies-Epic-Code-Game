using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementController : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public Vector3Int tilemapPosition;

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

    public void turnRight()
    {
        direction = (direction + 1) % directions.Length;

        animator.SetBool("Turn Right", true);
    }

    public void turnLeft()
    {
        direction = direction - 1;
        if (direction < 0)
        {
            direction = directions.Length - 1;
        }

        animator.SetBool("Turn Left", true);
    }

    public void moveForward()
    {
        Vector3Int nextTilemapPosition = tilemapPosition;

        switch (directions[direction])
        {
            case "br":
                transform.Translate(stepWidth, -stepHeight, 0);
                nextTilemapPosition += Vector3Int.down;

                break;
            case "bl":
                transform.Translate(-stepWidth, -stepHeight, 0);
                nextTilemapPosition += Vector3Int.left;
                break;
            case "tl":
                transform.Translate(-stepWidth, stepHeight, 0);
                nextTilemapPosition += Vector3Int.up;
                break;
            case "tr":
                transform.Translate(stepWidth, stepHeight, 0);
                nextTilemapPosition += Vector3Int.right;
                break;
        }

        Tile tile = tilemap.GetTile<Tile>(nextTilemapPosition);
        Debug.Log(tile.transform.GetPosition());
    }
}