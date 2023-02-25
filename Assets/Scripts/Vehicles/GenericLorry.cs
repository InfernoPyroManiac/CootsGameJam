using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenericLorry : MonoBehaviour
{
    public float PassSpeed = 0;
    public int currentLane;
    private float timer = 3;
    public GameObject Warning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) { PassSpeed = 8; }
        if (transform.position.x != 15) { Warning.SetActive(false); }

        transform.Translate(PassSpeed * Vector2.left * Time.deltaTime);
        if (transform.position.x < -30)
        {
            Destroy(this.transform.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.CompareTag("CatCollider"))
        {
            //kill coots :(
            Player.GetComponent<CatCollider>().Kill();
        }
    }
    // Sets the interaction layer
    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            if (child.CompareTag("LayerUp")) { child.gameObject.layer = layer+1; }
            else { child.gameObject.layer = layer; }
        }
    }

    public void LaneSwitch()
    {
        GetComponent<SortingGroup>().sortingLayerName = "lane " + currentLane;
        SetLayerAllChildren(transform, currentLane + 20);
    }

}

