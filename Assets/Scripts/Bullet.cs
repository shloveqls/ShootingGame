using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int speed = 10;

	// Use this for initialization
	void Start () {

		Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		rigidbody2D.velocity = gameObject.transform.up.normalized * speed;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
