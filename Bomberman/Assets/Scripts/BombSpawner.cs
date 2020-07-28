using UnityEngine;
using UnityEngine.Tilemaps;

public class BombSpawner : MonoBehaviour
{

    public Tilemap tilemap;
    public GameObject bombPrefab;
    private AudioManager aManager;

    public void activeBomb()
    {
        if(!GetComponentInParent<PlayerMovement>().mode && FindObjectOfType<GameController>().currentBombs < FindObjectOfType<GameController>().maxBombs)
        {
            Vector3 cellsize = tilemap.cellSize;
            Vector3 worldPos = transform.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 rayStart = tilemap.CellToWorld(cell) + new Vector3(cellsize[0] / 2, cellsize[1] / 2, 0);
            RaycastHit2D hit = Physics2D.Raycast(rayStart, new Vector2(cellsize[0], cellsize[1]), 1);
            Vector2 cellCenterPos = tilemap.GetCellCenterWorld(cell);
            if ((!hit.collider || hit.collider.tag != "Bomb") && FindObjectOfType<GameController>().currentBombs < FindObjectOfType<GameController>().maxBombs)
            {
                aManager.Play("Set Bomb");
                if (Instantiate(bombPrefab, cellCenterPos, Quaternion.identity))
                {
                    FindObjectOfType<GameController>().currentBombs += 1;
                }
            }
        }
        return ;
    }
    private void Start()
    {
        aManager = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        if(Input.GetKeyUp("m"))
        {
            GetComponentInParent<PlayerMovement>().mode = !GetComponentInParent<PlayerMovement>().mode;
        }
        if (Input.GetKeyUp("space"))
        {
            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 cellsize = tilemap.cellSize;
            Vector3 worldPos = transform.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            Vector3 rayStart = tilemap.CellToWorld(cell) + new Vector3(cellsize[0] / 2, cellsize[1] / 2, 0);
            RaycastHit2D hit = Physics2D.Raycast(rayStart, new Vector2(cellsize[0], cellsize[1]), 1);
            Vector2 cellCenterPos = tilemap.GetCellCenterWorld(cell);
            if ((!hit.collider || hit.collider.tag != "Bomb")&& FindObjectOfType<GameController>().currentBombs < FindObjectOfType<GameController>().maxBombs)
            {
                aManager.Play("Set Bomb");
                if(Instantiate(bombPrefab, cellCenterPos, Quaternion.identity))
                {
                    FindObjectOfType<GameController>().currentBombs += 1;
                }
            }
        }
    }
}
