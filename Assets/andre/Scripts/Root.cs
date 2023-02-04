using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
 This class represents a tilemap with a sequencial root tiles placed in order to follow the player path
 */
public class Root : MonoBehaviour
{
    // Variables
    public TileBase rootTile;
    public Tilemap tilemap;
    public GameObject player;


    // Place a root tile in the position of the player
    public void PlaceRootTile()
    {
        // Vector3Int cellPosition = tilemap.WorldToCell(player.transform.position);
        // tilemap.SetTile(cellPosition, rootTile);
    }


    // Start is called before the first frame update
    void Start()
    {
        // InvokeRepeating("PlaceRootTile", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    
    
}
