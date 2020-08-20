using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

namespace UnityStandardAssets.CrossPlatformInput
{
	public class AxisTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{

		public float axisValue = 1; // The axis that the value ha
		private GameObject main;
		private float state = 0;

		void Start()
		{
			main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
		}

		public void OnPointerDown(PointerEventData data)
		{
			state = axisValue;
			if (main.GetComponent<main_script>().BlockInput)
			{
				main.GetComponent<movement>().SetHorizontalInput(0);
			}
			else
			{
				main.GetComponent<movement>().SetHorizontalInput(state);
			}
		}

		public void OnPointerUp(PointerEventData data)
		{
			state = 0;
			((movement)main.GetComponent<movement>()).SetHorizontalInput(state);
		}
	}
}