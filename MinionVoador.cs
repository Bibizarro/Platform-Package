using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionVoador : MonoBehaviour {


	//movimentaçao
    public Transform minionVoador;
    public Transform rodny;
    public Transform mileva;
    Transform presa;
    public float delay = 0.009f;

	//hp
	public int vida = 1;
	bool morto;


    // Use this for initialization
    void Start () {

        mileva = GameObject.Find("Mileva").transform;
        rodny = GameObject.Find("Rodny").transform;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
		ChecandoPresa ();
		CacandoPlayer ();
		Morte ();

	}


    void CacandoPlayer()
    {
		

			minionVoador.position = Vector3.Lerp (minionVoador.position, presa.transform.position, delay);


    }

	void ChecandoPresa()
	{
		if (!morto) {
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
	}

		void Morte()
		{
			if (vida == 0)
			{
				morto = true;
			delay = 0;
				Destroy(gameObject, 1f);
			}
		}


		void OnTriggerEnter2D(Collider2D coll)
		{

			if (coll.tag == "TiroPlayer")
			{
				vida -= 1;
			}
		}
	
		
	
}
