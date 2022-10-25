using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting {

    public class UnlockablePlants : MonoBehaviour
    {
        private Dictionary<PlantType, GameObject> unlockable_icons;
        private Dictionary<PlantType, string> plantNames;
        [SerializeField] GameObject seedPanel;
        // Start is called before the first frame update
        void Start()
        {
            plantNames = GetComponent<PlantNameDictionaryScript>().DeserializeDictionary();
            unlockable_icons = GetComponent<UnlockableIconDictionaryScript>().DeserializeDictionary();
        }

        // Update is called once per frame
        void Update()
        {


        }

        public void Unlock_Progress()
        {
            List<PlantType> unlocks = new List<PlantType>();
            foreach(KeyValuePair<PlantType, GameObject> unlockable in unlockable_icons)
            {
                if(unlockable.Key == PlantType.Fungus_Purple)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 3 && 
                        PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 4)
                    {
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = plantNames[unlockable.Key];
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.localScale = new Vector3(1.440003F, 1.440003F, 1.440003F);
                        unlocks.Add(PlantType.Fungus_Purple);
                    }
                }
            }
            foreach(PlantType type in unlocks)
            {
                unlockable_icons.Remove(type);
            }
        }
    }
}
