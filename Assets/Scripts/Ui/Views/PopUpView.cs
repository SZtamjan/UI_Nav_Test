using System;
using UnityEngine;
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
        }
        private void OnDisable()
        {
            GUIController.Instance.ActiveScreenBlocker(false, this);
        }

        [Header("Pop Up Elements")]
        public Text LabelText;
        public Text MessageText;
        public Button YesButton;

    
        public void ActivePopUpView(PopUpInformation popUpInfo)
        {
            ClearPopUp();
            LabelText.text = popUpInfo.Header;
            MessageText.text = popUpInfo.Message;    

            if(popUpInfo.UseOneButton)
            {
                DisableBackButton();
                YesButton.GetComponentInChildren<Text>().text = "OK";
            }

            if(popUpInfo.Confirm_OnClick != null)
                YesButton.onClick.AddListener(() => popUpInfo.Confirm_OnClick());  

            if(popUpInfo.DisableOnConfirm)
                YesButton.onClick.AddListener(() => DestroyView());

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
    }
}