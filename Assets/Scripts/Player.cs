using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Spaceshipコンポーネント
	private Spaceship spaceship;
	
	// Startメソッドをコルーチンとして呼び出す
	void Start () {

		// Spaceshipコンポーネントを取得
		spaceship = gameObject.GetComponent<Spaceship> ();

		StartCoroutine ("InitBullet");

	}
	
	// Update is called once per frame
	void Update ()
	{
		// 右・左
		float x = Input.GetAxisRaw ("Horizontal");

		// 上・下
		float y = Input.GetAxisRaw ("Vertical");

		// 移動する向きを求める
		Vector2 direction = new Vector2 (x, y).normalized;

		// 移動
		Move (direction);
	}

	void Move (Vector2 direction)
	{
		// 画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		
		// 画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		
		// プレイヤーの座標を取得
		Vector2 pos = transform.position;

		// 移動量を加える
		pos += direction  * spaceship.speed * Time.deltaTime;
		
		// プレイヤーの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
		
		// 制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
	}

	// ぶつかった瞬間に呼び出される
	void OnTriggerEnter2D(Collider2D other) {

		// レイヤー名を取得
		string layerName = LayerMask.LayerToName (other.gameObject.layer);

		// レイヤー名がBullet (Enemy)の時は弾を削除
		if ("Bullet (Enemy)".Equals(layerName)) {
			// 弾の削除
			Destroy(other.gameObject);
		}

		if ("Bullet (Enemy)".Equals(layerName) || "Enemy".Equals(layerName)) {
			// 爆発する
			spaceship.Explosion ();
			// プレイヤーを削除
			Destroy (gameObject);
		}

	}

	private IEnumerator InitBullet() {

		while (true) {

			// 弾をプレイヤーと同じ位置/角度で作成
			spaceship.Shot (gameObject.transform);

			// ショット音を鳴らす
			AudioSource audio = gameObject.GetComponent<AudioSource>();
			audio.Play();

			// shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);

		}

	}

}
