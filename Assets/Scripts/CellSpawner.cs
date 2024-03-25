using System;
using TestTask.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class CellSpawner : MonoBehaviour
    {
        public event Action<int> Spawn;
        public event Action ChangeSizeCollection;

        [Header("Spawn settings")]
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private Transform parentPanel;
        [SerializeField] private CardBundleData[] cardBundleData;

        private CardBundleData currentData;

        private List<Cell> cellsOnScene = new List<Cell>();

        public CardBundleData CardBundleData => currentData;
        public List<Cell> CellsOnScene => cellsOnScene;

        #region Public
        public void DeleteAllCells()
        {
            int childCount = parentPanel.childCount;

            for (int i = 0; i < childCount; i++)
            {
                GameObject child = parentPanel.GetChild(i).gameObject;
                Destroy(child);
            }
        }
        public void SpawnAllCellForLevel(int level)
        {
            cellsOnScene.Clear();

            currentData = cardBundleData[UnityEngine.Random.Range(0, cardBundleData.Length)];

            Spawn?.Invoke(currentData.ConstraintCount);

            SpawnCell(level * currentData.ConstraintCount);
        }
        #endregion

        #region Private
        private void SpawnCell(int nums)
        {
            List<int> indexes = new List<int>();

            for (int i = 0; i < nums; i++)
            {
                Cell cell = Instantiate(cellPrefab);
                cellsOnScene.Add(cell);

                int randomIndex = GetRandomIndexData(indexes);
                indexes.Add(randomIndex);

                cell.SetSprite(currentData.CardDatas[randomIndex].Sprite, currentData.CardDatas[randomIndex].Rotation);
                cell.SetSymbol(currentData.CardDatas[randomIndex].Identity);

                cell.transform.SetParent(parentPanel);
            }

            ChangeSizeCollection?.Invoke();
        }

        private int GetRandomIndexData(List<int> arrayIndexes)
        {
            int randomIndex = UnityEngine.Random.Range(0, currentData.CardDatas.Length);

            foreach (var index in arrayIndexes)
            {
                if (randomIndex == index) return GetRandomIndexData(arrayIndexes);
            }

            return randomIndex;
        }
        #endregion
    }
}