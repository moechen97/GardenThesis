using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom_DarkGreen : MonoBehaviour
{
    private Animator animator;
    private bool isGrown;
    private float aliveTime = 20F;
    [SerializeField] GameObject flower;
    // Start is called before the first frame update
    void Start()
    {
        isGrown = false;
        animator = transform.GetComponentInChildren<Animator>();
        float speed = Random.Range(0.005F, 0.100F);
        Debug.Log("SPEED: " + speed);
        animator.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGrown && animator.GetCurrentAnimatorStateInfo(0).IsName("Fungus_Stem_Darkgreen_FinishGrow"))
        {
            Debug.Log("GROWN");
            isGrown = true;
            animator.speed = 1.00F;
            flower.SetActive(true);
        }
        else if(isGrown)
        {
            aliveTime -= Time.deltaTime;
            if(aliveTime <= 0.0F)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
