using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// ヒットポイント
	public int hp = 1;

	// スコアのポイント
	public int point = 100;

	// Spaceshipコンポーネント
	private Spaceship spaceship;

	// Use this for initialization
	void Start () {

		// Spaceshipコンポーネントを取得
		spaceship = gameObject.GetComponent<Spaceship> ();

		// ローカル座標のY軸のマイナス方向に移動する
		Move (transform.up * -1);

		StartCoroutine ("InitBullet");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {

		// レイヤー名を取得
		string layerName = LayerMask.LayerToName (other.gameObject.layer);

		Debug.Log (layerName);

		// レイヤー名がBullet (Player)以外の時は何も行わない
		if (!"Bullet (Player)".Equals (layerName)) return;

		// PlayerBulletのTransformを取得
		Transform playerBulletTransform = other.transform.parent;

		// Bulletコンポーネントを取得
		Bullet bullet = playerBulletTransform.GetComponent<Bullet> ();

		// ヒットポイントを減らす
		hp = hp - bullet.power;
		
		// 弾の削除
		Destroy (other.gameObject);

		// ヒットポイントが0以下であれば
		if (hp <= 0) {
			// スコアコンポーネントを取得してポイントを追加
			Score score = FindObjectOfType<Score>();
			score.AddPoint(point);

			// 爆発
			spaceship.Explosion ();
			
			// エネミーの削除
			Destroy (gameObject);
		} else {
			spaceship.GetAnimator().SetTrigger("Damage");
		}

	}

	// 機体の移動
	public void Move (Vector2 direction)
	{
		Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		rigidbody2D.velocity = direction * spaceship.speed;
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
