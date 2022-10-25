using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    [CreateAssetMenu(fileName = "New Dictionary Storage", menuName = "Data Objects/Plant Name Dictionary Storage Object")]
    public class PlantNameDictionaryScriptaleObject : ScriptableObject
    {
        [SerializeField]
        List<PlantType> keys = new List<PlantType>();
        [SerializeField]
        List<string> values = new List<string>();

        public List<PlantType> Keys { get => keys; set => keys = value; }
        public List<string> Values { get => values; set => values = value; }
    }
}