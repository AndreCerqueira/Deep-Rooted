using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileDestroyer : MonoBehaviour
{
    public Tilemap tilemap;
    public float delay = 0.05f;
    private ContactPoint2D[] contacts = new ContactPoint2D[16];

    void OnCollisionStay2D(Collision2D other)
    {
        int numContacts = other.GetContacts(contacts);
        if(numContacts > 0)
        StartCoroutine(DestroyTilesAfterDelay(other, numContacts));
    }

    private IEnumerator DestroyTilesAfterDelay(Collision2D other, int numContacts)
    {

        yield return new WaitForSeconds(delay);
        print("total" + numContacts);
        for (int i = 0; i < numContacts; i++)
        {
            print(i);
            Vector3Int cellPosition = tilemap.WorldToCell(contacts[i].point);
            cellPosition = cellPosition - new Vector3Int(0, 1, 0);
            tilemap.SetTile(cellPosition, null);
            if (Input.GetAxis("Horizontal") > 0)
            {
                cellPosition = cellPosition + new Vector3Int(1, 0, 0);
                tilemap.SetTile(cellPosition, null);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                cellPosition = cellPosition - new Vector3Int(1, 0, 0);
                tilemap.SetTile(cellPosition, null);
            }
        }
    }
}
