
using UnityEngine;

namespace TestTask.ScriptableObjects
{
    [System.Serializable]
    public class CardData
    {
        [SerializeField] private string identity;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Vector3 rotationToSpawn;
        public string Identity => identity;
        public Sprite Sprite => sprite;
        public Vector3 Rotation => rotationToSpawn;
    }

    [CreateAssetMenu]
    public class CardBundleData : ScriptableObject
    {
        [SerializeField] private CardData[] cardDatas;
        [SerializeField] private int constraintCount;
        public CardData[] CardDatas => cardDatas;
        public int ConstraintCount => constraintCount;
    }
}