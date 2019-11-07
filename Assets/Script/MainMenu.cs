using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//FOr loading scene
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	Scene currentScene;
	private void Start()
	{
		currentScene = SceneManager.GetActiveScene();
	}
	private void Update()
	{
		if (currentScene.buildIndex == 4 && Input.GetButtonDown("Jump"))
		{
			SceneManager.LoadScene(0);
		}
		else if(Input.anyKeyDown)
		{
			NextScene();
		}
	}
	public void NextScene()
	{
		//Get the current scene
		currentScene = SceneManager.GetActiveScene();
		//Load the next scene aftercrurent scene
		SceneManager.LoadScene(currentScene.buildIndex + 1);
	}

}