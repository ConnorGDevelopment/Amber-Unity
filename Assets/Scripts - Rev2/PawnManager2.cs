using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PawnManager2 : CommandManager
{

    public Pawn2 Selected {get; private set;}

    public UnityEvent OnSelect = new();
    public UnityEvent OnDeselect = new();

    public void Select(Pawn2 pawn) {
        if(pawn != null && pawn.gameObject.GetInstanceID() == Selected.GetInstanceID() || pawn == null) {
            Deselect();
        } else {
            Debug.Log($"PawnManager: Pawn {pawn.gameObject.GetInstanceID()} Selected");
            Selected = pawn;
            OnSelect.Invoke();
        }
    }

    public void Deselect() {
        Debug.Log($"PawnManager: Pawn Deselected");
        Selected = null;
        OnDeselect.Invoke();
    }

    public bool IsSelectedPawn(Pawn2 pawn) {
        if(Selected == null || Selected.gameObject.GetInstanceID() != pawn.gameObject.GetInstanceID()) {
            return false;
        } else {
            return true;
        }
    }
}