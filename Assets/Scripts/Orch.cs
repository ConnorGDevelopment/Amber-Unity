using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class PawnManager : ItemManager
{
    protected override string ManagerName => "Orch";
    protected override string ItemCategory => "Pawn";

    public UnityEvent OnMove = new();

    public void MovePawn(Vector3 cords)
    {
        var _cords = Orch.Terrain.GetCellCenterWorld(Orch.Terrain.WorldToCell(cords));
        Debug.Log($"Orch: Pawn {Selected.GetInstanceID()} moving to {_cords} (World: {cords})");
        Selected.transform.position = _cords;
        Deselect();
    }
}

public class TileManager : VectorItemManager
{
    protected override string ManagerName => "Orch";
    protected override string ItemCategory => "Tile";
}

public enum ActionState {
        None,
        Move,
        Attack
    }

public class Orch : MonoBehaviour
{
    // GameObject Refs
    public Camera MainCam { get; private set; }
    public HighlightGridController HighlightGrid { get; private set; }
    public Tilemap Terrain { get; private set; }

    // ScriptableObjects
    public PawnManager PawnManager;
    public TileManager TileManager;

    void Start()
    {
        MainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        HighlightGrid = GameObject
            .FindWithTag("HighlightGrid")
            .GetComponent<HighlightGridController>();

        Terrain = GameObject.FindWithTag("Terrain").GetComponent<Tilemap>();

        PawnManager = ScriptableObject.CreateInstance<PawnManager>();
        TileManager = ScriptableObject.CreateInstance<TileManager>();
    }

    public ActionState ActionState = ActionState.None;
    
    public void RaycastResolver(RaycastHit2D[] hits) {
        foreach(var hit in hits) {
            if(hit.collider.gameObject.TryGetComponent(out Pawn pawn)) {
                PawnManager.Select(pawn.gameObject);
            }

            if(hit.collider.gameObject.TryGetComponent(out TilemapCollider2D _)) {
                TileManager.Select(hit.point);
            }
        }
    }
}
