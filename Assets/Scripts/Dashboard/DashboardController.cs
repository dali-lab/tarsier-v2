using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashboardController : MonoBehaviour
{
    public Dashboard[] dashboards;
    public Dashboard currentDashboard;

    Dictionary<Dashboard.DashboardType, Dashboard> _dashboardDict = new Dictionary<Dashboard.DashboardType, Dashboard>();

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
    }



    public void UpdateDashboard(Dashboard.DashboardType newDashboard)
    {
        currentDashboard.Cleanup();
        currentDashboard.gameObject.SetActive(false);
        currentDashboard = _dashboardDict[newDashboard];
        currentDashboard.Setup();
    }
}
