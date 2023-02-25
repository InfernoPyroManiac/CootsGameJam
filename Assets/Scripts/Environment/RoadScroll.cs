using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScroll : MonoBehaviour
{
    
    public float repeatStart;
    public float repeatStop;
    public float PassSpeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * PassSpeed * Time.deltaTime);

        if (repeatStop >= transform.position.x)
        {
            transform.position = new Vector2(repeatStart, transform.position.y);
        }

    }
}
