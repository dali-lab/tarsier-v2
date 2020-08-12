﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardController : MonoBehaviour
{
    public Dashboard[] dashboards;
    public Dashboard currentDashboard;

    Dictionary<Dashboard.DashboardType, Dashboard> _dashboardDict = new Dictionary<Dashboard.DashboardType, Dashboard>();

    private static DashboardController _dashboardController;
    //singleton instance
    public static DashboardController Instance
    {
        get
        {
            if (!_dashboardController)
            {
                _dashboardController = FindObjectOfType(typeof(DashboardController)) as DashboardController;
                if (!_dashboardController)
                {
                    UnityEngine.Debug.LogError("There needs to be one active DashboardController script on a GameObject in your scene.");
                }
            }
            return _dashboardController;
        }
    }

    private void Start()
    {
        foreach (Dashboard dashboard in dashboards)
        {
            if (!_dashboardDict.ContainsKey(dashboard.dashboardType))
            {
                _dashboardDict.Add(dashboard.dashboardType, dashboard);
            }
            else
            {
                UnityEngine.Debug.LogError("Dashboard type already declared. Skipping add to dictionary.");
            }
        }
        foreach (Dashboard dashboard in dashboards)
        {
            dashboard.Cleanup();
            dashboard.gameObject.SetActive(false);

        }
        currentDashboard.gameObject.SetActive(true);
        currentDashboard.Setup();
    }

    public void UpdateDashboard(Dashboard.DashboardType newDashboard)
    {
        currentDashboard.Cleanup();
        currentDashboard.gameObject.SetActive(false);
        currentDashboard = _dashboardDict[Dashboard.DashboardType.Home];//newDashboard];
        currentDashboard.gameObject.SetActive(true);
        currentDashboard.Setup();
    }
}
