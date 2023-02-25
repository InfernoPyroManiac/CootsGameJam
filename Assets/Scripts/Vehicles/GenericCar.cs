using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenericCar : MonoBehaviour
{
    public float PassSpeed;
    public int currentLane;
    private int Number;

    // Start is called before the first frame update
    void Start()
    {
        Number = Random.Range(1, 6);

        switch (Number)
        {

            case 1
                :
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 2
                :
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 3
                :
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.905f, 0.768f, 0.513f);
                break;
            case 4
                :
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.713f, 0.062f, 0.756f);
                break;
            case 5
                :
                gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case 6
                :
                
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.113f, 0.145f, 0.227f);
                break;
            case 7
                :
                
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f);
                break;
        }
        if (PassSpeed <= 0) { PassSpeed = 3; }
    }

    // Update is called once per frame
    void Update()
    {
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

