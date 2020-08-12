using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDashboard : Dashboard
{
    public new DashboardType dashboardType;

    private DashboardController _dashboardController;
    private TutorialController _tutorialController;


    public override void Setup()
    {
        _dashboardController = DashboardController.Instance;
        _tutorialController = gameObject.GetComponent<TutorialController>();

        _tutorialController.enabled = true;
        _tutorialController.tutorialEnd.AddListener(End);
    }

    private void End()
    {
        _dashboardController.UpdateDashboard(DashboardType.Home);
    }

    public override void Cleanup()
    {
        if (_tutorialController != null)
        {
            _tutorialController.tutorialEnd.RemoveListener(End);
            _tutorialController.enabled = false;
        }
    }
}
