using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemyController : MonoBehaviour {

    //SEの設定
    private AudioSource audiosource;
    public AudioClip Damage; //ダメージを受けた時のSE
    public AudioClip Delete; //消える時のSE

    //弾を出す時間を設定
    private float VurretTime;
    //弾の設定
    public GameObject Burret;
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
        if (this.gameObject.tag == "Enemy")
        {
            Life = 1;
        }
        else if (this.gameObject.tag == "Strong Enemy")
        {
            Life = 3;
        }
        //初めに弾を出す。
        GameObject tama = Instantiate(Burret) as GameObject;
        tama.transform.position = new Vector2(this.transform.position.x , this.transform.position.y - 0.7f);

    }
	
	// Update is called once per frame
	void Update () {

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //一定時間が経過するごとに弾を出す。
        VurretTime += Time.deltaTime;
        if (VurretTime >= 1.0f)
        {
            GameObject tama = Instantiate(Burret) as GameObject;
            tama.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.7f);
            VurretTime = 0;
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

        //Hpがなくなると消える
        if (Life <= 0)
        {
            //音を鳴らす
            audiosource.PlayOneShot(Delete, 1.0f);
            Invoke("Death", 0.1f);
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
    private void Death()
    {

        Destroy(gameObject);
    }
}
