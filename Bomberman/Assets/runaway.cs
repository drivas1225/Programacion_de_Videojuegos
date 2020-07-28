using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class runaway : MonoBehaviour
{
    public Tilemap tilemap;

    public List<Tile> collidable;
    public Tile tile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int currentPos = tilemap.WorldToCell(transform.position);
        Vector3 worldPos = tilemap.GetCellCenterWorld(currentPos);
        int radio = FindObjectOfType<MapDestroyer>().radio + 1 ;
        Debug.DrawLine(worldPos, worldPos + new Vector3(radio, 0, 0), Color.blue);
        RaycastHit2D hitRight = Physics2D.Raycast(worldPos,new Vector2(1,0),radio);
        Debug.DrawLine(worldPos, worldPos + new Vector3(-radio, 0, 0), Color.green);
        RaycastHit2D hitLeft = Physics2D.Raycast(worldPos, new Vector2(-1, 0), radio);
        Debug.DrawLine(worldPos, worldPos + new Vector3(0, radio, 0), Color.red);
        RaycastHit2D hitUp = Physics2D.Raycast(worldPos, new Vector2(0, 1), radio);
        Debug.DrawLine(worldPos, worldPos + new Vector3(0, -radio, 0), Color.yellow);
        RaycastHit2D hitDown = Physics2D.Raycast(worldPos, new Vector2(0, -1), radio);
        
        if (hitRight.collider && hitRight.collider.gameObject.tag == "Bomb")
        {
            GetComponentInParent<DinasourMovement>().movSpeed= 3;
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(-1, 0, 0));
            if (!collidable.Contains(tile))
            {
                GetComponentInParent<DinasourMovement>().horizontal = -1.0f;
                GetComponentInParent<DinasourMovement>().vertical = 0.0f;
            }
        }
        else if(hitLeft.collider && hitLeft.collider.gameObject.tag == "Bomb")
        {
            GetComponentInParent<DinasourMovement>().movSpeed = 3;
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(1, 0, 0));
            if (!collidable.Contains(tile))
            {
                GetComponentInParent<DinasourMovement>().horizontal = 1.0f;
                GetComponentInParent<DinasourMovement>().vertical = 0.0f;
            }
        }
        else if (hitUp.collider && hitUp.collider.gameObject.tag == "Bomb")
        {
            GetComponentInParent<DinasourMovement>().movSpeed = 3;
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(0, -1, 0));
            if (!collidable.Contains(tile))
            {
                GetComponentInParent<DinasourMovement>().horizontal = 0.0f;
                GetComponentInParent<DinasourMovement>().vertical = -1.0f;
            }
        }
        else if (hitDown.collider && hitDown.collider.gameObject.tag == "Bomb")
        {
            GetComponentInParent<DinasourMovement>().movSpeed = 3;
            tile = tilemap.GetTile<Tile>(currentPos + new Vector3Int(0, 1, 0));
            if (!collidable.Contains(tile))
            {
                GetComponentInParent<DinasourMovement>().horizontal = 0.0f;
                GetComponentInParent<DinasourMovement>().vertical = 1.0f;
            }
        }
        else
        {
            if(GetComponentInParent<DinasourMovement>().movSpeed != 1)
            {
                GetComponentInParent<DinasourMovement>().movSpeed --;
            }
        }

    }
}
