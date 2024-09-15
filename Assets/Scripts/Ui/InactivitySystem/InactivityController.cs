using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ui.InactivitySystem
{
    public class InactivityController : MonoBehaviour
    {
        [Tooltip("In seconds")] [SerializeField]
        private float inactivityThreshold = 5.0f;

        [Tooltip("In seconds")] [SerializeField]
        private float fadeDuration = 1f;

        [SerializeField] private InputActionAsset inputActions;
        [SerializeField] private CanvasGroup inputInfoCanvasGroup;

        private bool _triggered = true;
        private float _lastInputTime;

        private Coroutine _fadeInCor;
        private bool _afterInactivity = false;

        private void Start()
        {
            _lastInputTime = Time.realtimeSinceStartup;

            foreach (var map in inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    action.performed += OnInputPerformed;
                }
            }

            inputActions.Enable();
        }

        private void OnInputPerformed(InputAction.CallbackContext context)
        {
            _lastInputTime = Time.realtimeSinceStartup;
            _triggered = true;
            
            if(!_afterInactivity) return;
            if (_fadeInCor != null) StopCoroutine(_fadeInCor);
            _fadeInCor = StartCoroutine(FadeInCanvasGroup(false));
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup - _lastInputTime >= inactivityThreshold && _triggered)
            {
                _triggered = false;
                OnInactivity();
            }
        }

        private void OnInactivity()
        {
            //Debug.Log("Lack of activity for " + inactivityThreshold + " seconds.");
            if (_fadeInCor != null) StopCoroutine(_fadeInCor);
            _fadeInCor = StartCoroutine(FadeInCanvasGroup(true));
        }

        private IEnumerator FadeInCanvasGroup(bool value)
        {
            _afterInactivity = value;
            
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float alfa = Mathf.Clamp01(elapsedTime / fadeDuration);

                if (value)
                {
                    inputInfoCanvasGroup.alpha = alfa; //fade in
                }
                else
                {
                    inputInfoCanvasGroup.alpha = 1 - alfa; //fade out
                }


                yield return null;
            }

            if (value) //Just to be sure its visible or not visible all the way
            {
                inputInfoCanvasGroup.alpha = 1f;
            }
            else
            {
                inputInfoCanvasGroup.alpha = 0f;
            }
        }

        private IEnumerator InfoDisplayWaveAnimation()
        {
            //After it's fully visible, we can add small animation where it fades in and out a little
            //or slightly changes its size
            //DOTween would be the best solution
            yield return null;
        }

        private void OnDestroy()
        {
            foreach (var map in inputActions.actionMaps)
            {
                foreach (var action in map.actions)
                {
                    action.performed -= OnInputPerformed;
                }
            }
        }
    }
}