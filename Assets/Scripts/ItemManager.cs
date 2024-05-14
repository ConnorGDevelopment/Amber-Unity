using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class NeedsItemManager : ScriptableObject
{
    protected virtual string ManagerName { get; }
    protected virtual string ItemCategory { get; }

    private GameObject _selected;

    public virtual GameObject Selected
    {
        get { return _selected; }
        set { _selected = value; }
    }

    public UnityEvent OnSelect = new();
    public UnityEvent OnDeselect = new();

    protected void SelectCalls()
    {
        OnSelect.Invoke();
    }

    protected void DeselectCalls()
    {
        OnDeselect.Invoke();
    }

    protected Orch Orch { get; private set; }

    public void Awake()
    {
        Orch = GameObject.FindWithTag("Orch").GetComponent<Orch>();
    }
}

interface IItemManager<in T>
{
    public abstract void SelectMain(T newSelection);
    public abstract void SelectLogging(T newSelection);
    public abstract void Select(T newSelection);
    public abstract void DeselectMain();
    public abstract void DeselectLogging();
    public abstract void Deselect();
}

public abstract class ItemManager : NeedsItemManager, IItemManager<GameObject>
{
    public virtual void SelectMain(GameObject newSelection)
    {
        if (Selected == null || newSelection.GetInstanceID() != Selected.GetInstanceID())
        {
            Selected = newSelection;
            OnSelect.Invoke();
        }
        else
        {
            Deselect();
        }
    }

    public virtual void SelectLogging(GameObject newSelection)
    {
        Debug.Log($"{ManagerName}: {ItemCategory} {newSelection.GetInstanceID()} selected");
    }

    public void Select(GameObject newSelection)
    {
        SelectMain(newSelection);
        SelectCalls();
        SelectLogging(newSelection);
    }

    public virtual void DeselectMain()
    {
        Selected = null;
    }

    public virtual void DeselectLogging()
    {
        Debug.Log($"{ManagerName}: {ItemCategory} deselected");
    }

    public void Deselect()
    {
        DeselectMain();
        DeselectCalls();
        DeselectLogging();
    }
}

public class VectorItemManager : ItemManager, IItemManager<Vector3?>
{
    private Vector3? _selected;
    public new virtual Vector3? Selected
    {
        get
        {
            if (_selected.HasValue)
            {
                return _selected;
            }
            else
            {
                return null;
            }
        }
        set
        {
            if (value.HasValue)
            {
                var _value = (Vector3)value;
                _selected = TileHelper.WorldToWorldCellCenter(_value, Orch.Terrain);
            }
            else
            {
                _selected = null;
            }
        }
    }

    public virtual void SelectMain(Vector3? newSelection)
    {
        if (newSelection != null && newSelection != Selected)
        {
            Selected = newSelection;
            OnSelect.Invoke();
        }
        else
        {
            Deselect();
        }
    }

    public virtual void SelectLogging(Vector3? newSelection)
    {
        Debug.Log($"{ManagerName}: {ItemCategory} {newSelection.Value} selected");
    }

    public void Select(Vector3? newSelection)
    {
        SelectMain(newSelection);
        SelectCalls();
        SelectLogging(newSelection);
    }
}
