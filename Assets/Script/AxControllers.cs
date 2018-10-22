using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxControllers : MonoBehaviour
{
    //進むスピード
    private float speed = 0.1f;
    //AxManを取得
    public GameObject AxMan;
    //時間
    private float TimE;
    //アニメーション用のコンポーネントを入れる
    private Animator myAnimator;

    // Use this for initialization
    void Start()
    {
        //Ax　Manのオブジェクトを取得
        AxMan = GameObject.Find("Ax Man");

        //Animatorコンポーネントの取得
        this.myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }
        //生成されてからの時間を設定
        TimE += Time.deltaTime;

        //AxManが右向きの時、右へ進ませる,左の時は左へ進ませ、左のアニメーション開始する
        if (this.transform.position.x > AxMan.transform.position.x )
        {
            if (this.tag =="Green")
            {
                this.transform.Translate(speed *2, 0, 0);
            }
            else if (this.tag == "Red")
            {
                this.transform.Translate(speed * 0.5f, 0, 0);
            }
            else
            {
                this.transform.Translate(speed, 0, 0);
            }                    
        }
         else if (this.transform.position.x < AxMan.transform.position.x )
        {          
            this.myAnimator.SetBool("Left", true);
            if (this.tag == "Green")
            {
                this.transform.Translate(-speed * 2, 0, 0);
            }
            else if (this.tag == "Red")
            {
                this.transform.Translate(-speed * 0.5f, 0, 0);
            }
            else
            {
                this.transform.Translate(-speed, 0, 0);
            }
        }

        //一定の時間を超えたら、消す
        if (TimE > 1.0f)
        {
            Destroy(gameObject);
        }
    }

    //何かに接触した時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //敵、壁、アイテム、ゴールに当たったら、消える
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Item" || collision.gameObject.tag == "Goal")
        {
            Destroy(gameObject);
        }
    }

}
