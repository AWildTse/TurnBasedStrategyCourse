using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class UnitActionSystem : MonoBehaviour
{
    /// <summary>
    /// Event for when a unit is changed
    /// </summary>
    public event EventHandler OnSelectedUnitChanged;

    /// <summary>
    /// UnitActionSystem static instance to access UnitActionSystem from outside of class
    /// </summary>
    public static UnitActionSystem Instance
    {
        get;
        private set;
    }

    /// <summary>
    /// The variable for the user's selected unit
    /// </summary>
    [SerializeField] private Unit _selectedUnit;
    /// <summary>
    /// The property (read-only) for the user's selected unit
    /// </summary>
    public Unit SelectedUnit
    {
        get => _selectedUnit;
    }
    /// <summary>
    /// The layermask to differentiate what a unit gameobject is from other layers
    /// </summary>
    [SerializeField] private LayerMask _unitLayerMask;

    private void Awake()
    {
        //A null check for Instance- if there's more than one, this means that this script was accessed
        //more than once which should not have happened
        if (Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (TryHandleUnitSelection()) return;

            _selectedUnit.Move(MouseWorld.GetPosition());
        }
    }

    /// <summary>
    /// This methoid reads where the mouse is positioned at time of method call and checks to see if it
    /// hits the correct layermask (which is set to the unit layer).
    /// </summary>
    /// <returns><code>true</code> If our mouse is on a gameobject set to the unit layer that
    /// has a unit script component and returns <code>false</code> If either gameobject does not have the unit
    /// script component or our mouse is not on a unit layer gameobject.</returns>
    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _unitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// sets our private variable and lets our event know that a change has been made.
    /// </summary>
    /// <param name="unit"></param>
    private void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
}
