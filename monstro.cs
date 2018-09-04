using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monstro : MonoBehaviour {
	//movimentação
	public Rigidbody2D rb; 
	public float speed ;
	public Transform mileva;
	public Transform rodny;
    private Transform presa;
	private Transform middle;
	bool chasingChicken;
	//pulo
	public LayerMask plataform;
	public float jumpForce;
	public Transform lineStart;
	public Transform lineEnd;
	public Transform groundCheck;
	public float groundCheckSize;

	//tiro
	public Transform origemTiro;
	public GameObject tiro;
	float cooldownTiro;
	float velocidadeTiro = 1f;

    //vida
    public int vida = 1;
    bool morto;


	// Use this for initialization
	void Start () { 

		rb = GetComponent <Rigidbody2D>() ;
		mileva = GameObject.Find ("Mileva").transform;
		rodny = GameObject.Find ("Rodny").transform;
		middle = GameObject.Find ("Middle").transform;
		lineStart = GameObject.Find ("LineStart").transform;
		lineEnd = GameObject.Find ("LineEnd").transform;

	}

	// Update is called once per frame
	void Update () {

        ChecandoPresa();
		Movimentacao ();
		JumpChecker ();
		CooldownTiro ();
		Atirando ();
		BackToMiddle();
        Morte();
	}




		void Movimentacao(){
        
		if (transform.position.x > presa.position.x && !chasingChicken) //caso esteja à esqueda dele
			{
				transform.eulerAngles = new Vector2 (0,180);
				rb.velocity = new Vector2 ( -speed * Time.deltaTime , rb.velocity.y);
			}

		if (transform.position.x < presa.position.x  && !chasingChicken) //caso esteja à direita dele
			{
				transform.eulerAngles = new Vector2 (0,180);
				rb.velocity = new Vector2 (speed * Time.deltaTime , rb.velocity.y);
			}


		if (rb.velocity.x < 0) 
		{
			transform.eulerAngles = new Vector2(0,180);
		}
		if (rb.velocity.x > 0) 
		{
			transform.eulerAngles = new Vector2(0,0);
		}


		}


    void ChecandoPresa()
    {
        if (Vector2.Distance(mileva.position , transform.position) < Vector2.Distance(rodny.position, transform.position))
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

	void Atirando ()
	{
		if(cooldownTiro <= 0 && !morto)
		
			{
			
			if(Vector2.Distance(new Vector2(presa.position.x, 0) ,new Vector2(transform.position.x, 0))< 7 && Vector2.Distance(new Vector2(0,presa.position.y),new Vector2(0,transform.position.y))< 1)

			{
			var cloneTiro = Instantiate(tiro,origemTiro.position,Quaternion.identity) as GameObject ;
			cloneTiro.transform.eulerAngles = transform.eulerAngles;
				cooldownTiro = velocidadeTiro;
			}



		}

	}

	void CooldownTiro(){
		if (cooldownTiro > 0)
		{cooldownTiro -= Time.deltaTime;}
			
		
	}
    void Morte()
    {
        if (vida == 0)
        {
            morto = true;
            speed = 0;
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
		
	void BackToMiddle()
	{
		
		if (transform.position.x > middle.position.x  && chasingChicken) //caso esteja à esqueda dele
		{
			transform.eulerAngles = new Vector2 (0,180);
			rb.velocity = new Vector2 ( -speed * Time.deltaTime , rb.velocity.y);
		}

		if (transform.position.x < middle.position.x  && chasingChicken) //caso esteja à direita dele
		{
			transform.eulerAngles = new Vector2 (0,180);
			rb.velocity = new Vector2 (speed * Time.deltaTime , rb.velocity.y);
		}

		if(transform.position == middle.position)
		chasingChicken = false;
		
	}


	void JumpChecker()
	{
		Debug.DrawLine(lineStart.position, lineEnd.position, Color.red);
		if(Physics2D.Linecast(lineStart.position,lineEnd.position , plataform))
		{
			if (transform.position.y - presa.position.y  <= 1) {
				Jump ();
			}
			if (transform.position.y - presa.position.y > 3) {
				chasingChicken = true;
				BackToMiddle();
			}
		}
	}


		void Jump(){
		rb.AddForce (new Vector2 (transform.position.x, jumpForce));
		}
	}










