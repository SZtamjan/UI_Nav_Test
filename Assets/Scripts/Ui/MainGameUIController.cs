using System;
using System.Collections;
using System.Collections.Generic;
using NPCsSystems.Souls;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
    public class MainGameUIController : MonoBehaviour
    {
        [SerializeField] private Button pauseButton;

        private GUIController _guiController;

        private void Awake()
        {
            SetupVariables();
            
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(.2f);
            SelectMiddleSoul();
        }
        
        

        #region MiddleSoulSelection
        
        public void SelectMiddleSoul()
        {
            SoulEnemy soul = DetectMiddleSoul();

            if (soul == null) //No enemies/No buttons
            {
                pauseButton.Select();
                return;
            }
            
            foreach (Transform child in soul.InteractionPanelObjectProperty.transform)
            {
                if (!child.TryGetComponent(out Button btn))
                {
                    Debug.LogWarning($"Lack of {nameof(Button)} in GameObject {child.name}");
                    return;
                }

                if (!btn.gameObject.activeInHierarchy) continue;

                btn.Select();
                return;
            }

            foreach (Transform child in soul.ActionsPanelObjectProperty.transform)
            {
                if (!child.TryGetComponent(out Button btn))
                {
                    Debug.LogWarning($"Lack of {nameof(Button)} in GameObject {child.name}");
                    return;
                }

                if (!btn.gameObject.activeInHierarchy) continue;

                btn.Select();
                return;
            }

            Debug.LogError("Fatal exception, no buttons selected");
        }

        private SoulEnemy DetectMiddleSoul()
        {
            int middleSoulIndex = CompareSoulsPositions();

            //if -1 then select buttons above
            if (middleSoulIndex < 0) return null;

            return _guiController.SoulsUiControllerInstances[middleSoulIndex].GetComponent<SoulEnemy>();
        }

        private int CompareSoulsPositions()
        {
            //If there are no enemies then return exception
            if (_guiController.SoulsUiControllerInstances.Count == 0) return -1;

            //If there are less than 3 then select the first one in the list
            if (_guiController.SoulsUiControllerInstances.Count < 3) return 0;

            List<float> soulsPositions = new List<float>();
            foreach (SoulsUiController soul in _guiController.SoulsUiControllerInstances)
            {
                soulsPositions.Add(soul.transform.position.x);
            }

            soulsPositions.Sort(); //Positions sorted
            float epsilon = 0.01f; //Used to compare floats to be sure if value is identical

            for (int i = 0; i < 2; i++)
            {
                //Detecting which one enemy is in the middle
                if (Mathf.Abs(soulsPositions[1] - _guiController.SoulsUiControllerInstances[i].transform.position.x) <
                    epsilon) return i;
            }

            return -1;
        }
        
        #endregion

        private void SetupVariables()
        {
            if (!TryGetComponent(out GUIController guiController))
            {
                Debug.LogWarning($"Lack of {nameof(GUIController)} on GameObject {gameObject.name}");
                return;
            }

            _guiController = guiController;
        }
    }
}