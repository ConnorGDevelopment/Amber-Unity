using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightGridController : MonoBehaviour
{
    private Tilemap _tilemap;

    private Orch _orch;

    public TileBase MoveTile;
    public TileBase AttackTile;

    void Start()
    {
        _tilemap = gameObject.GetComponent<Tilemap>();
        _orch = GameObject.FindWithTag("Orch").GetComponent<Orch>();

        // Select
        _orch.PawnManager.OnSelect.AddListener(HighlightPawn);

        // Deselect
        _orch.PawnManager.OnDeselect.AddListener(Clear);
    }

    private void Highlight(Vector3Int cell, TileBase highlightTile)
    {
        _tilemap.SetTile(cell, highlightTile);
    }

    private void Highlight(List<Vector3Int> cells, TileBase highlightTile)
    {
        foreach (var cell in cells)
        {
            Highlight(cell, highlightTile);
        }
    }

    private void Clear()
    {
        _tilemap.ClearAllTiles();
    }

    private void HighlightPawn()
    {
        if (_orch.PawnManager.Selected.TryGetComponent(out Pawn pawn))
        {
            switch (_orch.ActionState)
            {
                case ActionState.Move:
                    Highlight(
                        TileHelper.AddDeltaVecs(
                            pawn.gameObject.transform.position,
                            TileHelper.GenSimpleRange(pawn.Movement),
                            _tilemap
                        ),
                        MoveTile
                    );
                    break;
            }
        }
    }

    public ActionState GetHighlightState(Vector3 worldCell)
    {
        var cellPosition = _tilemap.WorldToCell(worldCell);

        if (!_tilemap.HasTile(cellPosition))
        {
            return ActionState.None;
        }

        var tile = _tilemap.GetTile(cellPosition);

        if (tile == MoveTile)
        {
            return ActionState.Move;
        }
        else if (tile == AttackTile)
        {
            return ActionState.Attack;
        }
        else
        {
            return ActionState.None;
        }
    }
}
