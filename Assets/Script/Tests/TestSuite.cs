using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestSuite
    {
		private GameObject game;
		private GameManager gameManager;
		private Player player;
		private OnEmpty onEmpty;

		[SetUp]

		public void Setup()
		{
			//Load the game prefab
			GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
			//Instantiate into the world
			game = Object.Instantiate(prefab);
			//Getting player reference
			player = game.GetComponentInChildren<Player>();
		}
		[UnityTest]
		public IEnumerator GamePrefabLoaded()
		{
			yield return new WaitForEndOfFrame();
			//Checking if the gameobject game is spawned into the world
			Assert.NotNull(game);

		}
		[UnityTest]
		public IEnumerator PlayerExists()
		{
			yield return new WaitForEndOfFrame();
			//Checking if the gameobject player is spawned into the world
			Assert.NotNull(player);
		}
		[UnityTest]
		public IEnumerator PlayerIsJumping()
		{
			//Let the world load
			yield return new WaitForEndOfFrame();
			//Getting the player starting position and store them as playerPosY
			float playerPosY = player.transform.localPosition.y;
			//Call the jump function
			player.Jump();
			//Let it wait until the end off frame so we can get the new playerposY value
			yield return new WaitForEndOfFrame();
			//Checking if the new Jumping value playerPosY is greater than the starting value
			Assert.Greater(player.transform.localPosition.y, playerPosY);
		}
		[UnityTest]
		public IEnumerator ItemCollected()
		{
			//Load the itemPrefab from resouces
			GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
			//Getting player position value so it will spawn on top of the player
			Vector3 playerPos = player.transform.position;
			//Instantiate the item into the world
			GameObject item = Object.Instantiate(itemPrefab, playerPos, Quaternion.identity);
			//Wait for the item has collided with the player
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			//Return true if the item is destroyed
			Assert.IsTrue(item == null);
			

		}
		[UnityTest]
		public IEnumerator DoorOpen()
		{
			//Load the itemPrefab from resouces
			GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Item");
			//Getting player position value so it will spawn on top of the player
			Vector3 playerPos = player.transform.position;
			//Instantiate the item into the world
			itemPrefab = Object.Instantiate(itemPrefab, playerPos, Quaternion.identity);
			//Getting onEmpty Script reference
			onEmpty = game.GetComponentInChildren<OnEmpty>();
			//Checking if the Parent has no children in them (or all items have been collected)
			if(onEmpty.transform.childCount == 0)
			{
				//Then Call the OpenDoor Function
				onEmpty.OpenDoor();
			}
			//Wait for a while for it to run
			yield return new WaitForFixedUpdate();
			yield return new WaitForEndOfFrame();
			//Return true if the door has been opened;
			Assert.IsTrue(onEmpty.isOpen = true);

		}
		[TearDown]
		public void TearDown()
		{
			Object.Destroy(game);
		}
	}
}
