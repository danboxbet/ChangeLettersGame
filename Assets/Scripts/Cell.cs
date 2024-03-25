using TestTask.Collector;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TestTask.UI;
using System;

namespace TestTask
{
    public class Cell : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private UICell cellUI;

        public event Action EnterCorrectAnswer;

        private CellCollector cellCollector;

        private bool isRightAnswer;

        Tween scaleTween;
        Tween shakeTween;

        private Vector3 originalScale;

        private string mySymbols;
        public string MySymbols => mySymbols;

        #region Unity_Events
        private void Awake()
        {
            cellCollector = FindObjectOfType<CellCollector>();
            cellCollector.AssignedCorrectSymbol += CheckRightMySymbols;
        }

        private void Start()
        {
            originalScale = transform.localScale;
        }

        private void OnDestroy()
        {
            cellCollector.AssignedCorrectSymbol -= CheckRightMySymbols;

            scaleTween.Kill();
            shakeTween.Kill();
        }
        #endregion

        #region Public
        public void SetSprite(Sprite sprite, Vector3 rotation)
        {
            cellUI.ChangeUI(sprite, rotation);
        }
        public void SetSymbol(string symbols)
        {
            mySymbols = symbols;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (isRightAnswer)
            {
                ShowRightSolution();
            }

            if (!isRightAnswer) ShowFalseSolution();
        }
        #endregion

        #region Private
        private void CheckRightMySymbols(Cell cell)
        {
            if (cell == this) isRightAnswer = true;
            else isRightAnswer = false;
        }

        private void ShowRightSolution()
        {
            scaleTween?.Kill();

            scaleTween = transform.DOScale(1.2f, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, 0.5f)
                    .SetEase(Ease.OutQuad)
                    .OnComplete(() =>
                    {
                        EnterCorrectAnswer?.Invoke();
                    });
            });

        }
        private void ShowFalseSolution()
        {
            if (shakeTween != null && shakeTween.IsActive())
            {
                shakeTween.OnComplete(() => {
                    shakeTween = transform.DOShakePosition(0.5f, new Vector3(20f, 0, 0), 10, 90, false, true);
                });
            }
            else
            {
                shakeTween = transform.DOShakePosition(0.5f, new Vector3(20f, 0, 0), 10, 90, false, true);
            }
        }
        #endregion
    }
}