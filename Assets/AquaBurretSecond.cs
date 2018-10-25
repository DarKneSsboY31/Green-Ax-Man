using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaBurretSecond : MonoBehaviour {
    //進むスピード
    private float speed = 0.2f;
    //Boss1を取得
    public GameObject BossTwo;
    //時間
    private float TimE;

    // Use this for initialization
    void Start()
    {
        //Boss1のオブジェクトを取得
        BossTwo = GameObject.Find("Boss2");

    }

    // Update is called once per frame
    void Update()
    {
        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        //生成されてからの時間を設定
        TimE += Time.deltaTime;

        //Boss1が右向きの時、右へ進ませる,左向きの時は左へ進ませ、左のアニメーション開始する
        if (this.transform.position.x > BossTwo.transform.position.x)
        {
            this.transform.Translate(speed, 0, 0);
        }
        else if (this.transform.position.x < BossTwo.transform.position.x)
        {
            this.transform.Translate(-speed, 0, 0);
        }

        //一定の時間を超えたら、消す
        if (TimE > 1.5f)
        {
            Destroy(gameObject);
        }
    }

    //何かに接触した時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //斧、壁、スペシャルマンに当たったら、消える
        if (collision.gameObject.tag == "SpecialMan" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "AxMan" || collision.gameObject.tag == "Ax" || collision.gameObject.tag == "Green" || collision.gameObject.tag == "Red")
        {
            Destroy(gameObject);
        }
    }
}
