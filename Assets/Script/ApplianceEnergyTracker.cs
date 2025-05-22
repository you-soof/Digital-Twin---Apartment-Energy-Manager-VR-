using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public enum ApplianceCategory
    {
        Lighting = 0,
        HeatingCooling = 1,
        Computer =2,
        Circulation = 3,
        Others = 4
        
    }
    public class ApplianceEnergyTracker : MonoBehaviour
    {
        [Header("Appliance Properties")]
        [SerializeField] private string applianceName;
        [SerializeField] private ApplianceCategory applianceCategory;
        [SerializeField] private float powerInWatts;
        [SerializeField] private TMP_Text _currentEnergyText;
        [SerializeField] private TMP_Text _applianceNameText;
        [SerializeField] private TMP_Text _powerText;
        [SerializeField] private TMP_Text _energyText;
        
        public bool isOn = true;

        [Header("Runtime Data")]
        public float totalEnergyConsumed_Wh = 0f;
        private float onTimeSeconds = 0f;
        public float energyConsumedThisFrame;

        private void Start()
        {
            if(_applianceNameText != null) _applianceNameText.text = applianceName;
            
            energyConsumedThisFrame = 0f;
            totalEnergyConsumed_Wh = 0f;
            onTimeSeconds = 0f;
            
            UpdateEnergyDashboard();
        }

        void Update()
        {
            UpdateEnergyDashboard();
            
            float deltaTime = Time.deltaTime;
            onTimeSeconds += deltaTime;
            
            if (isOn)
            {
                energyConsumedThisFrame = (powerInWatts * deltaTime) / 3600f;
                totalEnergyConsumed_Wh += energyConsumedThisFrame;
            }
            else
            {
                energyConsumedThisFrame = 0f;
            }
        }

        private void UpdateEnergyDashboard()
        {
            if(_currentEnergyText) _currentEnergyText.text = $"{energyConsumedThisFrame*1000:F3} mW";
            if(_powerText) _powerText.text = $"{powerInWatts/1000} kW";
            if(_energyText) _energyText.text = totalEnergyConsumed_Wh.ToString("F2") + " Wh";
        }

        public void TogglePower()
        {
            isOn = !isOn;
            Debug.Log($"{applianceName} is now {(isOn ? "ON" : "OFF")}");
        }

        public string GetStatus()
        {
            return $"{applianceName}: {(isOn ? "ON" : "OFF")}, Power: {powerInWatts}W, Energy: {totalEnergyConsumed_Wh:F4} kWh";
        }
    }
}