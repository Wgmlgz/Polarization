using UnityEngine;

class FollowRobot : MonoBehaviour
{
    [SerializeField] float yOffset;
    [SerializeField] float followDistance;
    [SerializeField] float flySpeed;
    [SerializeField] float effMoveSpeed;
    [SerializeField] float effSpeed;

    GameObject main;
    CameraFollow cam;

    float effTime = 0f;

    private void Awake()
    {
        main = GameObject.FindGameObjectWithTag("Phantom");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    private void Update()
    {
        //var
        Vector3 targetPos = main.transform.position;
        Vector3 curPos = transform.position;
        Vector3 newPos = curPos;
        float targetDistance = (targetPos - curPos).magnitude;


        //calc
        targetPos.y += yOffset;

        if(targetDistance > followDistance)
        {            
            targetPos = Vector3.Lerp(curPos, targetPos, (targetDistance - followDistance)/ targetDistance);
            newPos = Vector3.Lerp(curPos, targetPos, flySpeed * Time.deltaTime);
        }

        //flip
        transform.localScale = new Vector3((newPos.x > main.transform.position.x)?-1f : 1f,1f,1f);

        //eff
        effTime += Time.deltaTime;
        if(effTime >= effSpeed * 2f)
        {
            effTime = 0f;
        }
        if(effTime < effSpeed)
        {
            newPos.y += effMoveSpeed * Time.deltaTime;
        }
        else
        {
            newPos.y -= effMoveSpeed * Time.deltaTime;
        }

        //end
        transform.position = newPos;
    }
}