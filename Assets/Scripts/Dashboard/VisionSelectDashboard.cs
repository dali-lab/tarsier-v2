using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anivision.Dashboard
{
    public class VisionSelectDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.VisionSelect;

        public Button backToHome;
        public Button beeButton;

        private DashboardController _dashboardController;


        public override void Setup()
        {
            _dashboardController = DashboardController.Instance;

            beeButton.enabled = true;
            beeButton.onClick.AddListener(VisionSwitch);

            backToHome.enabled = true;
            backToHome.onClick.AddListener(ToHomeDashboard);
        }

        private void VisionSwitch()
        {
            //TODO: switch to other animal vision
        }

        private void ToHomeDashboard()
        {
            _dashboardController.UpdateDashboard(DashboardType.Home);
        }

        public override void Cleanup()
        {
            beeButton.onClick.RemoveListener(VisionSwitch);
            beeButton.enabled = false;

            backToHome.onClick.RemoveListener(ToHomeDashboard);
            backToHome.enabled = false;
        }
    }
}
