using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroMileva : MonoBehaviour {

    public float speed;

    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		speed += 0.09f;
		transform.Translate(Vector2.right * speed*Time.deltaTime);
        Destroy(gameObject, 1.5f);
    }

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.tag == "Inimigo" ) {
			Destroy (gameObject);
		}
	}


}
