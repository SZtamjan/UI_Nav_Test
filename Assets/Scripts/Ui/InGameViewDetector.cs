using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
    public class InGameViewDetector : MonoBehaviour
    {
        private GUIController _guiController;

        private void Awake()
        {
            _guiController = GUIController.Instance;
        }

        private void OnEnable()
        {
            if(_guiController == null) return;
            if (_guiController.SoulsUiControllerInstances == null) _guiController.SoulsUiControllerInstances = new List<SoulsUiController>();
            _guiController.ActivateEnemiesButton(true);
        }
        private void OnDisable()
        {
            if(_guiController == null) return;
            if (_guiController.SoulsUiControllerInstances == null) _guiController.SoulsUiControllerInstances = new List<SoulsUiController>();
            _guiController.ActivateEnemiesButton(false);
        }
    }
}