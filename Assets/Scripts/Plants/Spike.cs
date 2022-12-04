using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class Spike : Plant
    {
        [HideInInspector] public int generation = 1;
        private int nextGeneration;
        private Coroutine circleSpread = null;
        private Dictionary<int, int> generationSpread = new Dictionary<int, int>() { {2, 6}, {3, 10}, {4, 14}, {5, 18}, {6, 22}, {7, 26} };
        [SerializeField] private float breedRadius;
        
        
        protected override void Start()
        {
            id = PlantType.Plant_Spike;
            base.Start();
            finishGrowAnimationName = "Spike_Tem_FullyGrow";
            transform.parent = PlantManager.SpikeTransform;
            nextGeneration = generation + 1;
            

        }

        private void CheckForSpikeDestroy()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 1F, LayerMask.GetMask("Plant"));
            foreach (Collider hit in hits)
            {
                Plant plant = hit.transform.parent.GetComponent<Plant>();
                if (plant.id == PlantType.Plant_Spike)
                {
                    continue;
                }
                GameObject plantObject = hit.transform.parent.gameObject;
                Destroy(plantObject);
            }
        }
        protected override void Update()
        {
            base.Update();
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Spike_Tem_Grow"))
            {
                CheckForSpikeDestroy();
            }
        }

        protected override void FixedUpdate()
        {
            if(generation == 1 && isGrown)
            {
                if(circleSpread == null)
                {
                    circleSpread = StartCoroutine(CircleSpread());
                }
            }
        }
        private IEnumerator CircleSpread()
        {
            yield return new WaitForSeconds(breedingSpeed);
            int numSpikes = generationSpread[nextGeneration];
            float radius = breedRadius * (nextGeneration - 1);
            for(int i = 0; i < numSpikes; i++)
            {
                float angle = i * Mathf.PI * 2 / numSpikes;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                Vector3 pos = transform.position + new Vector3(x, 0F, z);
                Ray groundRay = new Ray(pos + new Vector3(0F, 1F, 0F), Vector3.down);
                RaycastHit hit;
                if (!Physics.Raycast(groundRay, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
                {
                    continue;
                }
                float angleDegrees = angle * Mathf.Rad2Deg;
                Quaternion rot = Quaternion.Euler(0F, angleDegrees, 0F);
                GameObject newSpike = Instantiate(plantPrefab, pos, rot);
                newSpike.GetComponent<Spike>().generation = nextGeneration;
            }
            nextGeneration++;
            yield return new WaitForSeconds(breedingSpeed);
            circleSpread = null;
        }
    }
}