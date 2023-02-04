using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    const float UPDATE_CHUNK_DELAY = 1.5f;

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
        //StartCoroutine(DestroyTiles());
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    // Corroutine to create tiles in the tilemap from -width to width and -height to height
    IEnumerator CreateTiles()
    {
        int lastY = 0;
        while (true)
        {
            CreateDirtChunk(lastY);
            lastY -= height;
            yield return new WaitForSeconds(UPDATE_CHUNK_DELAY);
        }
    }

    IEnumerator DestroyTiles()
    {
        yield return new WaitForSeconds(UPDATE_CHUNK_DELAY * 6);
        int lastY = 0;
        while (true)
        {
            DestroyDirtChunk(lastY);
            lastY -= height;
            yield return new WaitForSeconds(UPDATE_CHUNK_DELAY * 2);
        }
    }

    
    private void CreateDirtChunk(int lastY)
    {
        for (int x = -width; x < width; x++)
        {
            for (int y = lastY; y > lastY - height; y--)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), dirt);
            }
        }
    }

    private void DestroyDirtChunk(int lastY)
    {
        for (int x = -width; x < width; x++)
        {
            for (int y = lastY; y > lastY - height; y--)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), null);
            }
        }
    }
}
