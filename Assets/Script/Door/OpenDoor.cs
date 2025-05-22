using UnityEngine;
using Sirenix.OdinInspector;
namespace Script.Door
{
    public class OpenDoor : MonoBehaviour
    {
        [SerializeField] private Transform doorHinge;
        [SerializeField] private float openAngle = 90f;
        [SerializeField] private float openSpeed = 2f;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private AudioClip _openSound;
        [SerializeField] private AudioClip _closeSound;

        private bool isOpened = false;
        private Quaternion closedRotation;
        private Quaternion openedRotation;
        private Quaternion currentTargetRotation;
        
        void Start()
        {
            closedRotation = transform.localRotation;
            openedRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
            currentTargetRotation = closedRotation;
            
            _audio.clip = null;
        }

        void Update()
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, currentTargetRotation, Time.deltaTime * openSpeed);
        }

        [Button]
        public void ToggleDoor()
        {
            isOpened = !isOpened;
            currentTargetRotation = isOpened ? openedRotation : closedRotation;
            
            if (_audio != null)
            {
                _audio.clip = isOpened ? _openSound : _closeSound;
                _audio.Play();
            }
        }
    }
}