using Script;
using UnityEngine;
using XCharts.Runtime;

public class EnergyGraph : MonoBehaviour
{
    [SerializeField] private LineChart chart;
    [SerializeField] private ApplianceEnergyTracker applianceEnergyTracker;
    private float timer;
    private float updateInterval = 1f; 

    
    void Start()
    {
        ClearChartData();
        chart.AddSerie<Line>("Energy Consumption");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            float energyNow = GetCurrentEnergy();

            string timeLabel = System.DateTime.Now.ToString("HH:mm:ss");
            chart.AddData(0, energyNow, timeLabel);

            timer = 0f;
        }
    }

    float GetCurrentEnergy()
    {
        if (applianceEnergyTracker != null)
        {
            return applianceEnergyTracker.energyConsumedThisFrame;
        }
        else
        {
            return 0f;
        }
    }

    public void ClearChartData()
    {
        chart.ClearData();
    }
}