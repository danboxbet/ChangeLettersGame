
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Collector
{
    public class CellCollector : MonoBehaviour
    {
        public event Action<Cell> AssignedCorrectSymbol;

        [SerializeField] private CellSpawner cellSpawner;

        private string currentSymbols;
        private int rightIndex;
        public int RightIndex => rightIndex;


        #region Unity_Events
        private void Start()
        {
            AssignedCorrectSymbol?.Invoke(cellSpawner.CellsOnScene[rightIndex]);
        }
        #endregion

        #region Public
        public bool CheckAnswerIsCorrect(Cell cell)
        {
            for (int i = 0; i < cellSpawner.CellsOnScene.Count; i++)
            {
                if (cellSpawner.CellsOnScene[i] == cell)
                {
                    if (i == rightIndex) return true;
                    else return false;
                }
            }

            return false;
        }
        public void AssignRandomlyCorrectAnswer()
        {
            rightIndex = GetRandomIndex();
            currentSymbols = cellSpawner.CellsOnScene[rightIndex].MySymbols;
            AssignedCorrectSymbol?.Invoke(cellSpawner.CellsOnScene[rightIndex]);
        }
        #endregion

        #region Private
        private int GetRandomIndex()
        {
            int index = UnityEngine.Random.Range(0, cellSpawner.CellsOnScene.Count);

            if (cellSpawner.CellsOnScene[index].MySymbols == currentSymbols)
            {
                return GetRandomIndex();
            }

            return index;
        }
        #endregion
    }
}