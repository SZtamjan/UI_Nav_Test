using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui
{
    public class SoulCombatUIController : MonoBehaviour, ISelectHandler
    {
        public void OnSelect(BaseEventData eventData)
        {
            StartCoroutine(SetExistingSoulUiControllerAndChangeCombatUI());
        }

        private IEnumerator SetExistingSoulUiControllerAndChangeCombatUI()
        {
            yield return new WaitUntil(() => GUIController.Instance.SoulsUiControllerInstances.Count > 0);
            GUIController.Instance.SoulsUiControllerInstances[0].HideCombatButtons();
        }
    }
}