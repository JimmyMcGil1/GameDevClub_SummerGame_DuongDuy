using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] Tilemap map;  
    [SerializeField] List<TileData> tileDatas;
    private Dictionary<TileBase,TileData> dataFromTiles;

    private void Awake() {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles) 
            {
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);
            TileBase clickTile = map.GetTile(gridPosition);
            if (clickTile != null) {
                map.SetTile(gridPosition,null);
            }
            float buffSpeed = dataFromTiles[clickTile].buffSpeed;
            
        }   
    }
    public float GetTileWalkingSpeed(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);
        if (tile == null) return 0;
        return dataFromTiles[tile].buffSpeed;
    }
    public void DestroyTile(Vector2 worldPosition) {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);
        TileBase tile = map.GetTile(gridPosition);
        if (tile != null) {
            map.SetTile(gridPosition, null);
        }
    }
}
