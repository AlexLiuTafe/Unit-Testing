using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnEnter : MonoBehaviour
{
	public UnityEvent onEnter;
	public string hitTag = "";
	public bool destroy = false;

	void OnTriggerEnter(Collider col)
	{
		if (hitTag == col.tag || hitTag == "")
		{
			if (destroy)
			{
				Destroy(col.gameObject);
			}
		}
		//Calling this function
		onEnter.Invoke();

	}
}
