using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformDestroyer : MonoBehaviour
{
    // Variables
    [SerializeField] 
    private Tilemap tilemap;


    private void OnTriggerStay2D(Collider2D other)
    {
            // Get the position of the player
            Vector3Int playerPosition = tilemap.WorldToCell(other.transform.position);
            // Get the tile at the player's position
            TileBase tile = tilemap.GetTile(playerPosition);
            // If the tile is not null
            if (tile != null)
            {
                // Destroy the tile
                tilemap.SetTile(playerPosition, null);
            }


            if (other.gameObject.CompareTag("Obstacle"))
            {
                Destroy(other.gameObject);
            }
    }


}
