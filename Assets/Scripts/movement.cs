using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class movement : MonoBehaviour
{
	public bool isParalyzed;
	public CharacterController2D controller;
	private GameObject main;

	public float runSpeed = 40f;

	float horizontalMove = 40f;
	[HideInInspector] bool jump = false;
	[HideInInspector] bool act = false;

	[HideInInspector] public bool jumpInput = true;
	[HideInInspector] public bool actInput = false;

	[HideInInspector] float horizontalInput;

	void Start()
	{
		main = GameObject.FindGameObjectWithTag("main");
	}

	public void SetHorizontalInput(float newInput)
	{
		horizontalInput = newInput;
	}
	public void JumpInput()
	{
		jump = true;
	}
	public void ActInput()
	{
		act = true;
	}


	void Update()
	{
		horizontalMove = (horizontalInput + Input.GetAxisRaw("Horizontal")) * runSpeed;

		if (Input.GetButtonDown("Jump") || jumpInput)
		{
			jump = true;
		}
		if (Input.GetButtonDown("Act"))
		{
			act = true;
		}
		if (act && !isParalyzed)
		{
			
			GameObject[] npcs;
			npcs = GameObject.FindGameObjectsWithTag("can_dialog");

			float minDistance = 0;
			bool findNPC = false;
			GameObject dialogNPC = null;
			foreach (GameObject i in npcs)
			{
				Debug.Log("act");
				float distance = (main.transform.position - i.transform.position).magnitude;

				if(distance <= 5)
				{
					Debug.Log("act1");
					if (findNPC == false)
					{
						minDistance = distance;
						dialogNPC = i;
					}
					else
					{
						if(distance < minDistance)
						{
							minDistance = distance;
							dialogNPC = i;
						}
					}
					findNPC = true;
				}
			}
			if (findNPC)
			{
				dialogNPC.GetComponent<dialogNPC>().tryDialog();
				CameraFollow cam = ((GameObject)GameObject.FindGameObjectsWithTag("MainCamera").GetValue(0)).GetComponent<CameraFollow>();
				Vector3 camPos = Vector3.Lerp(dialogNPC.transform.position, gameObject.transform.position, .5f);
				camPos.y -= 3;
				cam.forcePosDialog = camPos;
				cam.doForcePosDialog = true;
			}
			else
			{
				GetComponent<CharacterController2D>().Dash();
			}
			
		}

	}

	void FixedUpdate()
	{
		// Move our character
		//controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		float move = horizontalMove;
		if (isParalyzed)
		{
			((CharacterController2D)controller).Move(0f * Time.fixedDeltaTime, false, false);
		}
		else
		{
			((CharacterController2D)controller).Move(move * Time.fixedDeltaTime, false, jump);
		}
		jump = false;
		act = false;
	}
}
