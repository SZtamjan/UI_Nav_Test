using System;
using NPCsSystems.Enemies;
using NPCsSystems.Souls;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class SoulsUiController : MonoBehaviour
    {
        private Transform _interactionPanelObject;
        private Transform _actionsPanelObject;

        private void Awake()
        {
            SetupVariables();
            SendDataToGUIController();
        }

        private void SetupVariables()
        {
            if (!TryGetComponent(out SoulEnemy soulEnemy))
            {
                Debug.LogWarning($"Lack of {nameof(SoulEnemy)} on GameObject {gameObject.name}");
                return;
            }

            _interactionPanelObject = soulEnemy.InteractionPanelObjectProperty.transform;
            _actionsPanelObject = soulEnemy.ActionsPanelObjectProperty.transform;
        }

        private void SendDataToGUIController()
        {
            GUIController.Instance.SoulsUiControllerInstances.Add(this);
        }
        
        public void ActivateInteractableButtonsForEnemies(bool value)
        {
            Iterate(_interactionPanelObject,value);
            Iterate(_actionsPanelObject,value);
        }
        
        private void Iterate(Transform parent, bool value)
        {
            foreach (Transform child in parent)
            {
                if(!child.TryGetComponent(out Button btn))
                {
                    Debug.LogWarning($"No button on {child.name}");
                    continue;
                }
                Debug.Log($"Zmieniono interactable na {btn.name}");
                btn.interactable = value;
            }
        }
    }
}