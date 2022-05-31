using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementController : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    public bool drankMilk = false;

    Animator animator;

    public int direction = 0;

    private string[] directions = { "br", "bl", "tl", "tr" };
    private float stepWidth;
    private float stepHeight;
    private Vector3Int tilemapPosition;
    private Vector3 offset = new Vector3(0f, 0.2f, 0f);
    private bool keyBearer = false;

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
        return sprite == null || sprite.name == "milk" || sprite.name == "forbidden_milk" || sprite.name == "key";
    }

    Vector3Int tileInFront()
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

        return nextTilemapPosition;
    }

    public void moveForward()
    {
        Vector3Int nextTilemapPosition = tileInFront();

        if (tileIsClear(nextTilemapPosition + new Vector3Int(0, 0, 1)))
        {
            transform.position = grid.CellToWorld(nextTilemapPosition) + offset;
            tilemapPosition = nextTilemapPosition;

            if (on("milk"))
            {
                drinkMilk();
            }
            else if (on("forbidden_milk") && keyBearer)
            {
                drinkMilk();
            }
            else if (on("key"))
            {
                drinkKey();
            }
        }
    }

    public bool on(string item)
    {
        Sprite sprite = tilemap.GetSprite(tilemapPosition + new Vector3Int(0, 0, 1));
        return sprite != null && sprite.name == item;
    }

    public void drinkMilk()
    {
        tilemap.SetTile(tilemapPosition + new Vector3Int(0, 0, 1), null);
        drankMilk = true;
    }

    public void drinkKey()
    {
        tilemap.SetTile(tilemapPosition + new Vector3Int(0, 0, 1), null);
        keyBearer = true;
    }
}