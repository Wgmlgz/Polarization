using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

namespace UnityStandardAssets.CrossPlatformInput
{
	public class AxisTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		// designed to work in a pair with another axis touch button
		// (typically with one having -1 and one having 1 axisValues)
		public string axisName = "Horizontal"; // The name of the axis
		public float axisValue = 1; // The axis that the value ha
		private GameObject main;
		private float state = 0;
		AxisTouchButton m_PairedWith; // Which button this one is paired with
		CrossPlatformInputManager.VirtualAxis m_Axis; // A reference to the virtual axis as it is in the cross platform input

		GameObject GetChildWithName(GameObject obj, string name)
		{
			Transform trans = obj.transform;
			Transform childTrans = trans.Find(name);
			if (childTrans != null)
			{
				return childTrans.gameObject;
			}
			else
			{
				return null;
			}
		}

		void Start()
		{
			main = (GameObject)GameObject.FindGameObjectsWithTag("main").GetValue(0);
		}
		void OnEnable()
		{
			if (!CrossPlatformInputManager.AxisExists(axisName))
			{
				// if the axis doesnt exist create a new one in cross platform input
				m_Axis = new CrossPlatformInputManager.VirtualAxis(axisName);
				CrossPlatformInputManager.RegisterVirtualAxis(m_Axis);
			}
			else
			{
				m_Axis = CrossPlatformInputManager.VirtualAxisReference(axisName);
			}
			FindPairedButton();
		}

		void FindPairedButton()
		{
			// find the other button witch which this button should be paired
			// (it should have the same axisName)
			var otherAxisButtons = FindObjectsOfType(typeof(AxisTouchButton)) as AxisTouchButton[];

			if (otherAxisButtons != null)
			{
				for (int i = 0; i < otherAxisButtons.Length; i++)
				{
					if (otherAxisButtons[i].axisName == axisName && otherAxisButtons[i] != this)
					{
						m_PairedWith = otherAxisButtons[i];
					}
				}
			}
		}

		void OnDisable()
		{
			// The object is disabled so remove it from the cross platform input system
			m_Axis.Remove();
		}

		public void OnPointerDown(PointerEventData data)
		{
			state = axisValue;
			((movement)main.GetComponent<movement>()).SetHorizontalInput(state);
		}

		public void OnPointerUp(PointerEventData data)
		{
			state = 0;
			((movement)main.GetComponent<movement>()).SetHorizontalInput(state);
		}
	}
}