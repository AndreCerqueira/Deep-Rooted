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
    public GameObject player;
    public ResourceManager resourceManager;
    public bool stopGrow = false;

    public Tilemap tilemap;
    public float delay = 0.05f;
    private ContactPoint2D[] contacts = new ContactPoint2D[16];

    private void Start()
    {
        // DestroyExtraTiles, 5sec delay, 0.05sec delay
        StartCoroutine(DestroyExtraTiles());
    }



    private void Update()
    {
        // Get player position in tilemap
        Vector3 playerPosition = player.transform.position;
        Vector3Int playerPositionInTilemap = tilemap.WorldToCell(playerPosition);

        // rootPosition

        // Get distance between player and rootPosition
        int distanceToRootPosition = playerPositionInTilemap.y - rootPosition.y;

        print(distanceToRootPosition);

        if (distanceToRootPosition < -11)
        {
            // trigger game over
            print("Game Over");

            // Disable tilemap named Root
            rootTilemap.gameObject.SetActive(false);

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            // play audio
            GetComponent<Animator>().SetTrigger("death");
            GetComponent<PlayerController>().enabled = false;
            this.enabled = false;
        }

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

    public static float CalcularValor(float entrada)
    {
        if (entrada >= 100)
        {
            return 0.5f;
        }
        else
        {
            return (100f - entrada) / 250f + 0.5f;
        }
    }


    private IEnumerator PlaceRootInSpaceDestroyed(Vector3Int cellPosition)
    {
        // Get Resource from Resource Manager
        int value = resourceManager.resourceAmount;

        // Get player position
        Vector3 playerPosition = player.transform.position;
        Vector3Int playerCellPosition = tilemap.WorldToCell(playerPosition);

        // Wait for player to be at some distance
        print("DISTANCE: " + Vector3.Distance(playerCellPosition, cellPosition));
        while (stopGrow)
        {
            yield return new WaitForFixedUpdate();
        }


        float time = CalcularValor(value);
        yield return new WaitForSeconds(time); // <----- change this to make root stay behind
        
        rootPosition = cellPosition;
        rootTilemap.SetTile(cellPosition, root);

        // StartCoroutine(DestroyRootTileAfterPlayerDistant(cellPosition));
    }

    private IEnumerator DestroyRootTileAfterPlayerDistant(Vector3Int cellPosition)
    {
        bool isDistant = false;
        while (!isDistant)
        {
            yield return new WaitForSeconds(0.1f);

            // Get player position
            Vector3 playerPosition = player.transform.position;
            Vector3Int playerPositionInTilemap = rootTilemap.WorldToCell(playerPosition);

            // Get distance between player and root
            int distanceToRoot = playerPositionInTilemap.y - rootPosition.y;
            // Destroy tiles
            if (distanceToRoot < -5)
            {
                rootTilemap.SetTile(cellPosition, null);
                isDistant = true;
            }

        }
    }




}
