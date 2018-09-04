using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionTanker : MonoBehaviour {
    //movimentação
    public Rigidbody2D rb;
    public float speed;
    public Transform mileva;
    public Transform rodny;
    private Transform presa;
    private Transform middle;
    bool chasingChicken;
	 
	//ataque
	public Transform attackSpawn;
	public GameObject attack;
	public LayerMask player;
	public Transform lineEnd;
	float cooldownAttack = 1f;
	float canAttack;

   
    //vida
    public int vida = 1;
   
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        mileva = GameObject.Find("Mileva").transform;
        rodny = GameObject.Find("Rodny").transform;
        middle = GameObject.Find("Middle").transform;
        
    }
	
	// Update is called once per frame
	void Update () {

		Debug.DrawLine(transform.position, lineEnd.position, Color.blue);
        ChecandoPresa();
        Movimentacao();
		CooldownAttack ();
		MeleeAttack ();
        Morte();

    }

    void Movimentacao()
    {
		if (canAttack <= 0) {

			if (transform.position.x > presa.position.x && !chasingChicken) { //caso esteja à esqueda dele
				transform.eulerAngles = new Vector2 (0, 180);
				rb.velocity = new Vector2 (-speed * Time.deltaTime, rb.velocity.y);
			}

			if (transform.position.x < presa.position.x && !chasingChicken) { //caso esteja à direita dele
				transform.eulerAngles = new Vector2 (0, 180);
				rb.velocity = new Vector2 (speed * Time.deltaTime, rb.velocity.y);
			}


			if (rb.velocity.x < 0) {
				transform.eulerAngles = new Vector2 (0, 180);
			}
			if (rb.velocity.x > 0) {
				transform.eulerAngles = new Vector2 (0, 0);
			}
		}

    }

	void MeleeAttack()

	{ 
		
		if(Physics2D.Linecast(transform.position,lineEnd.position,player) && canAttack <= 0)
		{

			var cloneAttack = Instantiate(attack, attackSpawn.position, Quaternion.identity) as GameObject;
			cloneAttack.transform.eulerAngles = transform.eulerAngles;
			canAttack = cooldownAttack;
			Destroy (cloneAttack, 0.2f);
		}

	}


    void ChecandoPresa()
    {
        if (Vector2.Distance(mileva.position, transform.position) < Vector2.Distance(rodny.position, transform.position))
        {
            presa = mileva;
        }

        if (Vector2.Distance(mileva.position, transform.position) == Vector2.Distance(rodny.position, transform.position))
        {
            presa = mileva;
        }

        if (Vector2.Distance(mileva.position, transform.position) > Vector2.Distance(rodny.position, transform.position))
        {
            presa = rodny;
        }



    }


	void CooldownAttack()
	{
		if (canAttack > 0) {

			canAttack -= Time.deltaTime;
		}

	}

    void Morte()
    {
        if (vida == 0)
        {
           
            speed = 0;
            Destroy(gameObject, 1f);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {

		if (coll.tag == "TiroPlayer" || coll.tag == "LightsaberMileva")
        {
            vida -= 1;
        }




    }

}
