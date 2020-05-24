using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDestroyer : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile wallTile;
    public Tile destructibleTile;
    public GameObject explosionPrefab;
    public int radio = 2;


    public void Explode (Vector2 worldPos)
    {
        Vector3Int originCell = tilemap.WorldToCell(worldPos);
        ExplodeCell(originCell);
        for(int i = 1; i <= radio; i++)
        {
            if(!ExplodeCell(originCell + new Vector3Int(i, 0, 0)))
            {
                break;
            }
        }
        for (int i = 1; i <= radio; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(i*-1, 0, 0)))
            {
                break;
            }
        }
        for (int i = 1; i <= radio; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(0,i, 0)))
            {
                break;
            }
        }
        for (int i = 1; i <= radio; i++)
        {
            if (!ExplodeCell(originCell + new Vector3Int(0, i*-1, 0)))
            {
                break;
            }
        }
        
    }

    bool ExplodeCell (Vector3Int cell)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        if (tile == wallTile)
        {
            return false;
        }
        Vector3 cellsize = tilemap.cellSize;
        Vector3 rayStart = tilemap.CellToWorld(cell) + new Vector3(cellsize[0] / 2, cellsize[1] / 2, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayStart, new Vector2(cellsize[0], cellsize[1]), 1);
        Vector2 cellCenterPos = tilemap.GetCellCenterWorld(cell);
        if (hit.collider)
        {
            if (hit.collider.tag == "Bomb")
            {
                Vector3 bombLocation = (hit.transform.gameObject.transform.position);
                Destroy(hit.transform.gameObject);
                Explode(bombLocation);
            }
        }

        Vector3 explosionPos = tilemap.GetCellCenterWorld(cell);
        Instantiate(explosionPrefab, explosionPos, Quaternion.identity);
        if (tile == destructibleTile)
        {
            tilemap.SetTile(cell, null);
            return false;
        }
        return true;
    }
}
