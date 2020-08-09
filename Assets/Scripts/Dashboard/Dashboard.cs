using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Dashboard : MonoBehaviour
{
    public enum DashboardType { Tutorial, Home, Animal };
    public DashboardType dashboardType { get; set; }
    public abstract void Setup();
    public abstract void Cleanup();

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        Cleanup();
    }
}
