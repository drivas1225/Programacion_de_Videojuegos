using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DinasourMovement : MonoBehaviour
{
    public float movSpeed = 2;

    public Rigidbody2D rb;
    public Animator animator;
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile destructibleTile;
    public float horizontal = 1.0f;
    public float vertical = 0.0f;
    public Vector2 movment;
    public Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = new Vector3(transform.position.x, transform.position.y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //input
        Vector3Int currentPos = tilemap.WorldToCell(transform.position);
        Tile tile;
        tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int((int)horizontal, (int)vertical, 0));
        if (currentPosition == transform.position )
        {
            Debug.Log(tilemap.GetCellCenterWorld(currentPos));
            List<Vector2> directions = new List<Vector2>();
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(1, 0, 0));
            if (tile != wallTile && tile != destructibleTile)
            {
                directions.Add(new Vector2(1.0f, 0f));
            }
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(-1, 0, 0));
            if (tile != wallTile && tile != destructibleTile)
            {
                directions.Add(new Vector2(-1.0f, 0f));
            }
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(0, 1, 0));
            if (tile != wallTile && tile != destructibleTile)
            {
                directions.Add(new Vector2(0f, 1.0f));
            }
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(0, -1, 0));
            if (tile != wallTile && tile != destructibleTile)
            {
                directions.Add(new Vector2(0f, -1.0f));
            }

            movment = directions[Random.Range(0, directions.Count)];
            horizontal = movment.x;
            vertical = movment.y;
        }
        movment.x = horizontal;
        movment.y = vertical;
        rb.MovePosition(rb.position + movment * movSpeed * Time.deltaTime);
        currentPosition = rb.transform.position;
        animator.SetFloat("Horizontal", movment.x);
        animator.SetFloat("Vertical", movment.y);
        animator.SetFloat("Speed", movment.sqrMagnitude);
    }

    
}
