using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planting
{
    public class Spike : Plant
    {
        [HideInInspector] public int generation = 1;
        private int nextGeneration;
        [field: SerializeField] public float breedAppearTimer = 0.3F;
        private Coroutine circleSpread = null;
        private Dictionary<int, int> generationSpread = new Dictionary<int, int>() { {2, 5}, {3, 8}, {4, 12}, {5, 16} };
        protected override void Start()
        {
            id = PlantType.Plant_Spike;
            finishGrowAnimationName = "Spike_Tem_FullyGrow";
            base.Start();
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
            yield return new WaitForSeconds(breedAppearTimer);
            Debug.Log("NEXT GENERATION : " + nextGeneration);
            int numSpikes = generationSpread[nextGeneration];
            Debug.Log("NUM SPIKES: " + numSpikes);
            float radius = growthRadius * (nextGeneration - 1);
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
                Debug.Log("SPAWN SPIKE");
                yield return new WaitForSeconds(breedAppearTimer);
            }
            nextGeneration++;
            circleSpread = null;
        }
    }
}