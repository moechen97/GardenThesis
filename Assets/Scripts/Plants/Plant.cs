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
            aliveTime -= Time.deltaTime;
            if (aliveTime <= 0.0F)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    private void CheckForBreeding()
    {
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
            if (collision.transform.parent.GetComponent<Plant>().isBreeding)
            {
                Vector3 midpoint = (transform.position + collision.transform.position) / 2F;
                Debug.Log("BREEDING");
                
                Ray ray = new Ray(midpoint, Vector3.right);
                RaycastHit hit;
                Vector3 direction = Vector3.right;
                float distance = 0.15F;
                if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Plant")))
                {
                    direction = Vector3.left;
                    ray = new Ray(midpoint, direction);
                    if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Plant")))
                    {
                        direction = Vector3.down;
                        ray = new Ray(midpoint, direction);
                        if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Plant")))
                        {
                            direction = Vector3.up;
                            ray = new Ray(midpoint, direction);
                            if (Physics.Raycast(ray, out hit, distance, LayerMask.GetMask("Plant")))
                            {
                                continue;
                            }
                            else
                            {
                                GameObject newPlant = GameObject.Instantiate(plantPrefab);
                                newPlant.transform.position += Vector3.up * distance;
                                collision.transform.parent.GetComponent<Plant>().isBreeding = false;
                                break;
                            }
                        }
                        else
                        {
                            GameObject newPlant = GameObject.Instantiate(plantPrefab);
                            newPlant.transform.position += Vector3.down * distance;
                            collision.transform.parent.GetComponent<Plant>().isBreeding = false;
                            break;
                        }
                    }
                    else
                    {
                        GameObject newPlant = GameObject.Instantiate(plantPrefab);
                        newPlant.transform.position += Vector3.left * distance;
                        collision.transform.parent.GetComponent<Plant>().isBreeding = false;
                        break;
                    }
                }
                else
                {
                    GameObject newPlant = GameObject.Instantiate(plantPrefab);
                    newPlant.transform.position += Vector3.right * distance;
                    collision.transform.parent.GetComponent<Plant>().isBreeding = false;
                    break;
                }
            }
        }
    }
}
