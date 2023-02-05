using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    const float UPDATE_CHUNK_DELAY = 0.5f;

    [SerializeField] 
    private TileBase dirt;
    [SerializeField]
    private TileBase root;
    [SerializeField]
    private Tilemap tileMap;
    [SerializeField]
    GameObject player;
    public int width;
    public int height;

    private int initialPositionY = 0;
    private int finalPositionY = -19;

    
    // Start is called before the first frame update
    void Start()
    {
        // Starting chunk goes from y = 0 to y = -19

    }


    // Update is called once per frame
    void Update()
    {
        // Get distance between player and the finalPositionY
        Vector3 playerPosition = player.transform.position;
        Vector3Int playerPositionInTilemap = tileMap.WorldToCell(playerPosition);
        int distanceToFinalPosition = playerPositionInTilemap.y - finalPositionY;

        // Get distance between player and initialPositionY
        int distanceToInitialPosition = playerPositionInTilemap.y - initialPositionY;

        // Draw dirt tile at finalPositionY if distance is less than 20
        if (distanceToFinalPosition < 20)
        {
            DrawDirtLine(finalPositionY);
            finalPositionY -= 1;
        }

        // Remove dirt tile at initialPositionY if distance is greater than 20
        if (distanceToInitialPosition < -20)
        {
            RemoveDirtLine(initialPositionY);
            initialPositionY -= 1;
        }
    }


    private void DrawDirtLine(int y)
    {
        for (int x = -width; x < width; x++)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), dirt);
        }
    }


    private void RemoveDirtLine(int y)
    {
        for (int x = -width; x < width; x++)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), null);
        }
    }


    /*

    // Corroutine to create tiles in the tilemap from -width to width and -height to height
    IEnumerator CreateTiles()
    {
        int lastY = -20;
        while (true)
        {
            CreateDirtChunk(lastY);
            lastY -= 1;
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator DestroyTiles()
    {
        yield return new WaitForSeconds(5f);
        int lastY = 0;
        while (true)
        {
            DestroyDirtChunk(lastY);
            lastY -= 1;
            yield return new WaitForSeconds(0.15f);
        }
    }

    
    private void CreateDirtChunk(int lastY)
    {
        for (int x = -width; x < width; x++)
        {
            tileMap.SetTile(new Vector3Int(x, lastY, 0), dirt);
        }
    }

    private void DestroyDirtChunk(int lastY)
    {
        for (int x = -width; x < width; x++)
        {
            tileMap.SetTile(new Vector3Int(x, lastY, 0), null);
        }
    }*/
}
