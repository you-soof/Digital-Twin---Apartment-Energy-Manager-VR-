using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XCharts.Runtime;

namespace Script
{
    public class ApplianceEnergyManager : MonoBehaviour
    {
        [Header("References")]
        public List<GameObject> _listOfAppliances;
        public List<ApplianceEnergyTracker> appliances;
        public TMP_Text totalPowerText;
        public TMP_Text totalEnergyText;

        [Header("XCharts")]
        public LineChart lineChart; // Now only LineChart used

        private Dictionary<ApplianceCategory, float> energyPerCategory = new();
        private Dictionary<ApplianceCategory, float> powerPerCategory = new();

        private int timeStep = 0;
        private float totalPower = 0f;
        private float totalEnergy = 0f;

        private void Start()
        {
            foreach (GameObject obj in _listOfAppliances)
            {
                ApplianceEnergyTracker tracker = obj.GetComponentInChildren<ApplianceEnergyTracker>();
                if (tracker != null)
                {
                    appliances.Add(tracker);
                }
                else
                {
                    Debug.LogWarning("No ApplianceEnergyTracker found on " + obj.name);
                }
            }
        }

        private void Update()
        {
            UpdatePowerAndEnergy();
            UpdateTextUI();
            UpdateLineChart();
        }

        public void OnAllAppliances()
        {
            foreach (var appliance in appliances)
            {
                appliance.isOn = true;
            }
        }
        public void OffAllAppliances()
        {
            foreach (var appliance in appliances)
            {
                appliance.isOn = false;
            }
        }

        private void UpdatePowerAndEnergy()
        {
            energyPerCategory.Clear();
            powerPerCategory.Clear();
            totalPower = 0f;
            totalEnergy = 0f;

            foreach (ApplianceCategory category in Enum.GetValues(typeof(ApplianceCategory)))
            {
                energyPerCategory[category] = 0f;
                powerPerCategory[category] = 0f;
            }

            foreach (var appliance in appliances)
            {
                var category = appliance.applianceCategory;
                float power = appliance.isOn ? appliance.powerInWatts : 0f;
                float energy = appliance.totalEnergyConsumed_Wh;

                powerPerCategory[category] += power;
                energyPerCategory[category] += energy;

                totalPower += power;
                totalEnergy += energy;
            }
        }

        private void UpdateTextUI()
        {
            if (totalPowerText)
                totalPowerText.text = $"{(totalPower / 1000f):F2} kW";

            if (totalEnergyText)
                totalEnergyText.text = $"{totalEnergy:F2} Wh";
        }

        private void UpdateLineChart()
        {
            if (lineChart == null)
                return;

            float powerKW = totalPower / 1000f;

            var powerSerie = lineChart.GetSerie("Power (kW)");
            if (powerSerie != null)
            {
                powerSerie.AddData(timeStep.ToString(), powerKW);
            }

            timeStep++;
        }
    }
}
