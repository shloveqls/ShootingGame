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
	void Update () {
	
		// 右・左
		float x = Input.GetAxisRaw ("Horizontal");

		// 上・下
		float y = Input.GetAxisRaw ("Vertical");

		// 移動する向きを求める
		Vector2 direction = new Vector2 (x, y).normalized;

		// 移動
		spaceship.Move (direction);

	}

	private IEnumerator InitBullet() {

		while (true) {

			// 弾をプレイヤーと同じ位置/角度で作成
			spaceship.Shot (gameObject.transform);

			// shotDelay秒待つ
			yield return new WaitForSeconds(spaceship.shotDelay);

		}

	}

}
