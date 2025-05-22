using UnityEngine;

namespace Script
{
    public class TVControl : MonoBehaviour
    {
        [SerializeField] private GameObject _screen;
        [SerializeField] private ApplianceEnergyTracker _tvEnergyTracker;

        private void Update()
        {
            if (_tvEnergyTracker.isOn)
            {
                _screen.SetActive(true);
            }
            else
            {
                _screen.SetActive(false);
            }
        }
    }
}
