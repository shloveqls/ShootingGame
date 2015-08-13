using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Spaceshipコンポーネント
	private Spaceship spaceship;

	// Use this for initialization
	void Start () {

		// Spaceshipコンポーネントを取得
		spaceship = gameObject.GetComponent<Spaceship> ();

		// ローカル座標のY軸のマイナス方向に移動する
		spaceship.Move (gameObject.transform.up * -1);

		StartCoroutine ("InitBullet");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		// レイヤー名を取得
		string layerName = LayerMask.LayerToName (other.gameObject.layer);

		// レイヤー名がBullet (Player)以外の時は何も行わない
		if ("Bullet (Player)".Equals (layerName)) return;

		// 弾の削除
		Destroy (other.gameObject);

		// 爆発
		spaceship.Explosion ();

		// エネミーの削除
		Destroy (gameObject);

	}

	private IEnumerator InitBullet() {

		if (!spaceship.canShot) {
			yield break;
		}

		while (true) {

			// 子要素を全て取得する
			for (int i = 0; i < gameObject.transform.childCount; i++) {

				Transform shotPosition = gameObject.transform.GetChild(i);

				// ShotPositionの位置/角度で弾を撃つ
				spaceship.Shot(shotPosition);

			}

			// shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);

		}

	}

}
