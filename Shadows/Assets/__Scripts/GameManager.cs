using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	private static GameManager _instance;

	public static GameManager Instance { get { return _instance; } }

	public int playerLives = 3;
	public int playerHealth = 6;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}

		DontDestroyOnLoad(this);

	}

	public void Reset()
	{
		playerLives = 3;
		playerHealth = 6;

	}

}
