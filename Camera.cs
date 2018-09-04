using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {



    Vector2 velocidade;

	public float xSuave , ySuave;

	public bool limite;

	public Vector3 minCam , maxCam;

	public GameObject player;

	void Start() {


		player = GameObject.FindGameObjectWithTag ("Player");




	}

	// Update is called once per frame
	void FixedUpdate () {

        float xzin = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocidade.x, xSuave);
			float yzin = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocidade.y, ySuave);

        transform.position = new Vector3 (xzin, yzin, transform.position.z);

        if (limite)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCam.x, maxCam.x),
                Mathf.Clamp(transform.position.y, minCam.y, maxCam.y),
                Mathf.Clamp(transform.position.z, minCam.z, maxCam.z));

        }
      


    }
}
