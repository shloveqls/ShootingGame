﻿using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	// スクロールするスピード
	public float speed = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// 時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
		float y = Mathf.Repeat (Time.time * speed, 1);

		// Yの値がずれていくオフセットを作成
		Vector2 offset = new Vector2 (0, y);

		// マテリアルにオフセットを設定する
		Renderer renderer = gameObject.GetComponent<Renderer>();
		renderer.sharedMaterial.SetTextureOffset ("_MainTex", offset);

	}

}
