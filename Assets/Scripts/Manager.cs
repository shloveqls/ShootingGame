﻿using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	// Playerプレハブ
	public GameObject player;
	
	// タイトル
	private GameObject title;

	// Use this for initialization
	void Start ()
	{
		// Titleゲームオブジェクトを検索し取得する
		title = GameObject.Find ("Title");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// ゲーム中ではなく、Xキーが押されたらtrueを返す。
		if (IsPlaying () == false && Input.GetKeyDown (KeyCode.X)) {
			GameStart ();
		}
	}
	
	void GameStart ()
	{
		// ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
		title.SetActive (false);
		Instantiate (player, player.transform.position, player.transform.rotation);
	}
	
	public void GameOver ()
	{
		// ハイスコアの保存
		Score score = FindObjectOfType<Score> ();
		score.Save();

		// ゲームオーバー時に、タイトルを表示する
		title.SetActive (true);
	}
	
	public bool IsPlaying ()
	{
		// ゲーム中かどうかはタイトルの表示/非表示で判断する
		return title.activeSelf == false;
	}
}
