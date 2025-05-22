using UnityEngine;

namespace Script
{
    public class ApplianceControl : MonoBehaviour
    {
        [SerializeField] private GameObject _appliance;
        [SerializeField] private ApplianceEnergyTracker _applianceEnergyTracker;

        private void Update()
        {
            if (_applianceEnergyTracker.isOn)
            {
                _appliance.SetActive(true);
            }
            else
            {
                _appliance.SetActive(false);
            }
        }
    }
}
