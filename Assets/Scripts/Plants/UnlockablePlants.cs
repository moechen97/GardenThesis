using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting {

    public class UnlockablePlants : MonoBehaviour
    {
        private Dictionary<PlantType, GameObject> unlockable_icons;
        [SerializeField] GameObject seedPanel;

        private int indexcount = 0;
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
                        indexcount++;
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Fungus_Purple.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.SetSiblingIndex(indexcount);
                       
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        
                        fungusPurpleIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Fungus_Purple);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Peach)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 2 && 
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 1)
                    {
                        indexcount++;
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Peach.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.SetSiblingIndex(indexcount);
                        
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        
                        fungusPurpleIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Plant_Peach);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Drum)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Plant_Peach] >= 3 && 
                       PlantManager.bredPlantCounter[PlantType.Plant_Peach] >= 1)
                    {
                        indexcount++;
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Drum.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.SetSiblingIndex(indexcount);
                        
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        
                        fungusPurpleIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Plant_Drum);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Spike)
                {
                    if (PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 1 &&
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 0)
                    {
                        indexcount++;
                        GameObject spikeIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        spikeIcon.name = PlantType.Plant_Spike.ToString();
                        spikeIcon.transform.parent = seedPanel.transform;
                        spikeIcon.transform.SetSiblingIndex(indexcount);
                        
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        
                        spikeIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Plant_Spike);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Bubble)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Fungus_Green] >= 5 && 
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 4)
                    {
                        indexcount++;
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Bubble.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.SetSiblingIndex(indexcount);
                        
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        
                        fungusPurpleIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Plant_Bubble);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Capture)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Plant_Bubble] >= 5 && 
                       PlantManager.bredPlantCounter[PlantType.Plant_Bubble] >= 1)
                    {
                        indexcount++;
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Capture.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.SetSiblingIndex(indexcount);
                        
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        
                        fungusPurpleIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Plant_Capture);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Rings)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Plant_Peach] >= 5 && 
                       PlantManager.bredPlantCounter[PlantType.Plant_Peach] >= 2)
                    {
                        indexcount++;
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Rings.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.SetSiblingIndex(indexcount);
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        fungusPurpleIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Plant_Rings);
                    }
                }
                if(unlockable.Key == PlantType.Plant_Lotus)
                {
                    if(PlantManager.plantedPlantCounter[PlantType.Plant_Drum] >= 4 && 
                       PlantManager.bredPlantCounter[PlantType.Fungus_Green] >= 0)
                    {
                        indexcount++;
                        GameObject fungusPurpleIcon = GameObject.Instantiate(unlockable_icons[unlockable.Key]);
                        fungusPurpleIcon.name = PlantType.Plant_Lotus.ToString();
                        fungusPurpleIcon.transform.parent = seedPanel.transform;
                        fungusPurpleIcon.transform.SetSiblingIndex(indexcount);
                        //delete last child
                        int a = seedPanel.transform.childCount - 1;
                        Object.Destroy(seedPanel.transform.GetChild(a).gameObject);
                        fungusPurpleIcon.transform.localScale = new Vector3(1F, 1F, 1F);
                        unlocks.Add(PlantType.Plant_Lotus);
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
