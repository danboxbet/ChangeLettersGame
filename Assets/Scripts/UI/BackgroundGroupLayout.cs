using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    public class BackgroundGroupLayout : MonoBehaviour
    {
        [SerializeField] private CellSpawner cellSpawner;

        public GridLayoutGroup gridLayoutGroup;
        public Image backgroundImage;

        #region Unity_Events
        private void Start()
        {
            cellSpawner.ChangeSizeCollection += SetSize;

            SetSize();
        }
        private void OnDestroy()
        {
            cellSpawner.ChangeSizeCollection -= SetSize;
        }
        #endregion

        #region Public
        public void SetSize()
        {
            StartCoroutine(ChangeSize(0.01f));
        }
        #endregion

        #region Private
        private Vector2 CalculateContentSize()
        {
            int itemCount = gridLayoutGroup.transform.childCount;

            Vector2 cellSize = gridLayoutGroup.cellSize;

            Vector2 spacing = gridLayoutGroup.spacing;

            int columns = Mathf.CeilToInt((float)itemCount / gridLayoutGroup.constraintCount);

            float contentWidth = columns * cellSize.x + (columns - 1) * spacing.x;

            float contentHeight = Mathf.CeilToInt((float)itemCount / columns) * cellSize.y + (Mathf.CeilToInt((float)itemCount / columns) - 1) * spacing.y;

            return new Vector2(contentHeight + 70, contentWidth + 70);
        }
        #endregion

        #region Coroutine
        IEnumerator ChangeSize(float time)
        {
            yield return new WaitForSeconds(time);

            Vector2 contentSize = CalculateContentSize();

            backgroundImage.rectTransform.sizeDelta = contentSize;
            backgroundImage.rectTransform.localPosition = gridLayoutGroup.transform.localPosition;
        }
        #endregion
    }
}