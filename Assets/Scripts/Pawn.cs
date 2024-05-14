using UnityEngine;
using UnityEngine.Tilemaps;

public class Pawn : MonoBehaviour
{
    public string PawnName;
    public int Movement = 6;

    public int Health = 20;

    private Orch _orch;
    public Material OutlineMat;
    private Material _defaultMat;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _orch = GameObject.FindWithTag("Orch").GetComponent<Orch>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        // Getting the component is explicitly used so it doesn't act as a ref
        _defaultMat = gameObject.GetComponent<SpriteRenderer>().material;

        // Correct starting position to grid
        gameObject.transform.position = TileHelper.WorldToWorldCellCenter(
            gameObject.transform.position,
           _orch.Terrain
        );

        _orch.PawnManager.OnSelect.AddListener(ToggleOutline);
        _orch.PawnManager.OnDeselect.AddListener(ToggleOutline);
    }

    public void ToggleOutline()
    {
        if (
            _orch.PawnManager.Selected
            && _orch.PawnManager.Selected.GetInstanceID() == gameObject.GetInstanceID()
        )
        {
            Debug.Log($"Pawn: Highlighting {PawnName}");
            _spriteRenderer.material = OutlineMat;
        }
        else
        {
            _spriteRenderer.material = _defaultMat;
        }
    }
}
