using System;
using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Ui.Views
{
    public class UiView : MonoBehaviour
    {
        [Header("UI VIEW elements")] [SerializeField]
        private bool UnpauseOnClose = false;

        [SerializeField] private bool CloseOnNewView = true;
        [SerializeField] private Button backButon;
        [SerializeField] private Button firstSelected;

        public Button BackButton => backButon;
        
        public Button FirstSelected
        {
            get => firstSelected;
            set => firstSelected = value;
        }

        private UiView ParentView;

        public virtual void Awake()
        {
            backButon.onClick.AddListener(() => DisableView_OnClick(this));
        }

        private void OnEnable()
        {
            if (firstSelected != null)
            {
                StartCoroutine(SelectFirstButton());
            }
        }

        public void ActiveView_OnClick(UiView viewToActive)
        {
            viewToActive.SetParentView(this);
            viewToActive.ActiveView();
            this.ActiveView(!CloseOnNewView);
        }

        private void DisableView_OnClick(UiView viewToDisable)
        {
            viewToDisable.DisableView();
        }

        public void DestroyView_OnClick(UiView viewToDisable)
        {
            viewToDisable.DestroyView();
        }

        public IEnumerator SelectFirstButton()
        {
            yield return new WaitForEndOfFrame();
            firstSelected.Select();
        }
        
        public IEnumerator SelectButton(Button btn)
        {
            yield return new WaitForEndOfFrame();
            btn.Select();
        }

        public void SetParentView(UiView parentView)
        {
            ParentView = parentView;
        }

        public void ActiveView(bool active)
        {
            this.gameObject.SetActive(active);
        }

        public void ActiveView(Action onBackButtonAction = null)
        {
            if (onBackButtonAction != null)
                backButon.onClick.AddListener(() => onBackButtonAction());


            if (!gameObject.activeSelf)
                this.ActiveView(true);
        }

        public void DisableView()
        {
            if (ParentView != null)
            {
                ParentView.ActiveView();
            }


            if (UnpauseOnClose)
                GameController.Instance.IsPaused = false;

            this.ActiveView(false);
        }

        public void DestroyView()
        {
            if (ParentView != null)
            {
                ParentView.ActiveView();
            }

            Destroy(this.gameObject);
        }

        public void DisableBackButton()
        {
            backButon.gameObject.SetActive(false);
        }

        public Button GetBackButton()
        {
            return backButon;
        }
    }
}