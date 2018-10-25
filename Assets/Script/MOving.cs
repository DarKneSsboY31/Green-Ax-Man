using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOving : MonoBehaviour {

    //右限、左限の設定
    public float Rightest = 33.0f;
    public float Leftest = -1.0f;

    //上限へ行くフラグ、下限へ行くフラグ
    private bool GoRight = false;
    private bool GoLeft = false;

    //スピード
    public float speed = 0.07f;

    // Use this for initialization
    void Start()
    {

        //最初は右行きのフラグをON
        GoRight = true;
    }

    // Update is called once per frame
    void Update()
    {

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //右限まで行くと、左行きのフラグをON
        if (this.transform.position.x > Rightest)
        {
            GoRight = false;
            GoLeft = true;

        }
        //左限に行くと、右行きのフラグをON
        else if (this.transform.position.x < Leftest)
        {
            GoRight = true;
            GoLeft = false;
        }

        //右行きフラグがONの時、右へ行く。左行きフラグがONならば左へ。
        if (GoRight)
        {

            Invoke("GoingRight", 0.1f);
        }
        else if (GoLeft)
        {
            Invoke("GoingLeft", 0.1f);
        }


    }

    //上、下息のフラグがONになれば動く関数
    private void GoingRight()
    {
        this.transform.Translate(speed, 0, 0); //右
    }

    private void GoingLeft()
    {
        this.transform.Translate(-speed, 0, 0); //左
    }
}
