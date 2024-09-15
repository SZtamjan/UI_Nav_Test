using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using NPCsSystems.Souls;
using Singleton;
using Ui.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GUIController : Singleton<GUIController>
    {
        [SerializeField] private GameObject DisableOnStartObject;
        
        [SerializeField] private Button closeWindow;
        [SerializeField] private Scrollbar scrollObj;
        [SerializeField] private InventoryView inventoryView;
        [SerializeField] private RectTransform ViewsParent;
        [SerializeField] private GameObject InGameGuiObject;
        [SerializeField] private PopUpView PopUp;
        [SerializeField] private PopUpScreenBlocker ScreenBlocker;

        public List<Button> inventoryButtons = new List<Button>();

        [HideInInspector] public List<SoulsUiController> SoulsUiControllerInstances;
        
        public InventoryView InventoryView => inventoryView;
        
        private void Awake()
        {
            inventoryButtons.Add(closeWindow);
        }

        private void Start()
        {
            DisableOnStartObject.SetActive(false);
            if (ScreenBlocker)
                ScreenBlocker.InitBlocker();
        }

        public void SelectInventoryButton()
        {
            StartCoroutine(SelectInventoryButtonCor());
        }

        private IEnumerator SelectInventoryButtonCor()
        {
            yield return new WaitUntil(() => inventoryButtons[10].interactable); //number set randomly, just waiting for buttons to be interactable
            if(InventoryView.FirstSelected == null) InventoryView.CheckFirstSelectedButtonAndSetNewOne();
            InventoryView.FirstSelected.Select();
        }
        
        public void ActivateInventoryButtons(bool value)
        {
            scrollObj.interactable = value;
            foreach (Button btn in inventoryButtons)
            {
                btn.interactable = value;
            }
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