using System.Collections;
using System.Collections.Generic;
using Anivision.Notebook;
using UnityEngine;

namespace Anivision.Dashboard
{
    /// <summary>
    /// The dashboard for vision select that listens for presses that goes back to the home dashboard.
    /// </summary>
    public class VisionSelectDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.VisionSelect;

        [Tooltip("Button to go back to the home dashboard.")]
        public Button backToHome;

        private DashboardController _dashboardController;


        public override void Setup()
        {
            _dashboardController = DashboardController.Instance;

            backToHome.enabled = true;
            backToHome.onClick.AddListener(ToHomeDashboard);
        }

        private void ToHomeDashboard()                                                      // swap out this current vision dashboard for the home dashboard
        {
            _dashboardController.UpdateDashboard(DashboardType.Home);
        }

        public override void Cleanup()
        {
            backToHome.onClick.RemoveListener(ToHomeDashboard);
            backToHome.enabled = false;
        }
    }
}
