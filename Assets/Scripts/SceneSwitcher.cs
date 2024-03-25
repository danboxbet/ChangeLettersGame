
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace TestTask.Scenes
{
    public class SceneSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject loadPanel;
        [SerializeField] private Image imageResetPanel;

        Tween loadTween;

        #region Unity_Events
        private void OnDestroy()
        {
            loadTween?.Kill();
        }
        #endregion

        #region Public
        public void PrepareToReset()
        {
            loadPanel.SetActive(true);

            Image image = loadPanel.GetComponent<Image>();
            image.color = new Color(imageResetPanel.color.r, imageResetPanel.color.g, imageResetPanel.color.b, imageResetPanel.color.a);

            loadTween?.Kill();
            loadTween = image.DOFade(1, 2).OnComplete(() =>
              {
                  ResetScene();
              }
            );
        }
        #endregion

        #region Private
        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        #endregion
    }
}