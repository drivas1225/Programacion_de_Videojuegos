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
            Vector3 worldPos = transform.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector2 cellCenterPos = tilemap.GetCellCenterWorld(cell);
            Instantiate(bombPrefab, cellCenterPos, Quaternion.identity);
        }
    }
}
