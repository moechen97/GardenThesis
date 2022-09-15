using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [HideInInspector] public string id;
    protected string finishGrowAnimationName;
    private Animator animator;
    private bool isGrown;
    [SerializeField] GameObject plantPrefab;
    [SerializeField] private float growthSpeed_lowRange;
    [SerializeField] private float growthSpeed_highRange;
    [SerializeField] private float aliveTime = 20F;
    [SerializeField] GameObject flower;
    [HideInInspector] public bool isBreeding = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        isGrown = false;
        animator = transform.GetComponentInChildren<Animator>();
        //float speed = Random.Range(0.005F, 0.100F);
        float speed = Random.Range(growthSpeed_lowRange, growthSpeed_highRange);
        animator.speed = speed;
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isGrown && animator.GetCurrentAnimatorStateInfo(0).IsName(finishGrowAnimationName))
        {
            Debug.Log("GROWN");
            isGrown = true;
            isBreeding = true;
            animator.speed = 1.00F;
            flower.SetActive(true);
            CheckForBreeding();
        }
        else if (isGrown)
        {
            if(Mathf.Approximately(aliveTime % 2, 0F))
            {
                CheckForBreeding();
            }
            aliveTime -= Time.deltaTime;
            if (aliveTime <= 0.0F)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void CheckForBreeding()
    {
        if(!isBreeding || !GrowthManager.CanBreedPlant(id))
        {
            return;
        }
        Collider[] hits = Physics.OverlapSphere(transform.position, 0.1F, LayerMask.GetMask("Plant"));
        Debug.Log("HITS COUNT: " + hits.Length);
        foreach (Collider collision in hits)
        {
            if (collision.transform == transform)
            {
                continue;
            }
            if(!collision.transform.parent.GetComponent<Plant>().id.Equals(id))
            {
                continue;
            }
            if(!collision.transform.parent.GetComponent<Plant>().isGrown)
            {
                continue;
            }
            if (collision.transform.parent.GetComponent<Plant>().isBreeding)
            {
                Vector3 midpoint = (transform.position + collision.transform.position) / 2F;
                List<Vector3> directionList = new List<Vector3> { Vector3.left, Vector3.right, new Vector3(0F, 0F, 1F), new Vector3(0F, 0F, -1F),
                    Vector3.Normalize(new Vector3(1F, 0F, 1F)), Vector3.Normalize(new Vector3(-1F, 0F, 1F)), 
                    Vector3.Normalize(new Vector3(-1F, 0F, -1F)), Vector3.Normalize(new Vector3(1F, 0F, -1F)) };
                directionList = Shuffle(directionList);
                RaycastHit hit;
                foreach (Vector3 direction in directionList)
                {
                    float distance = 0.25F;
                    Ray groundRay = new Ray(midpoint + (direction * distance), Vector3.down);
                    if (!Physics.Raycast(groundRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                    {
                        continue;
                    }
                    Ray ray = new Ray(midpoint, direction);
                    if (!Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Plant")))
                    {
                        if (GrowthManager.SpawnPlantBreed(id))
                        {
                            GameObject newPlant = GameObject.Instantiate(plantPrefab);
                            newPlant.transform.position += direction * distance;
                            collision.transform.parent.GetComponent<Plant>().isBreeding = false;
                            isBreeding = false;
                            break;
                        }
                    }
                }
            }
        }
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
        GrowthManager.DecrementPlant(id);
    }
}
