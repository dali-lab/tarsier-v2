using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anivision.Tutorial;

namespace Anivision.Dashboard
{
    public class TutorialDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.Tutorial;

        private DashboardController _dashboardController;
        private TutorialController _tutorialController;


        public override void Setup()
        {
            _dashboardController = DashboardController.Instance;
            if (_dashboardController == null) throw new System.Exception("Must have an dashboard controller in the scene");

            _tutorialController = gameObject.GetComponent<TutorialController>();
            if (_tutorialController == null) throw new System.Exception("Must have an tutorial controller on this object");

            _tutorialController.enabled = true;
            _tutorialController.tutorialEnd.AddListener(End);
        }

        private void End()
        {
            _dashboardController.UpdateDashboard(DashboardType.Home);           // swap out this current tutorial dashboard for the home dashboard
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
}
