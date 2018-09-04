using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mileva : MonoBehaviour {
	//movimentação
    public float velocidade;
    private Rigidbody2D rb;
	//Pulo e parede
	public LayerMask Plataforma;
    public LayerMask doesNotStick;
    public Vector2 colisaoPiso = Vector2.zero;
	public bool estaNoChao;
	public float raio;
	public Color debugCorColisao = Color.magenta;
	public float forcaPulo;
    bool isOnWall;
    public Transform wallChecker;
    public float wallCheckerSize = 0.5f;

    
    //tiro e espada
    public Transform attacksSpawn;
    public GameObject tiro;
    float cooldownTiro;
    float velocidadeTiro = 0.5f;
	public GameObject lightsaber;
	float cooldownAttack = 0.5f;
	float canAttack;
	float jumpAttacks;


	//health
	public int health = 5;
	private bool dead;


    // Use this for initialization
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
	{
		
		Movimentacao();
		GroundAndWallChecker();
		Pulo();
        Atirando();
            CooldownTiro();
		Dead ();
		MeleeAttack ();
		AttackOnJumping ();
		CooldownLightsaber();

    }

    void Movimentacao()
    {
        if (!isOnWall)
        {
            rb.velocity = new Vector2(velocidade * Input.GetAxis("Horizontal"), rb.velocity.y);

        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }


    }
		

	//Verificar se está no chão
	private void GroundAndWallChecker()
	{
		var posicao = colisaoPiso;
		posicao.x += transform.position.x;
		posicao.y += transform.position.y;
		estaNoChao = Physics2D.OverlapCircle (posicao, raio, Plataforma);
        isOnWall = Physics2D.OverlapCircle(wallChecker.position, wallCheckerSize, doesNotStick);
        
		if (estaNoChao)
		{
			jumpAttacks = 0;
		}

	}

	private void Pulo()
	{

		if (Input.GetKey(KeyCode.W))
		{
			if (estaNoChao && rb.velocity.y == 0) {

				rb.AddForce (new Vector2 (0, forcaPulo));		
			}		
		}
	}
	//Desenhar a area do detector de chão

	void OnDrawGizmos()
	{
		Gizmos.color = debugCorColisao;
		var posicao = colisaoPiso;
		posicao.x += transform.position.x;
		posicao.y += transform.position.y;
		Gizmos.DrawWireSphere (posicao, raio);
        Gizmos.DrawWireSphere(wallChecker.position, wallCheckerSize);
	}
    void Atirando()
    {
        if (Input.GetKey(KeyCode.Y) && cooldownTiro <= 0)
        {

			var cloneTiro = Instantiate(tiro, attacksSpawn.position, Quaternion.identity) as GameObject;
            cloneTiro.transform.eulerAngles = transform.eulerAngles;
            cooldownTiro = velocidadeTiro;
        }

    }


	void MeleeAttack()

	{ if (Input.GetKey(KeyCode.U) && canAttack <= 0 && cooldownTiro <=0 && jumpAttacks == 0)

		{

			var cloneLightsaber = Instantiate(lightsaber, attacksSpawn.position, Quaternion.identity) as GameObject;
			lightsaber.transform.eulerAngles = transform.eulerAngles;
			canAttack = cooldownAttack;
			Destroy (cloneLightsaber, 0.2f);
		}
		
	}
		

	void CooldownLightsaber()
	{
		if (canAttack > 0) {
			
			canAttack -= Time.deltaTime;
		}
		
	}


	void AttackOnJumping()
	{
		if(!estaNoChao && (Input.GetKey(KeyCode.U)) || !estaNoChao && (Input.GetKey(KeyCode.Y)))
		{
			jumpAttacks += 1;
			}

			}




    void CooldownTiro()
    {
        if (cooldownTiro > 0)
            cooldownTiro -= Time.deltaTime;
    }

	void Dead()
	{
		if (health == 0) 
		{
			dead = true;
			velocidade = 0;
			forcaPulo = 0;
		}
	}


	void OnTriggerEnter2D (Collider2D coll)
	{
		if (coll.tag == "TiroMonstro" && !dead) 
		{
			health -= 1;
		}
		if (coll.tag == "Explosion" && !dead) 
		{
			if (health >= 2)
				health -= 2;
			else
				health -= 1;
		}
	}
	void OnColliderEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Inimigo" && !dead) 
		{
			health -= 1;
		}
	}



}