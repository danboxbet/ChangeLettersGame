
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    public class UICell : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private RectTransform rectTransform;

        private Sprite sprite;

        #region Public
        public void ChangeUI(Sprite sprite, Vector3 rotation)
        {
            this.sprite = sprite;
            rectTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            ChangeImage();
        }
        #endregion

        #region Private
        private void ChangeImage()
        {
            image.sprite = sprite;
        }
        #endregion
    }
}