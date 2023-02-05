using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class BresenhamLine
{
    public static List<Vector3Int> GetLine(Vector3Int from, Vector3Int to)
    {
        List<Vector3Int> line = new List<Vector3Int>();
        int x0 = from.x;
        int y0 = from.y;
        int x1 = to.x;
        int y1 = to.y;
        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;
        while (true)
        {
            line.Add(new Vector3Int(x0, y0, 0));
            if (x0 == x1 && y0 == y1)
            {
                break;
            }
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err = err - dy;
                x0 = x0 + sx;
            }
            if (e2 < dx)
            {
                err = err + dx;
                y0 = y0 + sy;
            }
        }
        return line;
    }

    public static void DrawLine(Vector3Int from, Vector3Int to, Tilemap tilemap, TileBase tile)
    {
        List<Vector3Int> line = GetLine(from, to);
        foreach (Vector3Int cell in line)
        {
            tilemap.SetTile(cell, tile);
        }
    }
}
