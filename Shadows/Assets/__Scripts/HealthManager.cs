using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxPlayerHealth;
    public int currentHealth = 6;
	public Image[] healthImage;



	private static HealthManager _instance;

	public static HealthManager Instance { get { return _instance; } }


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

	}


	private void Start()
	{
		currentHealth = GameManager.Instance.playerHealth;
		
	}

	// Update is called once per frame
	void Update()
    {
		switch (currentHealth)
		{

			case 6:
				healthImage[0].fillAmount = 1;
				healthImage[1].fillAmount = 1;
				healthImage[2].fillAmount = 1;
				break;

			case 5:
				healthImage[0].fillAmount = 0.5f;
				healthImage[1].fillAmount = 1;
				healthImage[2].fillAmount = 1;
				break;

			case 4:
				healthImage[0].fillAmount = 0;
				healthImage[1].fillAmount = 1;
				healthImage[2].fillAmount = 1;
				break;

			case 3:
				healthImage[0].fillAmount = 0;
				healthImage[1].fillAmount = 0.5f;
				healthImage[2].fillAmount = 1;
				break;

			case 2:
				healthImage[0].fillAmount = 0;
				healthImage[1].fillAmount = 0;
				healthImage[2].fillAmount = 1;
				break;

			case 1:
				healthImage[0].fillAmount = 0;
				healthImage[1].fillAmount = 0;
				healthImage[2].fillAmount = 0.5f;
				break;

			case 0:
				healthImage[0].fillAmount = 0;
				healthImage[1].fillAmount = 0;
				healthImage[2].fillAmount = 0;
				break;
		}
	}

}
