using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{

    public Tilemap tilemap;
    public GameObject bombPrefab;
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cellsize = tilemap.cellSize;
            Vector3 worldPos = transform.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 rayStart = tilemap.CellToWorld(cell) + new Vector3(cellsize[0] / 2, cellsize[1] / 2, 0);
            RaycastHit2D hit = Physics2D.Raycast(rayStart, new Vector2(cellsize[0], cellsize[1]), 1);
            Vector2 cellCenterPos = tilemap.GetCellCenterWorld(cell);
            if (!hit.collider || hit.collider.tag != "Bomb")
            {
                GameObject bomba = Instantiate(bombPrefab, cellCenterPos, Quaternion.identity) as GameObject;
            }
        }
    }
}
