using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OnEmpty : MonoBehaviour
{
	public Transform door;
	public float distance = 1f;
	public float speed =0.01f;
	Vector3 endPos;
	public bool isOpen;
	private void Start()
	{
		isOpen = false;
		endPos = door.position + Vector3.right * distance;
	}
	void Update()
	{
		//Check if there are no more children left
		if (transform.childCount == 0)
		{
			
			OpenDoor();
			isOpen = true;
		}
	}
	//MY OWN SLIDING DOOR FUNCTION! YAY!
	public void OpenDoor()
	{
		//					lerping from start point to end point depending on the speed
		door.transform.position  = Vector3.Lerp(door.position,endPos,speed);
	}
}
