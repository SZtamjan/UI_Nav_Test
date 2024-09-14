using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui.Scroll
{
    public class ScrollController : MonoBehaviour //, ISelectHandler
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform contentPanel;
        [SerializeField] private float scrollSpeed = 0.1f;

        private RectTransform lastSelected;

        //This Update method can be probably changed to OnSelect method, Interface above, method semi-done below (done but doesn't work for some reason)
        private void Update()
        {
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

            if (selectedObject == null || selectedObject.transform.parent != contentPanel) return;

            if (!selectedObject.TryGetComponent(out RectTransform selectedRect))
            {
                Debug.LogWarning(
                    $"This shouldn't occur, not {nameof(RectTransform)} in selected {selectedObject.name} GO");
                return;
            }

            if (selectedRect != lastSelected)
            {
                lastSelected = selectedRect;
                for (int i = 0; i < 10; i++) //Calling it few times to make sure it scrolls to the right place
                {
                    ScrollTo(selectedRect);
                }
            }
        }

        // public void OnSelect(BaseEventData eventData)
        // {
        //     GameObject selectedObject = eventData.selectedObject;
        //     
        //     if (selectedObject == null || selectedObject.transform.parent != contentPanel) return;
        //     
        //     if (!selectedObject.TryGetComponent(out RectTransform selectedRect))
        //     {
        //         Debug.LogWarning($"This shouldn't occur, not {nameof(RectTransform)} in selected {selectedObject.name} GO");
        //         return;
        //     }
        //     
        //     if(selectedRect != lastSelected)
        //     {
        //         lastSelected = selectedRect;
        //         for (int i = 0; i < 10; i++)//Calling it few times to make sure it scrolls to the right place
        //         {
        //             ScrollTo(selectedRect);
        //         }
        //     }
        // }

        private void ScrollTo(RectTransform target)
        {
            Canvas.ForceUpdateCanvases();

            Vector3[] scrollRectCorners = new Vector3[4];
            scrollRect.viewport.GetWorldCorners(scrollRectCorners);

            Vector3[] targetCorners = new Vector3[4];
            target.GetWorldCorners(targetCorners);

            float targetMinY = targetCorners[0].y;
            float targetMaxY = targetCorners[2].y;

            float scrollDelta = 0;

            if (targetMaxY > scrollRectCorners[2].y)
            {
                scrollDelta = targetMaxY - scrollRectCorners[2].y;
            }
            else if (targetMinY < scrollRectCorners[0].y)
            {
                scrollDelta = targetMinY - scrollRectCorners[0].y;
            }

            if (scrollDelta != 0)
            {
                float contentHeight = contentPanel.rect.height;
                float scrollHeight = scrollRect.viewport.rect.height;
                float maxScrollY = contentHeight - scrollHeight;

                Vector2 currentScrollPos = scrollRect.normalizedPosition;

                float targetScrollY = Mathf.Clamp01(currentScrollPos.y + scrollDelta / maxScrollY);

                scrollRect.normalizedPosition = new Vector2(currentScrollPos.x, targetScrollY);
            }
        }
    }
}