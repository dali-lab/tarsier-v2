using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anivision.Dashboard
{
    public class VisionSelectDashboard : Dashboard
    {
        public override DashboardType dashboardType => DashboardType.VisionSelect;

        public Button backToHome;
        public Button [] otherAnimalButtons;

        private DashboardController _dashboardController;


        public override void Setup()
        {
            _dashboardController = DashboardController.Instance;

            foreach (Button button in otherAnimalButtons)
            {
                button.enabled = true;
                button.onClick.AddListener(VisionSwitch);
            }

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
            foreach (Button button in otherAnimalButtons)
            {
                button.enabled = false;
                button.onClick.RemoveListener(VisionSwitch);
            }

            backToHome.onClick.RemoveListener(ToHomeDashboard);
            backToHome.enabled = false;
        }
    }
}
