using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class movement : MonoBehaviour
{
	public bool isParalyzed;
	public CharacterController2D controller;
	private GameObject main;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool act = false;

	public bool jumpInput = true;
	public bool actInput = false;

	float horizontalInput;

	[SerializeField] GameObject actMenu;

	void Start()
	{
		main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
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
				float distance = (main.transform.position - i.transform.position).magnitude;

				if(distance <= 5)
				{
					if(findNPC == false)
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
			if(findNPC)
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
				Debug.Log("Open");
				actMenu.active = !actMenu.active;
			}
			
		}

	}

	void FixedUpdate()
	{
		// Move our character
		//controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		if (isParalyzed)
		{
			((CharacterController2D)controller).Move(0f * Time.fixedDeltaTime, false, false);
		}
		else
		{
			((CharacterController2D)controller).Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		}
		jump = false;
		act = false;
	}
}
