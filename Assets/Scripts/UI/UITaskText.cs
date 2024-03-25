using TestTask.Collector;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestTask.UI
{
    public class UITaskText : MonoBehaviour
    {
        [SerializeField] private CellCollector cellCollector;

        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float targetAlpha = 1f;

        private Text textComponent;
        private Tween tweenText;
        private void Awake()
        {
            cellCollector.AssignedCorrectSymbol += SetText;
        }
        private void Start()
        {

            if (textComponent == null) textComponent = GetComponent<Text>();

            Color startColor = textComponent.color;
            startColor.a = 0f;
            textComponent.color = startColor;

            tweenText = textComponent.DOFade(targetAlpha, fadeDuration);
        }
        private void OnDestroy()
        {
            cellCollector.AssignedCorrectSymbol -= SetText;
            tweenText.Kill();
        }

        public void SetText(Cell cell)
        {
            if (textComponent == null) textComponent = GetComponent<Text>();
            textComponent.text = "Find " + cell.MySymbols;
        }
    }
}