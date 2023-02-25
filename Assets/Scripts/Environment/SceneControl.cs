using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{

    // All Obstacles set to spawn
    public GameObject[] CarList;

    // CarType, LaneIndex, Speed, TimeAfter
    public int[] CarType; 
    public int[] LaneIndex;
    public float[] Speed;
    public float[] TimeAfter;


    private float timer;
    private int indexTracker = -1;

    private float currentMultiplier;
    private float currentDuration;

    private float noOverwriteMultiplier;
    private float noOverwriteDuration;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /////////////////////

        //Decrease both timers
        if (currentDuration > 0) { currentDuration -= Time.deltaTime; }
        if (noOverwriteDuration > 0) { noOverwriteDuration -= Time.deltaTime; }

        // if duration is up and there is no priority overlap
        if (currentDuration<=0 && noOverwriteMultiplier<=0)
        {
            //remove multiplier
            SetSpeedAllChildren(transform, 1 / (currentMultiplier+1));
            currentMultiplier = 0;
        }
        // if priority duration is up
        if (noOverwriteDuration<=0)
        {
            //remove priority multiplier
            SetSpeedAllChildren(transform, 1/(noOverwriteMultiplier+1));
            noOverwriteMultiplier = 0;
        }
   


        /////////////////////

        timer -= Time.deltaTime;

        if (timer < 0 && indexTracker+1 < CarType.Length)
        {
            indexTracker++;
            GameObject newVehicle = Instantiate(CarList[CarType[indexTracker]], new Vector2(15, -4+(1.5f * LaneIndex[indexTracker])), Quaternion.identity, transform);

            if (newVehicle.CompareTag("Vehicle"))
            {
                
                newVehicle.GetComponent<GenericCar>().PassSpeed = Speed[indexTracker];
                if (noOverwriteMultiplier > 0) { newVehicle.GetComponent<GenericCar>().PassSpeed *= noOverwriteMultiplier; }
                else if (currentMultiplier > 0) { newVehicle.GetComponent<GenericCar>().PassSpeed *= currentMultiplier +1; }
                newVehicle.GetComponent<GenericCar>().currentLane = LaneIndex[indexTracker];
                newVehicle.GetComponent<GenericCar>().LaneSwitch();
                timer = TimeAfter[indexTracker];
            }
            else if (newVehicle.CompareTag("credito"))
            {

                newVehicle.GetComponent<GenericLorry>().currentLane = LaneIndex[indexTracker];
                newVehicle.GetComponent<GenericLorry>().LaneSwitch();
                timer = TimeAfter[indexTracker];
            }
            else if (newVehicle.CompareTag("ludboss"))
            {

                newVehicle.GetComponent<GenericMogul>().currentLane = LaneIndex[indexTracker];
                newVehicle.GetComponent<GenericMogul>().LaneSwitch();
                timer = TimeAfter[indexTracker];
            }
        }
        //////////////////////
    }
    
    public void SpeedBoost(float Multiplier, float Duration)
    {
        // if new multiplier is more than or equal to old (and no current priority)
        if (Multiplier >= currentMultiplier && noOverwriteDuration <= 0)
        {

            // If new multiplier is equal to old
            if (Multiplier == currentMultiplier && currentDuration < Duration)
            {
                // Restart duration
                currentDuration = Duration;

            }
            else // if new multiplier is more than old
            {
                // remove old multiplier
                SetSpeedAllChildren(transform, 1 / (currentMultiplier+1));
                // set new multiplier
                currentMultiplier = Multiplier;
                SetSpeedAllChildren(transform, (currentMultiplier+1));
                // set new duration
                currentDuration = Duration;
            }
        }
    }
    public void SpeedBoostPriority(float Multiplier, float Duration)
    {

        // if new multiplier is more than or equal to old priority
        if (Multiplier >= noOverwriteMultiplier)
        {

            // If new multiplier is equal to old
            if (Multiplier == noOverwriteMultiplier && noOverwriteDuration < Duration)
            {
                // Restart duration
                noOverwriteDuration = Duration;

            }
            else // if new multiplier is more than old
            {
                // remove old multiplier
                SetSpeedAllChildren(transform, 1 / (currentMultiplier+1));
                // set new multiplier
                noOverwriteMultiplier = Multiplier;
                currentMultiplier = 0;
                SetSpeedAllChildren(transform, (noOverwriteMultiplier+1));
                // set new duration
                currentDuration = 0;
                noOverwriteDuration = Duration;

            }


        }



    }


    void SetSpeedAllChildren(Transform root, float speed)
    {
        var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
        foreach (var child in children)
        {
            if (child.gameObject) // prevents from being null I think???
            {

                if (child.gameObject.CompareTag("Vehicle"))
                {
                    child.gameObject.GetComponent<GenericCar>().PassSpeed *= speed;
                }
                else if (child.gameObject.CompareTag("Parallaxer"))
                {
                    child.gameObject.GetComponent<RoadScroll>().PassSpeed *= speed;
                }
                else if (child.gameObject.CompareTag("ludboss"))
                {
                    child.gameObject.GetComponent<GenericMogul>().PassSpeed *= speed;
                }

            }

        }
        
    }
}
