using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Planting {
    public class Plant : MonoBehaviour
    {
        [HideInInspector] protected PlantType id;
        protected string finishGrowAnimationName;
        private Animator animator;
        private bool isGrown;
        [SerializeField, Tooltip("Prefab for when breeding plant")] GameObject plantPrefab;
        [SerializeField, Tooltip("Minimum growth speed")] private float growthSpeed_minRange = 0.05F;
        [SerializeField, Tooltip("Maximum growth speed")] private float growthSpeed_maxRange = 0.1F;
        private float speed;
        [SerializeField, Tooltip("Amount of time the plant stays alive after reaching full growth")] private float aliveTime = 20F;
        [SerializeField, Tooltip("Blooming flower when fully grown")] protected GameObject flower;
        [HideInInspector] public bool isBreeding = false;
        [SerializeField, Tooltip("Distance to check/plant for nearby ones of same breed")] private float growthDistance = 2F;
        [HideInInspector] private bool wasBred = false;
        // Start is called before the first frame update
        protected virtual void Start()
        {
            isGrown = false;
            animator = transform.GetComponentInChildren<Animator>();
            //float speed = Random.Range(0.005F, 0.100F);
            speed = Random.Range(growthSpeed_minRange, growthSpeed_maxRange);
            animator.speed = speed * GameObject.FindGameObjectWithTag("SpeedManager").GetComponent<SpeedManager>().speed;
            //Notify growth manager of new plant
            PlantManager.IncrementPlant(id, this);          
            //flower.SetActive(false);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (!isGrown && animator.GetCurrentAnimatorStateInfo(0).IsName(finishGrowAnimationName))
            {
                isGrown = true;
                isBreeding = true;
                GameEvents.current.PlantFullyGrown(id, wasBred);
                animator.speed = 1.00F;
                //flower.SetActive(true);
                //Debug.Log(name+"isGrown");
            }
            else if (isGrown)
            {            
                aliveTime -= Time.deltaTime;
                if (aliveTime <= 0.0F)
                {
                    if (GetComponent<Fungus_MaterialChange>())
                    {
                        transform.GetComponent<Fungus_MaterialChange>().Withered();
                    }
                    else
                    {
                        Destroy(transform.parent.gameObject);
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            if (isGrown && isBreeding)
            {
                if (Random.Range(0, (int)(1F / Time.deltaTime) * 5) == 0)
                {
                    CheckForBreeding();
                }
            }
        }

        private void CheckForBreeding()
        {
            if(Resources.GetResourcesUsed() + PlantManager.resourceDict[id] > 1.0F)
            {
                return;
            }
            if (!isBreeding || !PlantManager.CanBreedPlant(id))
            {
                return;
            }
            Collider[] hits = Physics.OverlapSphere(transform.position, 1F, LayerMask.GetMask("Plant"));
            foreach (Collider collision in hits)
            {
                //if (collision.transform.parent == transform)
                //{
                //    continue;
                //}
                Plant otherPlant = collision.transform.parent.GetComponent<Plant>();
                
                if (otherPlant.id != id)
                {
                    //if plants are not same type, continue
                    continue;
                }
                if (!otherPlant.isGrown)
                {
                    //do not breed plant if not fully grown
                    continue;
                }
                if (otherPlant.isBreeding)
                {
                    Vector3 midpoint = (transform.position + collision.transform.position) / 2F;
                    List<Vector3> directionList = new List<Vector3> { Vector3.left, Vector3.right, new Vector3(0F, 0F, 1F), new Vector3(0F, 0F, -1F),
                    Vector3.Normalize(new Vector3(1F, 0F, 1F)), Vector3.Normalize(new Vector3(-1F, 0F, 1F)),
                    Vector3.Normalize(new Vector3(-1F, 0F, -1F)), Vector3.Normalize(new Vector3(1F, 0F, -1F)) };
                    //Randomize directions to check
                    directionList = Shuffle(directionList);
                    RaycastHit hit;
                    //Check for collision in each direction
                    foreach (Vector3 direction in directionList)
                    {                  
                        Ray groundRay = new Ray(midpoint + (direction * growthDistance), Vector3.down);
                        if (!Physics.Raycast(groundRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                        {
                            continue;
                        }
                        Ray ray = new Ray(midpoint, direction);
                        if (!Physics.Raycast(ray, out hit, growthDistance, LayerMask.GetMask("Plant")))
                        {
                            if (PlantManager.CanSpawnPlantBreed(id))
                            {
                                Breed(otherPlant, direction, growthDistance);
                                break;
                            }
                        }
                    }
                }
            }
        }

        protected void Breed(Plant otherPlant, Vector3 direction, float distance)
        {
            Vector3 newPlantPosition = transform.position + direction * distance;
            GameObject newPlant = GameObject.Instantiate(plantPrefab,newPlantPosition,quaternion.identity);
            newPlant.GetComponent<Plant>().WasBred();
            //newPlant.transform.position += direction * distance;
            otherPlant.isBreeding = false;
            isBreeding = false;
        }

        public List<Vector3> Shuffle(List<Vector3> directionList)
        {
            for (int i = 0; i < directionList.Count; i++)
            {
                Vector3 temp = directionList[i];
                int randomIndex = Random.Range(i, directionList.Count);
                directionList[i] = directionList[randomIndex];
                directionList[randomIndex] = temp;
            }
            return directionList;
        }

        private void OnDestroy()
        {
            //Notify growth manager that plant died
            PlantManager.DecrementPlant(id, this);
        }

        public void UpdateAnimationSpeed(float speedFactor)
        {
            animator.speed = speed * speedFactor;
        }

        public void WasBred()
        {
            wasBred = true;
        }
    }
}
