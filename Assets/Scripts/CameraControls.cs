using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class CameraControls : MonoBehaviour
{
    private CharacterController _characterController;
    public float PanSpeed = 5.0f;
    private Vector2 PanInput = Vector2.zero;

    private Orch _orch;

    public void MovePlayer(InputAction.CallbackContext ctx)
    {
        PanInput = ctx.ReadValue<Vector2>();
    }

    public void Click(InputAction.CallbackContext ctx)
    {
        // Makes ray from mouse position through screen
        // Starts from the camera because rays have to originate from a thing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // This makes a ray that only hits layers as prescribed by the orch
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

        // If the ray has a hit
        // ReadValueAsButton() return a boolean
        // Without this check, the event fires on button down and up
        if (ctx.ReadValueAsButton())
        {      
            _orch.RaycastResolver(hits);
        }
    }

    void Start()
    {
        // Grabs the CharacterController to whatever this is attached to
        _characterController = GetComponent<CharacterController>();
        // This searches the scene for a GO with this tag, should only ever be one Orch
        _orch = GameObject.FindWithTag("Orch").GetComponent<Orch>();
    }

    void FixedUpdate()
    {
        _characterController.Move(PanSpeed * Time.deltaTime * PanInput);
    }
}
