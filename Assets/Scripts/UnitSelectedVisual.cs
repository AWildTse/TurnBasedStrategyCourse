using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        //subscribe to the event OnSelectedUnitChanged, starts listening for when it's invoked,
        //and when it is, we call method UnitActionSystem_OnSelectedUnitChanged
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

        UpdateVisual();
    }

    /// <summary>
    /// The method called when a unit is changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="empty"></param>
    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }

    /// <summary>
    /// Turns on and off the meshRenderer that has the selected visual on click.
    /// </summary>
    private void UpdateVisual()
    {
        if (UnitActionSystem.Instance.SelectedUnit == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
