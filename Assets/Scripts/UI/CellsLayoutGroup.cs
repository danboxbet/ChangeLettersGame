
using UnityEngine;
using UnityEngine.UI;

namespace TestTask.UI
{
    public class CellsLayoutGroup : MonoBehaviour
    {
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        [SerializeField] private CellSpawner cellSpawner;

        #region Unity_Events
        private void Start()
        {
            cellSpawner.Spawn += ChangeConstraintCount;
            gridLayoutGroup.constraintCount = cellSpawner.CardBundleData.ConstraintCount;
        }
        #endregion

        #region Private
        private void ChangeConstraintCount(int count)
        {
            gridLayoutGroup.constraintCount = count;
        }
        #endregion
    }
}