using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting {

    public class UnlockablePlants : MonoBehaviour
    {
        public List<PlantType> unlockables;
        public List<GameObject> unlockable_icons;
        [SerializeField] GameObject seedPanel;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


        }

        public void Unlock_Progress()
        {
            List<PlantType> unlocks = new List<PlantType>();
            foreach(PlantType type in unlockables)
            {
                if(type == PlantType.Fungus_Purple)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 3 && 
                        PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 4)
                    {
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[0]);
                        fungusPurpleIcon.name = "Fungus_Purple";
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.localScale = new Vector3(1.440003F, 1.440003F, 1.440003F);
                        unlocks.Add(PlantType.Fungus_Purple);
                    }
                }
            }
            foreach(PlantType type in unlocks)
            {
                unlockables.Remove(type);
            }
        }
    }
}
