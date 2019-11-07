using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
	private Rigidbody rigid;
	public float fallDelay = 2.0f;
	bool isFalling;
	bool isDestroy;
	private void Start()
	{
		isFalling = false;
		isDestroy = false;
		rigid = GetComponent<Rigidbody>();
	}
	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			StartCoroutine(FallPlatform());
		}
	}
	private void Update()
	{
		if (transform.position.y < -7f)
		{
			Destroy(gameObject);
			isDestroy = true;
		}
	}
	IEnumerator FallPlatform()
	{
		yield return new WaitForSeconds(fallDelay);
		rigid.isKinematic = false;
		isFalling = true;
	}
}

