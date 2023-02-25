using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GenericMogul : MonoBehaviour
{
    public float PassSpeed = 0.6f;
    public int currentLane;
    private float timer;
    public GameObject target;
    public GameObject ball;
    public AudioSource elloguv;
    public AudioClip replacement;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<CatCharacter>().transform.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!elloguv.isPlaying) { elloguv.clip = replacement; elloguv.loop=true; }
        timer -= Time.deltaTime;
        if (timer <= 0 && PassSpeed < 3)
        { 
            GameObject newBall = Instantiate(ball, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            newBall.GetComponent<GenericCar>().PassSpeed =3;
            newBall.GetComponent<GenericCar>().currentLane = currentLane;
            newBall.GetComponent<GenericCar>().LaneSwitch();
            timer = 2;
        }

        if (transform.position.x - target.transform.position.x <= 12) { PassSpeed = 5; }
        if (target.GetComponent<CatCharacter>()) { currentLane = target.GetComponent<CatCharacter>().CurrentLane; }
        LaneSwitch();
        transform.Translate(PassSpeed * Vector2.left * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, target.transform.position.y), 2 * Time.deltaTime);
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

