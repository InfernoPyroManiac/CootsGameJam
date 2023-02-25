using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CatCharacter : MonoBehaviour
{
   
    public float Yspeed;    
    private Vector2 laneCentre;
    public int CurrentLane = 1;
    public int DesiredLane = 1;
    public float[] Lanes; 



    // Start is called before the first frame update
    void Start()
    {
        laneCentre = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {



        transform.position = Vector2.MoveTowards(transform.position, laneCentre, Yspeed * Time.deltaTime); //Move Towards Desired Lane

        if (CurrentLane != Lanes.Length - 1)
        {
            // If Up Arrow while below Upmost lane And within half the distance of a Desired lane
            if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && DesiredLane >= CurrentLane)
            {
                // then increase the desired lane
                laneCentre = new Vector2(transform.position.x, Lanes[CurrentLane + 1]);
                DesiredLane = CurrentLane + 1;

            }
            if (Mathf.Abs(transform.position.y - Lanes[CurrentLane]) > Mathf.Abs(transform.position.y - Lanes[CurrentLane + 1])) { CurrentLane++; }

        }

        if (CurrentLane != 0)
        {
            // If Down Arrow while above Downmost lane And within half the distance of a Desired lane 
            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && DesiredLane <= CurrentLane)
            {
                // then decrease the desired lane
                laneCentre = new Vector2(transform.position.x, Lanes[CurrentLane - 1]);
                DesiredLane = CurrentLane - 1;
            }
            if (Mathf.Abs(transform.position.y - Lanes[CurrentLane]) > Mathf.Abs(transform.position.y - Lanes[CurrentLane - 1])) { CurrentLane--; }
        }

        // Update Layer
        GetComponent<SortingGroup>().sortingLayerName = "lane " + CurrentLane;
        SetLayerAllChildren(transform, CurrentLane + 20);
    }
    // Sets the interaction layer
    void SetLayerAllChildren(Transform root, int layer)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            child.gameObject.layer = layer;
        }
    }

}
