using System.Collections;
using System.Collections.Generic;
using Anivision.NotebookSystem;
using UnityEngine;
using Anivision.Tutorial;
using TMPro;

namespace Anivision.Dashboard
{
    /// <summary>
    /// The dashboard for tutorials that finds the TutorialController in the scene to go through the tutorial steps
    /// and listens for the tutorialEnd event to replace this tutorial dashboard with the home dashboard.
    /// </summary>
    public class TutorialDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.Tutorial;
        
        public TextMeshPro TMP;
        public Button skipButton;
        public Button startButton;

        private DashboardController _dashboardController;
        private TutorialController _tutorialController;

        public override void Setup()
        {
            TMP.enabled = true;
            skipButton.gameObject.SetActive(true);
        }

        public override void Cleanup()
        {
            TMP.enabled = false;
            skipButton.gameObject.SetActive(false);
        }
    }
}
