using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementController : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;

    Animator animator;

    public int direction = 0;

    private string[] directions = { "br", "bl", "tl", "tr" };
    private float stepWidth;
    private float stepHeight;
    private Vector3Int tilemapPosition;
    private Vector3 offset = new Vector3(0f, 0.2f, 0f);

    void Start()
    {
        stepWidth = grid.cellSize.x;
        stepHeight = grid.cellSize.y;
        animator = GetComponent<Animator>();
        tilemapPosition = grid.WorldToCell(transform.position);
        transform.position = grid.CellToWorld(tilemapPosition) + offset;
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

    bool tileIsClear(Vector3Int position)
    {
        Sprite sprite = tilemap.GetSprite(position);
        return sprite == null;
    }

    public void moveForward()
    {
        Vector3Int nextTilemapPosition = tilemapPosition;

        switch (directions[direction])
        {
            case "br":
                nextTilemapPosition += Vector3Int.down;
                break;
            case "bl":
                nextTilemapPosition += Vector3Int.left;
                break;
            case "tl":
                nextTilemapPosition += Vector3Int.up;
                break;
            case "tr":
                nextTilemapPosition += Vector3Int.right;
                break;
        }

        if (tileIsClear(nextTilemapPosition + new Vector3Int(0, 0, 1)))
        {
            transform.position = grid.CellToWorld(nextTilemapPosition) + offset;
            tilemapPosition = nextTilemapPosition;
        }
    }
}