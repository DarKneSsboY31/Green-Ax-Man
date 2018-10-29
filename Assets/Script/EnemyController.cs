using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    //SEの設定
    private AudioSource audiosource;
    public AudioClip Damage; //ダメージを受けた時のSE
    public AudioClip Delete; //消える時のSE

    //生命力
    private int Life;
    //敵に当たったか判断
    private bool isDmaged = false;
    //無敵時間を設定
    private float DamageTime;

    // Use this for initialization

    void Start () {

        //AudioSourceコンポーネントを取得
        audiosource = GetComponent<AudioSource>();

        //敵のタグによってHpが変動
        if (this.gameObject.tag =="Enemy")
        {
            Life = 1;
        }
        else if (this.gameObject.tag =="Strong Enemy")
        {
            Life = 3;
        }
        
		
	}
	
	// Update is called once per frame
	void Update () {

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //Hpがなくなると消える
        if (Life <= 0)
        {
            //音を鳴らす
            audiosource.PlayOneShot(Delete, 1.0f);
            Invoke("Death", 0.1f);

        }

        //敵に当たると1.5秒点滅、レイヤー変えて一時無敵にする、時間が経つと元に戻る
        if (isDmaged)
        {
            DamageTime += Time.deltaTime;
            this.gameObject.layer = LayerMask.NameToLayer("EnemyDamage");
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            if (DamageTime > 0.75f)
            {
                isDmaged = false;
                this.gameObject.layer = LayerMask.NameToLayer("Enemy");
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                DamageTime = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //斧、SpeCialManに当たったら、ダメージ
        if (collision.gameObject.tag == "SpecialMan" || collision.gameObject.tag == "Ax" || collision.gameObject.tag == "Green" || collision.gameObject.tag == "Red")
        {
            //音を鳴らす
            audiosource.PlayOneShot(Damage, 1.0f);
            Life--;
            isDmaged = true;
        }
    }
    //消滅する時の関数
    private void Death() {

        Destroy(gameObject);
    }
}
