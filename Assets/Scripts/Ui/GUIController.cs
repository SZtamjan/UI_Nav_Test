using System;
using System.Collections.Generic;
using Controllers;
using NPCsSystems.Souls;
using Singleton;
using Ui.Views;
using UnityEngine;

namespace Ui
{
    public class GUIController : Singleton<GUIController>
    {
        [SerializeField] private GameObject DisableOnStartObject;

        [SerializeField] private RectTransform ViewsParent;
        [SerializeField] private GameObject InGameGuiObject;
        [SerializeField] private PopUpView PopUp;
        [SerializeField] private PopUpScreenBlocker ScreenBlocker;

        [HideInInspector] public List<SoulsUiController> SoulsUiControllerInstances;
        
        private void Start()
        {
            DisableOnStartObject.SetActive(false);
            if (ScreenBlocker)
                ScreenBlocker.InitBlocker();
        }

        public void ActivateEnemiesButton(bool value)
        {
            foreach (SoulsUiController soulsUiController in SoulsUiControllerInstances)
            {
                soulsUiController.ActivateInteractableButtonsForEnemies(value);
            }
        }

        private void ActiveInGameGUI(bool active)
        {
            InGameGuiObject.SetActive(active);
        }


        public void ShowPopUpMessage(PopUpInformation popUpInfo)
        {
            PopUpView newPopUp = Instantiate(PopUp, ViewsParent) as PopUpView;
            newPopUp.ActivePopUpView(popUpInfo);
        }

        public void ActiveScreenBlocker(bool active, PopUpView popUpView)
        {
            if (active)
                ScreenBlocker.AddPopUpView(popUpView);
            else
                ScreenBlocker.RemovePopUpView(popUpView);
        }


        #region IN GAME GUI Clicks

        public void InGameGUIButton_OnClick(UiView viewToActive)
        {
            viewToActive.ActiveView(() => ActiveInGameGUI(true));

            ActiveInGameGUI(false);
            GameController.Instance.IsPaused = true;
        }

        #endregion
    }
}