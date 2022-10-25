using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    [CreateAssetMenu(fileName = "New Dictionary Storage", menuName = "Data Objects/Unlockable Icon Dictionary Storage Object")]
    public class UnlockableIconDictionaryScriptaleObject : ScriptableObject
    {
        [SerializeField]
        List<PlantType> keys = new List<PlantType>();
        [SerializeField]
        List<GameObject> values = new List<GameObject>();

        public List<PlantType> Keys { get => keys; set => keys = value; }
        public List<GameObject> Values { get => values; set => values = value; }
    }
}