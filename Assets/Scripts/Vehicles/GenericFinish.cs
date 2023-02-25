using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFinish : MonoBehaviour
{
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) { transform.parent.localScale = Vector2.Scale(transform.parent.localScale, new Vector2(transform.parent.localScale.x, transform.parent.localScale.y* 0.9f*Time.deltaTime)); }
    }

    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.CompareTag("CatCollider"))
        {
            triggered = true;
            //win coots :)
            Player.GetComponent<CatCollider>().FinishLine();
        }
    }
}
