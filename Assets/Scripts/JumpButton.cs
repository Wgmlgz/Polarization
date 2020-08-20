using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

namespace UnityStandardAssets.CrossPlatformInput
{
	public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		private GameObject main;
		AxisTouchButton m_PairedWith; // Which button this one is paired with
		CrossPlatformInputManager.VirtualAxis m_Axis; // A reference to the virtual axis as it is in the cross platform input

		void Start()
		{
			main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
		}

		public void OnPointerDown(PointerEventData data)
		{
			if (main.GetComponent<main_script>().BlockInput == false)
			{
				main.GetComponent<movement>().JumpInput();
			}
		}

		public void OnPointerUp(PointerEventData data)
		{
		}
	}
}