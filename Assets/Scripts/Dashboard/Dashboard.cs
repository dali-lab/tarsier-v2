using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Dashboard : MonoBehaviour
{
    public enum DashboardType { Tutorial, Home, VisionSelect };
    public DashboardType dashboardType { get; set; }
    public abstract void Setup();
    public abstract void Cleanup();

}
