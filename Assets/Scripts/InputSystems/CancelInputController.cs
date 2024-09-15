using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace InputSystems
{
    public class CancelInputController : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;

        [SerializeField] private List<Button> backButtons;

        private void Start()
        {
            inputActions.actionMaps[1].actions[2].performed += CancelledPressed;
        }

        private void CancelledPressed(InputAction.CallbackContext context)
        {
            foreach (Button b in backButtons)
            {
                if (!b.gameObject.activeInHierarchy || !b.interactable) continue;
                Debug.Log($"Button {b.name} on parent {b.transform.parent}");
                b.onClick.Invoke();
                break;
            }
        }
        
        private void OnDestroy()
        {
            inputActions.actionMaps[1].actions[2].performed -= CancelledPressed;
        }
    }
}