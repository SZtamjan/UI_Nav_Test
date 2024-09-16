using System;
using System.Collections.Generic;
using NPCsSystems.Enemies;
using NPCsSystems.Souls;
using UnityEngine;
using UnityEngine.EventSystems;
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

        public void HideCombatButtons()
        {
            List<SoulsUiController> souls = GUIController.Instance.SoulsUiControllerInstances;
            foreach (SoulsUiController soul in souls)
            {
                if(!soul.TryGetComponent(out SoulEnemy soulEnemy))
                {
                    Debug.LogWarning($"No {nameof(SoulEnemy)} on {soul.name}");
                    continue;
                }
                soulEnemy.HideCombatWithEnemy();
            }
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
            if(parent == null) return;
            foreach (Transform child in parent)
            {
                if(!child.TryGetComponent(out Button btn))
                {
                    Debug.LogWarning($"No button on {child.name}");
                    continue;
                }

                btn.interactable = value;
            }
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
    }
}