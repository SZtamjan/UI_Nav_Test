using UnityEngine;
using UnityEngine.InputSystem;

namespace Ui.InactivitySystem
{
    public class InactivityController : MonoBehaviour
    {
        [Tooltip("In seconds")] public float inactivityThreshold = 5.0f;
        [SerializeField] private InputActionAsset inputActions;

        private bool _triggered = true;
        private float _lastInputTime;
        
        private void Start()
        {
            _lastInputTime = Time.time;

            foreach (var map in inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    action.performed += OnInputPerformed;
                }
            }

            inputActions.Enable();
        }

        private void OnInputPerformed(InputAction.CallbackContext context)
        {
            _lastInputTime = Time.time;
            _triggered = true;
        }

        private void Update()
        {
            if (Time.time - _lastInputTime >= inactivityThreshold && _triggered)
            {
                _triggered = false;
                OnInactivity();
            }
        }

        private void OnInactivity()
        {
            Debug.Log("Lack of activity for " + inactivityThreshold + " seconds.");
        }

        private void OnDestroy()
        {
            foreach (var map in inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    action.performed -= OnInputPerformed;
                }
            }
        }
    }
}