using UnityEngine;

class FollowRobot : MonoBehaviour
{
    [SerializeField] float yOffset;
    [SerializeField] float followDistance;
    [SerializeField] float flySpeed;
    [SerializeField] float effMoveSpeed;
    [SerializeField] float effSpeed;

    [SerializeField] Transform key1Pos;
    [SerializeField] Transform key2Pos;

    [HideInInspector] public GameObject key1 = null;
    [HideInInspector] public GameObject key2 = null;

    GameObject main;
    CameraFollow cam;

    float effTime = 0f;

    private void Awake()
    {
        main = GameObject.FindGameObjectWithTag("Phantom");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    public bool HasKey()
    {
        return (key1 != null || key2 != null);
    }
    public void Addkey(GameObject key)
    {
        if(key1 == null)
        {
            key1 = key;
            key.GetComponent<Key>().SetTarget(key1Pos);
        }
        else if(key2 == null)
        {
            key2 = key;
            key.GetComponent<Key>().SetTarget(key2Pos);
        }
    }
    public void RemoveKey()
    {
        if(key2 != null)
        {
            key2.GetComponent<Key>().GoSleep();
            key2 = null;
        }
        else
        {
            key1.GetComponent<Key>().GoSleep();
            key1 = null;
        }
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