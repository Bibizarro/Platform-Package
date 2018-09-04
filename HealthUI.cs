using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
	 //Mileva
	public Sprite[] milevaHealth;
	public Image milevaHealthUI;
	private Mileva mileva;
	//Rodny
	public Sprite[] rodnyHealth;
	public Image rodnyHealthUI;
	private Rodny rodny;


	// Use this for initialization
	void Start () 
	{
		rodny = GameObject.Find ("Rodny").GetComponent<Rodny>();
		mileva = GameObject.Find ("Mileva").GetComponent<Mileva> ();
	}
	
	// Update is called once per frame
	void Update () {

		milevaHealthUI.sprite = milevaHealth[mileva.health];

		rodnyHealthUI.sprite = rodnyHealth [rodny.health];
		
	}
}
