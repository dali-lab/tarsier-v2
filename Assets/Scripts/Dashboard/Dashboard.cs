using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Anivision.Dashboard
{
    public abstract class Dashboard : MonoBehaviour
    {
        public enum DashboardType { Tutorial, Home, VisionSelect };
        public abstract DashboardType dashboardType { get; }
        public abstract void Setup();
        public abstract void Cleanup();
    }
}
