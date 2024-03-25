
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestTask.UI
{
    public class PanelResetGame : MonoBehaviour
    {
        [SerializeField] private float endFade;
        [SerializeField] private float duration;

        private Tween tweenImage;

        private void OnEnable()
        {
            Image image = GetComponent<Image>();

            tweenImage?.Kill();
            tweenImage = image.DOFade(endFade, duration);
        }

        private void OnDestroy()
        {
            tweenImage.Kill();
        }
    }
}