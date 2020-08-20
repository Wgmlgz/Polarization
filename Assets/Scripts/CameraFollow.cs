using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] public bool useDefaults = true;
	[SerializeField] public bool ignoreZ = true;

	[SerializeField] public float smoothSpeed;
	[SerializeField] public Vector3 lookOffset;
	[SerializeField] public float flipOffset;

	[SerializeField] public bool doForceOffset;
	[SerializeField] public Vector3 forceOffset;

	[SerializeField] public bool forceY;

	[SerializeField] public bool doForcePos;
	[SerializeField] public Vector3 forcePos;

	[SerializeField] public bool doForcePosDialog;
	[SerializeField] public Vector3 forcePosDialog;

	[SerializeField] [Range(0f,2f)] public float shakeRange;
	[SerializeField] public bool shake;
	private GameObject target;
	

	private float rootPos;
	public bool lookDirection = true;
	public void ResetPos(bool look)
	{
		Vector3 newPos = target.transform.position;
		if (ignoreZ)
		{
			newPos.z = transform.position.z;
		}
		transform.position = newPos;

		rootPos = target.transform.position.x;
		lookDirection = look;
	}
	void Start()
	{
		rootPos = transform.position.x;
		target = GameObject.FindGameObjectWithTag("main");
		if (useDefaults)
		{
			smoothSpeed = 0.1f;
			lookOffset = new Vector3(5, 3, -25);
			flipOffset = 3;
		}
	}

	void LateUpdate()
	{
		Vector3 newPos = target.transform.position;
		// flip
		if (rootPos < (newPos.x - flipOffset))
		{
			rootPos = newPos.x - flipOffset;
			lookDirection = true;
		}
		else if (rootPos > (newPos.x + flipOffset))
		{
			rootPos = newPos.x + flipOffset;
			lookDirection = false;
		}


		// cam pos calc
		Vector3 tmpLookOffset = lookOffset;

		if(lookDirection == false)
			tmpLookOffset.x *= -1;

		if (doForceOffset)
			tmpLookOffset = forceOffset;

		if (doForcePos)
			tmpLookOffset = forcePos;

		if (doForcePosDialog)
			tmpLookOffset = forcePosDialog;

		Vector3 desiredPosition = tmpLookOffset;

		if (doForcePos == false && doForcePosDialog == false) {
			desiredPosition += newPos;
			if (forceY)
				desiredPosition.y = forcePos.y;
		}

		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

		if (ignoreZ)
			smoothedPosition.z = transform.position.z;

		Vector3 finPos = smoothedPosition;

		if (shake)
		{
			finPos.x += Random.Range(-shakeRange, +shakeRange);
			finPos.y += Random.Range(-shakeRange, +shakeRange);
		}
		transform.position = finPos;
	}

}