using UnityEngine;
using UnityEngine.Tilemaps;

public class Pawn2 : MonoBehaviour
{
    public string PawnName;
    public int Movement = 6;

    public int Health = 20;

    private PawnManager2 _pawnManager;
    private SpriteRenderer _spriteRenderer;
    private Material _defaultMat;
    public Material OutlineMat;

    void Start()
    {
        // Set Refs
        _pawnManager = GameObject.FindWithTag("PawnManager").GetComponent<PawnManager2>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _defaultMat = gameObject.GetComponent<SpriteRenderer>().material;

        // Init Functions
        var _tileHelper = GameObject.FindWithTag("TileHelper").GetComponent<TileHelper2>();
        _tileHelper.AlignToGrid(gameObject);

        // Attach Listeners
        _pawnManager.OnSelect.AddListener(ToggleOutline);
        _pawnManager.OnDeselect.AddListener(ToggleOutline);
    }
    

    public void ToggleOutline()
    {
        if (_pawnManager.IsSelectedPawn(this))
        {
            Debug.Log($"Pawn {GetInstanceID()}: Outline Enabled");
            _spriteRenderer.material = OutlineMat;
        }
        else
        {
            if(_spriteRenderer.material == OutlineMat) {
                Debug.Log($"Pawn {GetInstanceID()}: Outline Disabled");
            }
            _spriteRenderer.material = _defaultMat;
        }
    }

    public void Move(Vector3 destination) {
        _pawnManager.AddCommand(new MoveCommand(this, destination));
    }

}
