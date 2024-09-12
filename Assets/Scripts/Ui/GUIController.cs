using Controllers;
using Ui.Views;
using UnityEngine;

namespace Ui
{
    public class GUIController : MonoBehaviour
    {
        #region singleton

        public static GUIController Insntace;

        private void Awake()
        {
            DisableOnStartObject.SetActive(false);
            Insntace = this;
        }

        #endregion

        [SerializeField] private GameObject DisableOnStartObject;

        [SerializeField] private RectTransform ViewsParent;
        [SerializeField] private GameObject InGameGuiObject;
        [SerializeField] private PopUpView PopUp;
        [SerializeField] private PopUpScreenBlocker ScreenBlocker;

        private void Start()
        {
            if (ScreenBlocker)
                ScreenBlocker.InitBlocker();
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