﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	// 弾の移動スピード
	public int speed;

	// ゲームオブジェクト生成から削除するまでの時間
	public float lifeTime = 5;

	// Use this for initialization
	void Start () {

		// ローカル座標のY軸方向に移動する
		Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D> ();
		rigidbody2D.velocity = gameObject.transform.up.normalized * speed;

		// lifeTime秒後に削除
		Destroy (gameObject, lifeTime);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
