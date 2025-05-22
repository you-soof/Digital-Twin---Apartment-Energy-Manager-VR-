using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class MultipleApplianceEnergyTracker : MonoBehaviour
    {
        [Header("List of Appliances")]
        [SerializeField] private List<GameObject> _listofAppliances = new List<GameObject>();
        private List<ApplianceEnergyTracker> _appliances = new List<ApplianceEnergyTracker>();
        
        [Header("Switch Properties")]
        [SerializeField] private string applianceName;
        
        [SerializeField] private TMP_Text _currentEnergyText;
        [SerializeField] private TMP_Text _applianceNameText;
        [SerializeField] private TMP_Text _powerText;
        [SerializeField] private TMP_Text _energyText;
        
        public bool isOn = true;

        [Header("Runtime Data")]
        public float totalEnergyConsumed = 0f;
        // private float onTimeSeconds = 0f;
        public float energyConsumedPerFrame = 0f;
        public float totalAppliancesPower = 0f;

        private void Start()
        {
            
            foreach (GameObject obj in _listofAppliances)
            {
                ApplianceEnergyTracker tracker = obj.GetComponentInChildren<ApplianceEnergyTracker>();
                if (tracker != null)
                {
                    _appliances.Add(tracker);
                }
                else
                {
                    Debug.LogWarning("No ApplianceEnergyTracker found on " + obj.name);
                }
            }
            
            if(_applianceNameText != null) _applianceNameText.text = applianceName;
            
            // energyConsumedThisFrame = 0f;
            // totalEnergyConsumed_Wh = 0f;
            // onTimeSeconds = 0f;
            
            if (_powerText)
            {
                foreach (ApplianceEnergyTracker appliance in _appliances)
                {
                    totalAppliancesPower += appliance.powerInWatts;
                }
                _powerText.text = $"{totalAppliancesPower/1000} kW";
            }
            
            UpdateEnergyDashboard();
        }

        void Update()
        {
            UpdateEnergyDashboard();
            
            // float deltaTime = Time.deltaTime;
            // onTimeSeconds += deltaTime;
            //
            // if (isOn)
            // {
            //     energyConsumedThisFrame = (powerInWatts * deltaTime) / 3600f;
            //     totalEnergyConsumed_Wh += energyConsumedThisFrame;
            // }
            // else
            // {
            //     energyConsumedThisFrame = 0f;
            // }
        }

        private void UpdateEnergyDashboard()
        {
            if (_currentEnergyText)
            {
                foreach (ApplianceEnergyTracker appliance in _appliances)
                {
                    energyConsumedPerFrame += appliance.energyConsumedThisFrame;
                }
                _currentEnergyText.text = $"{energyConsumedPerFrame*1000:F3} mW";
            }
            
            if (_energyText)
            {
                foreach (ApplianceEnergyTracker appliance in _appliances)
                {
                    totalEnergyConsumed += appliance.totalEnergyConsumed_Wh;
                }
                _energyText.text = totalEnergyConsumed.ToString("F2") + " Wh";
            }
        }

        public void TogglePower()
        {
            foreach (ApplianceEnergyTracker appliance in _appliances)
            {
                appliance.isOn = !appliance.isOn;
            }
        }

        // public string GetStatus()
        // {
        //     return $"{applianceName}: {(isOn ? "ON" : "OFF")}, Power: {powerInWatts}W, Energy: {totalEnergyConsumed_Wh:F4} kWh";
        // }
    }
}
