using System.Collections;
using Assets.Common.Scripts.Commands;
using UnityEngine;

namespace Assets.Common.Scripts.View
{
    class TransitionScreen : MonoBehaviour, ITransitionScreen
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GameObject container;
        [SerializeField] private float fadeTime = 0.5f;

        public ICommand Show(bool instant = false)
        {
            return new Command(ShowE(instant));
        }
        private IEnumerator ShowE(bool instant)
        {
            if (instant)
            {
                SetInitState(true);
                yield break;
            }
            else
            {
                SetInitState(false);
                while (canvasGroup.alpha < 1)
                {
                    canvasGroup.alpha += Time.deltaTime/fadeTime;
                    yield return null;
                }
                SetInitState(true);
            }
        }


        public ICommand Hide(bool instant = false)
        {
            return new Command(HideE(instant));
        }
        private IEnumerator HideE(bool instant)
        {
            if (instant)
            {
                SetInitState(false);
                yield break;
            }
            else
            {
                SetInitState(true);
                while (canvasGroup.alpha > 0)
                {
                    canvasGroup.alpha -= Time.deltaTime / fadeTime;
                    yield return null;
                }
                SetInitState(false);
            }
        }

        private void SetInitState(bool stateShowed)
        {
            if (!stateShowed)
                canvasGroup.alpha = 0f;
            else
                canvasGroup.alpha = 1f;

            container.SetActive(stateShowed);
        }
    }
}
