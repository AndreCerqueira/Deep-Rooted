using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class TileDestroyer : MonoBehaviour
{
    public TileBase root;
    public Tilemap rootTilemap;
    public Vector3Int rootPosition;

    public Tilemap tilemap;
    public float delay = 0.05f;
    private ContactPoint2D[] contacts = new ContactPoint2D[16];

    private void Start()
    {
        // DestroyExtraTiles, 5sec delay, 0.05sec delay
        StartCoroutine(DestroyExtraTiles());
    }

    void OnCollisionStay2D(Collision2D other)
    {
        int numContacts = other.GetContacts(contacts);
        if(numContacts > 0)
        StartCoroutine(DestroyTilesAfterDelay(other, numContacts));
    }


    private IEnumerator DestroyExtraTiles() 
    {
        yield return new WaitForSeconds(0.2f);

        Tilemap temp = GameObject.Find("Extra").GetComponent<Tilemap>();

        // Destroy all extra tiles
        temp.ClearAllTiles();

    }


    private IEnumerator DestroyTilesAfterDelay(Collision2D other, int numContacts)
    {

        yield return new WaitForSeconds(delay);
        for (int i = 0; i < numContacts; i++)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(contacts[i].point);
            cellPosition = cellPosition - new Vector3Int(0, 1, 0);
            tilemap.SetTile(cellPosition, null);

            if (i == 0) {


                while (rootPosition.x != cellPosition.x || rootPosition.y != cellPosition.y)
                {
                    if (rootPosition.x > cellPosition.x)
                    {
                        rootPosition.x -= 1;
                    }
                    else if (rootPosition.x < cellPosition.x)
                    {
                        rootPosition.x += 1;
                    }
                    else if (rootPosition.y > cellPosition.y)
                    {
                        rootPosition.y -= 1;
                    }
                    else if (rootPosition.y < cellPosition.y)
                    {
                        rootPosition.y += 1;
                    }
                    StartCoroutine(PlaceRootInSpaceDestroyed(rootPosition));
                    // rootTilemap.SetTile(rootPosition, root);
                }


                    // StartCoroutine(PlaceRootInSpaceDestroyed(cell));


                }

            if (Input.GetAxis("Horizontal") > 0)
            {
                cellPosition = cellPosition + new Vector3Int(1, 0, 0);
                tilemap.SetTile(cellPosition, null);
                cellPosition = cellPosition + new Vector3Int(0, 1, 0);
                tilemap.SetTile(cellPosition, null);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                cellPosition = cellPosition - new Vector3Int(1, 0, 0);
                tilemap.SetTile(cellPosition, null);
                cellPosition = cellPosition + new Vector3Int(0, 1, 0);
                tilemap.SetTile(cellPosition, null);
            }
        }
    }

    private IEnumerator PlaceRootInSpaceDestroyed(Vector3Int cellPosition)
    {
        yield return new WaitForSeconds(0.5f);
        
        rootPosition = cellPosition;
        rootTilemap.SetTile(cellPosition, root);
        
        
        StartCoroutine(DestroyRootTileAfterTime(cellPosition));
    }

    private IEnumerator DestroyRootTileAfterTime(Vector3Int cellPosition)
    {
        yield return new WaitForSeconds(1.5f);
        rootTilemap.SetTile(cellPosition, null);
    }

    //private IEnumerator PlaceRootInSpaceDestroyed()
    //{
    //    yield return new WaitForSeconds(delay);
    //    for (int i = 0; i < 16; i++)
    //    {
    //        Vector3Int cellPosition = tilemap.WorldToCell(contacts[i].point);
    //        cellPosition = cellPosition - new Vector3Int(0, 1, 0);
    //        tilemap.SetTile(cellPosition, root);
    //        if (Input.GetAxis("Horizontal") > 0)
    //        {
    //            cellPosition = cellPosition + new Vector3Int(1, 0, 0);
    //            tilemap.SetTile(cellPosition, root);
    //            cellPosition = cellPosition + new Vector3Int(0, 1, 0);
    //            tilemap.SetTile(cellPosition, root);
    //        }
    //        else if (Input.GetAxis("Horizontal") < 0)
    //        {
    //            cellPosition = cellPosition - new Vector3Int(1, 0, 0);
    //            tilemap.SetTile(cellPosition, root);
    //            cellPosition = cellPosition + new Vector3Int(0, 1, 0);
    //            tilemap.SetTile(cellPosition, root);
    //        }
    //    }
    //}
}
