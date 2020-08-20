using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

namespace UnityStandardAssets.CrossPlatformInput
{
	public class ActButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private GameObject main;

		void Start()
		{
			main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
		}

		public void OnPointerDown(PointerEventData data)
		{
			if (main.GetComponent<main_script>().BlockInput == false)
			{
				main.GetComponent<movement>().ActInput();
			}
		}

		public void OnPointerUp(PointerEventData data)
		{
		}
	}
}