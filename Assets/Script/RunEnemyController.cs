using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemyController : MonoBehaviour {

    //敵によって変える数値。これによって、走る速さを変える。
    public int Number;
    //左、右それぞれに行くフラグ
    private bool RightFlag = false;
    private bool LeftFlag = false;
    //AxManを取得
    public GameObject AxMan;
    //生命力
    private int Life;
    //敵に当たったか判断
    private bool isDmaged = false;
    //無敵時間を設定
    private float DamageTime;

    // Use this for initialization
    void Start () {

        //始めは左へ行くフラグを立てる
        LeftFlag = true;

        //Ax　Manのオブジェクトを取得
        AxMan = GameObject.Find("Ax Man");

        //敵のタグによってHpが変動
        if (this.gameObject.tag == "Enemy")
        {
            Life = 1;
        }
        else if (this.gameObject.tag == "Strong Enemy")
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

        //落ちたら消える
        if (this.transform.position.y <= -6.0f)
        {
            Destroy(gameObject);
        }

        //フラグによって進む方向を変えて進む。数字によって速さが変わる
        if (LeftFlag)
        {
            
            if (Number == 0)
            {
                this.transform.Translate(-0.05f, 0, 0);
            }
            else if (Number == 1)
            {
                this.transform.Translate(-0.1f, 0, 0);
            }
        }
        else if (RightFlag)
        {
            if(Number == 0)
            {
                this.transform.Translate(0.05f, 0, 0);
            }
            else if (Number == 1)
            {
                this.transform.Translate(0.1f, 0, 0);
            }
        }

        //Hpがなくなると消える
        if (Life <= 0)
        {
            Destroy(gameObject);
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
            Life--;
            isDmaged = true;
        }

        //Wallにぶつかると、フラグ変更
        if ((collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Strong Enemy") && LeftFlag)
        {
            LeftFlag = false;
            RightFlag = true;
        }
        else if ((collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Strong Enemy") && RightFlag)
        {
            LeftFlag = true;
            RightFlag = false;
        }
    }
}
