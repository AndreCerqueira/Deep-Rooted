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
    private Tilemap tileMap;
    public int width;
    public int height;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateTiles());
        StartCoroutine(DestroyTiles());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

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
    }
}
