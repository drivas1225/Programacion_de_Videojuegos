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
        if (tilemap.GetInstantiatedObject(cell))
        {
            Debug.Log(tilemap.GetInstantiatedObject(cell));
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
