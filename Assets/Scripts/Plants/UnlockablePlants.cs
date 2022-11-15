using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting {

    public class UnlockablePlants : MonoBehaviour
    {
        private Dictionary<PlantType, GameObject> unlockable_icons;
        [SerializeField] GameObject seedPanel;
        // Start is called before the first frame update
        void Start()
        { 
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
                        fungusPurpleIcon.name = PlantType.Fungus_Purple.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.localScale = new Vector3(1.440003F, 1.440003F, 1.440003F);
                        unlocks.Add(PlantType.Fungus_Purple);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Peach)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 2 && 
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 1)
                    {
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Peach.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.localScale = new Vector3(1.440003F, 1.440003F, 1.440003F);
                        unlocks.Add(PlantType.Plant_Peach);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Drum)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Plant_Peach] >= 3 && 
                       PlantManager.bredPlantCounter[PlantType.Plant_Peach] >= 1)
                    {
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Drum.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.localScale = new Vector3(1.440003F, 1.440003F, 1.440003F);
                        unlocks.Add(PlantType.Plant_Drum);
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
