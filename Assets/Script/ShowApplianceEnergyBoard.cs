using UnityEngine;

namespace Script
{
    public class ShowApplianceEnergyBoard : MonoBehaviour
    {
        [SerializeField] private GameObject applianceEnergyBoard;

        private void ShowEnergyBoard()
        {
            applianceEnergyBoard.SetActive(true);
        }
        
        private void HideEnergyBox()
        {
            applianceEnergyBoard.SetActive(false);
        }
    }
}
