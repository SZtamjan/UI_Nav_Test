using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui.Views
{
    public class PopUpView : UiView
    {
        public GameObject PopUpScreenBlocker;

        public override void Awake()
        {
            GetBackButton().onClick.AddListener(() => DestroyView_OnClick(this));
        }

        private void OnEnable()
        {
            GUIController.Instance.ActiveScreenBlocker(true, this);
            StartCoroutine(SelectFirstButton());
        }

        private void OnDisable()
        {
            GUIController.Instance.ActiveScreenBlocker(false, this);
        }

        [Header("Pop Up Elements")] public Text LabelText;
        public Text MessageText;
        public Button YesButton;


        public void ActivePopUpView(PopUpInformation popUpInfo)
        {
            GUIController guiController = GUIController.Instance;
            InventoryView invView = guiController.InventoryView;
            
            guiController.ActivateInventoryButtons(false);
            
            ClearPopUp();
            LabelText.text = popUpInfo.Header;
            MessageText.text = popUpInfo.Message;

            if (popUpInfo.UseOneButton)
            {
                invView.SetInteractableButtonsOnPopUpView(false);
                DisableBackButton();
                YesButton.GetComponentInChildren<Text>().text = "OK";
                YesButton.onClick.AddListener(() => invView.SetInteractableButtonsOnPopUpView(true));
                StartCoroutine(SelectButton(YesButton));
            }
            
            if (popUpInfo.Confirm_OnClick != null)
                YesButton.onClick.AddListener(() => popUpInfo.Confirm_OnClick());

            if (popUpInfo.ButtonControl_OnClick != null)
                YesButton.onClick.AddListener(() => popUpInfo.ButtonControl_OnClick());

            if (popUpInfo.DisableOnConfirm)
            {
                if(!popUpInfo.UseOneButton)
                {
                    YesButton.onClick.AddListener(invView.activePopUpButton.Clear);
                    YesButton.onClick.AddListener(invView.SelectLastSelected);
                    YesButton.onClick.AddListener(() => guiController.ActivateInventoryButtons(true));
                }
                
                YesButton.onClick.AddListener(() => DestroyView());
            }
            
            if (!popUpInfo.UseOneButton)
            {
                invView.activePopUpButton.Add(YesButton);
                invView.activePopUpButton.Add(BackButton);
                
                BackButton.onClick.AddListener(invView.activePopUpButton.Clear);
                BackButton.onClick.AddListener(invView.SelectLastSelected);
                BackButton.onClick.AddListener(() => guiController.ActivateInventoryButtons(true));
                
                StartCoroutine(SelectButton(FirstSelected));
            }

            ActiveView();
        }

        private void ClearPopUp()
        {
            LabelText.text = "";
            MessageText.text = "";
            YesButton.onClick.RemoveAllListeners();
        }
    }

    public struct PopUpInformation
    {
        public bool UseOneButton;
        public bool DisableOnConfirm;
        public string Header;
        public string Message;
        public Action Confirm_OnClick;
        public Action ButtonControl_OnClick;
    }
}