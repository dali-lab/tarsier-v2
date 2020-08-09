using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToDashboardHome : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoHome);
    }

    private void GoHome()
    {
        //DashboardController.UpdateDashboard(Dashboard.DashboardType.Home);
    }
}
