using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FlowerSound : MonoBehaviour
{
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private float timeCheck;
    private AudioSource _audio;
    private float timeRecord;

    private bool canSing = true;
    // Start is called before the first frame update
    void Start()
    {
        _audio = GetComponent<AudioSource>();
        if (Physics2D.OverlapCircle(transform.position, 1, detectLayer))
        {
            Collider2D ground = Physics2D.OverlapCircle(transform.position, 1, detectLayer );
            //Debug.Log(ground.name);
            if (ground.name.Contains("1"))
            {
                _audio.pitch = 1f;
            }
            else if (ground.name.Contains("2"))
            {
                _audio.pitch = 1.2f;
            }
            ground.GetComponent<SingleLand>().plantedOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
        float distance = Vector3.Distance(transform.position, mouseWorldPosition);
        if ((distance < 0.2f) && canSing)
        {
            _audio.Play();
            StartCoroutine(Shrink());
            canSing = false;
            timeRecord = Time.time;
            
        }

        if (!canSing && (Time.time-timeRecord>timeCheck))
        {
            canSing = true;
        }
    }

    IEnumerator Shrink()
    {
        transform.DOScale(transform.localScale * 0.6f, 0.3f);
        yield return new WaitForSeconds(0.4f);
        transform.DOScale(transform.localScale / 0.6f, 0.4f);
    }
    
}
