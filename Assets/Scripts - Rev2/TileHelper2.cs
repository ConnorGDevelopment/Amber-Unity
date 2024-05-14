using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHelper2 : MonoBehaviour
{
    public Tilemap Terrain;
    public Tilemap HighlightGrid;
    public Camera MainCam;

    public Vector3 WorldToWorldCellCenter(Vector3 worldPosition)
    {
        return Terrain.GetCellCenterWorld(Terrain.WorldToCell(worldPosition));
    }

    public List<Vector3Int> GenSimpleRangePattern(int distance)
    {
        List<Vector3Int> pattern = new();

        for (int x = distance; x >= 0; x--)
        {
            for (int y = 0; y <= x; y++)
            {
                Vector3Int delta = new(x - y, y, 0);
                Debug.Log(delta);
                pattern.Add(delta * new Vector3Int(1, 1, 0));
                pattern.Add(delta * new Vector3Int(-1, 1, 0));
                pattern.Add(delta * new Vector3Int(-1, -1, 0));
                pattern.Add(delta * new Vector3Int(1, -1, 0));
            }
        }
        return pattern;
    }

    public List<Vector3Int> CenterPatternToWorldPosition(
        Vector3 objectWorldPosition,
        List<Vector3Int> pattern
    )
    {
        var objectGridPosition = Terrain.WorldToCell(objectWorldPosition);
        List<Vector3Int> centeredPattern = new();

        foreach (var patternCell in pattern)
        {
            centeredPattern.Add(objectGridPosition + patternCell);
        }

        return centeredPattern;
    }

    public Vector3 CameraToWorld(Vector3 mousePosition)
    {
        var cords = MainCam.ScreenToWorldPoint(mousePosition);
        cords.z = 0;
        return cords;
    }

    public void AlignToGrid(GameObject gameObject) {
        gameObject.transform.position = WorldToWorldCellCenter(gameObject.transform.position);
    }
}
