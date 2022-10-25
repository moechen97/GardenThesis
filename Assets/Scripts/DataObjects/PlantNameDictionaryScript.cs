using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class PlantNameDictionaryScript : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField]
        public PlantNameDictionaryScriptaleObject dictionaryData;
        [SerializeField]
        private List<PlantType> keys = new List<PlantType>();
        [SerializeField]
        private List<string> values = new List<string>();

        [HideInInspector] public Dictionary<PlantType, string> myDictionary = new Dictionary<PlantType, string>();

        public bool modifyValues;

        public void OnBeforeSerialize()
        {
            if (modifyValues == false)
            {
                keys.Clear();
                values.Clear();
                for (int i = 0; i < Mathf.Min(dictionaryData.Keys.Count, dictionaryData.Values.Count); i++)
                {
                    keys.Add(dictionaryData.Keys[i]);
                    values.Add(dictionaryData.Values[i]);
                }
            }
        }

        public void OnAfterDeserialize()
        {

        }

        public Dictionary<PlantType, string> DeserializeDictionary()
        {
            myDictionary = new Dictionary<PlantType, string>();
            dictionaryData.Keys.Clear();
            dictionaryData.Values.Clear();
            for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
            {
                dictionaryData.Keys.Add(keys[i]);
                dictionaryData.Values.Add(values[i]);
                myDictionary.Add(keys[i], values[i]);
            }
            modifyValues = false;
            return myDictionary;
        }

        public void PrintDictionary()
        {
            foreach (var pair in myDictionary)
            {
                Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
            }
        }
    }
}
