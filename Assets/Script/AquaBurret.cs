using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AquaBurret : MonoBehaviour {
    //進むスピード
    private float speed = 0.1f;
    //Boss1を取得
    public GameObject BossOne;
    //時間
    private float TimE;

    // Use this for initialization
    void Start()
    {
        //Boss1のオブジェクトを取得
        BossOne = GameObject.Find("Boss1");

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
        if (this.transform.position.x > BossOne.transform.position.x)
        {    
                this.transform.Translate(speed, 0, 0);
        }
        else if (this.transform.position.x < BossOne.transform.position.x)
        {    
                this.transform.Translate(-speed, 0, 0);   
        }

        //一定の時間を超えたら、消す
        if (TimE > 3.0f)
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
