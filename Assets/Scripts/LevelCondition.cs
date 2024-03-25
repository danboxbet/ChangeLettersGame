
using UnityEngine;
using TestTask.Collector;

namespace TestTask
{
    public class LevelCondition : MonoBehaviour
    {
        [SerializeField] private CellSpawner cellSpawner;
        [SerializeField] private CellCollector cellCollector;

        [SerializeField] private int maxLevel;
        [SerializeField] private GameObject resetPanel;

        private int currentLevel = 1;
        public int CurrentLevel => currentLevel;

        #region Unity_Events
        private void Awake()
        {
            resetPanel.SetActive(false);

            cellSpawner.SpawnAllCellForLevel(currentLevel);

            cellCollector.AssignRandomlyCorrectAnswer();

            SubscribeOnCellsEnter();
        }
        private void OnDestroy()
        {
            UnsubscribeOnCellsEnter();
        }
        #endregion

        #region Private
        private void CheckVictory()
        {
            if (currentLevel == maxLevel) resetPanel.SetActive(true);
            else
            {
                currentLevel++;
                cellSpawner.DeleteAllCells();
                UnsubscribeOnCellsEnter();

                cellSpawner.SpawnAllCellForLevel(currentLevel);
                cellCollector.AssignRandomlyCorrectAnswer();
                SubscribeOnCellsEnter();
            }
        }
        private void SubscribeOnCellsEnter()
        {
            foreach (var cell in cellSpawner.CellsOnScene)
            {
                cell.EnterCorrectAnswer += CheckVictory;
            }
        }
        private void UnsubscribeOnCellsEnter()
        {
            foreach (var cell in cellSpawner.CellsOnScene)
            {
                cell.EnterCorrectAnswer -= CheckVictory;
            }
        }
        #endregion
    }
}