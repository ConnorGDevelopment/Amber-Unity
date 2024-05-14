using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileHelper
{
    public static Vector3 WorldToWorldCellCenter(Vector3 worldCords, Tilemap tilemap)
    {
        return tilemap.GetCellCenterWorld(tilemap.WorldToCell(worldCords));
    }

    public static List<Vector3Int> GenSimpleRange(int distance)
    {
        List<Vector3Int> area = new();

        for (int x = distance; x >= 0; x--)
        {
            for (int y = 0; y <= x; y++)
            {
                Vector3Int delta = new(x - y, y, 0);
                Debug.Log(delta);
                area.Add(delta * new Vector3Int(1, 1, 0));
                area.Add(delta * new Vector3Int(-1, 1, 0));
                area.Add(delta * new Vector3Int(-1, -1, 0));
                area.Add(delta * new Vector3Int(1, -1, 0));
            }
        }
        return area;
    }

    public static List<Vector3Int> AddDeltaVecs(
        Vector3 objectWorldCords,
        List<Vector3Int> deltaVecs,
        Tilemap tilemap
    )
    {
        var objectCellCords = tilemap.WorldToCell(objectWorldCords);
        List<Vector3Int> withDeltaVecs = new();

        foreach (var deltaVec in deltaVecs)
        {
            withDeltaVecs.Add(objectCellCords + deltaVec);
        }

        return withDeltaVecs;
    }

    public static Vector3 CameraToWorld(Vector3 mousePosition, Camera mainCam)
    {
        var cords = mainCam.ScreenToWorldPoint(mousePosition);
        cords.z = 0;
        return cords;
    }
}
