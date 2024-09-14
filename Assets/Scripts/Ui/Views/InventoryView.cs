using Controllers;
using NPCsSystems.Souls;
using ScriptableObjectsScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Views
{
    public class InventoryView : UiView
    {

        [Header("Inventory Elements")]
        [SerializeField] private SoulInformation SoulItemPlaceHolder;
        [SerializeField] private Text Description;
        [SerializeField] private Text Name;
        [SerializeField] private Image Avatar;
        [SerializeField] private Button UseButton;
        [SerializeField] private Button DestroyButton;

        private RectTransform ContentParent;
        private GameObject CurrentSelectedGameObject;
        private SoulInformation CurrentSoulInformation;
        public override void Awake()
        {
            base.Awake();
            ContentParent = (RectTransform)SoulItemPlaceHolder.transform.parent;
            InitializeInventoryItems();
        }

        private void InitializeInventoryItems()
        {
            for (int i = 0, j = SoulController.Instance.Souls.Count; i < j; i++)
            {
                SoulInformation newSoul = Instantiate(SoulItemPlaceHolder.gameObject, ContentParent).GetComponent<SoulInformation>();
                newSoul.SetSoulItem(SoulController.Instance.Souls[i], () => SoulItem_OnClick(newSoul));
                if (i == 0) FirstSelected = newSoul.GetComponent<Button>();
            }
            SoulItemPlaceHolder.gameObject.SetActive(false);

        }

        private void OnEnable()
        {
            ClearSoulInformation();
            if (FirstSelected != null) StartCoroutine(SelectFirstButton());
        }

        private void ClearSoulInformation()
        {
            Description.text = "";
            Name.text = "";
            Avatar.sprite = null;
            SetupUseButton(false);
            SetupDestroyButton(false);
            CurrentSelectedGameObject = null;
            CurrentSoulInformation = null;

        }


        public void SoulItem_OnClick(SoulInformation soulInformation)
        {
            CurrentSoulInformation = soulInformation;
            CurrentSelectedGameObject = soulInformation.gameObject;
            SetupSoulInformation(soulInformation.soulItem);
        }


        private void SetupSoulInformation(SoulItem soulItem)
        {
            Description.text = soulItem.Description;
            Name.text = soulItem.Name;
            Avatar.sprite = soulItem.Avatar;
            SetupUseButton(soulItem.CanBeUsed);
            SetupDestroyButton(soulItem.CanBeDestroyed);
        }

        private void SelectElement(int index)
        {

        }

        private void CantUseCurrentSoul()
        {
            PopUpInformation popUpInfo = new PopUpInformation
            {
                DisableOnConfirm = true,
                UseOneButton = true,
                Header = "CAN'T USE",
                Message = "THIS SOUL CANNOT BE USED IN THIS LOCALIZATION"

            };
            GUIController.Instance.ShowPopUpMessage(popUpInfo);
        }

        private void UseCurrentSoul(bool canUse)
        {
            if (!canUse)
            {
                CantUseCurrentSoul();
            }
            else
            {

                //USE SOUL
                Destroy(CurrentSelectedGameObject);
                ClearSoulInformation();
            }

           
        }


        private void DestroyCurrentSoul()
        {
            Destroy(CurrentSelectedGameObject);
            ClearSoulInformation();
        }

        private void SetupUseButton(bool active)
        {
            UseButton.onClick.RemoveAllListeners();
            if (active)
            {
                bool isInCorrectLocalization = GameController.Instance.IsCurrentLocalization(CurrentSoulInformation.soulItem.UsableInLocalization);
                PopUpInformation popUpInfo = new PopUpInformation
                {
                    DisableOnConfirm = isInCorrectLocalization,
                    UseOneButton = false,
                    Header = "USE ITEM",
                    Message = "Are you sure you want to USE: " + CurrentSoulInformation.soulItem.Name + " ?",
                    Confirm_OnClick = () => UseCurrentSoul(isInCorrectLocalization)

                };
                UseButton.onClick.AddListener(() => GUIController.Instance.ShowPopUpMessage(popUpInfo));
            }



            UseButton.gameObject.SetActive(active);

        }

        private void SetupDestroyButton(bool active)
        {
            DestroyButton.onClick.RemoveAllListeners();
            if (active)    
            {
                PopUpInformation popUpInfo = new PopUpInformation
                {
                    DisableOnConfirm = true,
                    UseOneButton = false,
                    Header = "DESTROY ITEM",
                    Message = "Are you sure you want to DESTROY: " + Name.text + " ?",
                    Confirm_OnClick = () => DestroyCurrentSoul()

                };
                DestroyButton.onClick.AddListener(() => GUIController.Instance.ShowPopUpMessage(popUpInfo));
            }

            DestroyButton.gameObject.SetActive(active);
        }



    }
}
