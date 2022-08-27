using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class TouchPoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _sprites;
    private bool canFollow = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canFollow)
        {
            FollowMouse();
        }
        
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,10));
        transform.position = mousePosition;
    }

    public void DeleteItself()
    {
        foreach (var image in _sprites)
        {
            image.DOColor(new Color(1, 1, 1, 0), 0.2f);
        }

        canFollow = false;
        Destroy(this.gameObject,0.1f);
    }
}
