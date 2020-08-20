using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2 : MonoBehaviour
{
    public GameObject door;
    public GameObject lift;

    public void OpenDoor()
    {

        door.AddComponent<MovingPlatform>();
        GameObject doorNewState = new GameObject();

        Vector3 tmpPos = doorNewState.transform.position;
        tmpPos = door.transform.position;
        tmpPos.y += 10;
        doorNewState.transform.position = tmpPos;

        GameObject[] tmpTargets = { doorNewState };
        door.GetComponent<MovingPlatform>().targets = tmpTargets;
        door.GetComponent<MovingPlatform>().movingTime = 10;
        door.GetComponent<MovingPlatform>().doOnce = true;
    }

    public void ActiveLift()
    {
        //MovingPlatform.lift.move = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
