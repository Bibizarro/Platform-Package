using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionKamikaze : MonoBehaviour {
	//movimentação
	private Vector2 movement;
     Rigidbody2D rb; 
	public float speed ;
	public Transform mileva;
	public Transform rodny;
	private Transform presa;
	private Transform middle;
	//explosão
	int gonnaExplode;
	public float cooldownExplosion;
	public float cooldownToExplode;
	Animator an;
	//hp
	int health = 1;

	// Use this for initialization
	void Start () {
		movement = Vector2.zero;
		rb = GetComponent <Rigidbody2D>() ;
		an = GetComponent<Animator> ();
		mileva = GameObject.Find ("Mileva").transform;
		rodny = GameObject.Find ("Rodny").transform;
		middle = GameObject.Find ("Middle").transform;


	}
	
	// Update is called once per frame


	void Update () {

		ChecandoPresa ();
		Movimentacao ();
		Explosao ();
		Dead ();
		Movimentacao ();


	}


	void Movimentacao(){


		if (transform.position.x > presa.position.x) //caso esteja à esqueda dele
		{
			transform.eulerAngles = new Vector2 (0,180);
			rb.velocity = new Vector2 ( -speed * Time.deltaTime , rb.velocity.y);
		}

		if (transform.position.x < presa.position.x) //caso esteja à direita dele
		{
			transform.eulerAngles = new Vector2 (0,180);
			rb.velocity = new Vector2 (speed * Time.deltaTime , rb.velocity.y);
		}


	}


	void ChecandoPresa()
	{
		if (Vector2.Distance (mileva.position, transform.position) < Vector2.Distance (rodny.position, transform.position)) {
			presa = mileva;
		}

		if (Vector2.Distance (mileva.position, transform.position) == Vector2.Distance (rodny.position, transform.position)) {
			presa = mileva;
		}

		if (Vector2.Distance (mileva.position, transform.position) > Vector2.Distance (rodny.position, transform.position)) {
			presa = rodny;
		}

	}

	void Explosao()
	{
		if (Vector2.Distance (new Vector2 (presa.position.x, 0), new Vector2 (transform.position.x, 0)) < 1 && Vector2.Distance (new Vector2 (0, presa.position.y), new Vector2 (0, transform.position.y)) < 1)
		{
			gonnaExplode += 1 ;
		}

		PrepareToExplode ();
	}

	void PrepareToExplode()
	{

		if (gonnaExplode == 1) {
			cooldownToExplode = cooldownExplosion;
		}

		if (cooldownToExplode > 0 && gonnaExplode >= 1) {
			cooldownToExplode -= Time.deltaTime;
			speed = 0;
			rb.mass = 9999999;
		} 

		else if (cooldownToExplode <= 0 && gonnaExplode >=1) {
				an.SetBool ("_exploding", true);
				Destroy (gameObject, 0.5f);
			}
		}

	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.tag == "TiroPlayer") 
		{
			health -= 1;
		}
	}

	void Dead()
	{
		if (health <= 0) 
		{
			Destroy (gameObject, 1f);
		}
	}


}


