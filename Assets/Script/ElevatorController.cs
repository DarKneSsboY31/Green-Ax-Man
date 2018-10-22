using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

    //上限、下限の設定
    public float Highest = 33.0f;
    public float lowest = -1.0f;

    //上限へ行くフラグ、下限へ行くフラグ
    private bool GoUp = false;
    private bool GoDown = false;

    //スピード
    public float speed = 0.07f;

	// Use this for initialization
	void Start () {

        //最初は上行きのフラグをON
        GoUp = true;
	}
	
	// Update is called once per frame
	void Update () {

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //上限まで行くと、下行きのフラグをON
        if (this.transform.position.y > Highest)
        {
            GoUp = false;
            GoDown = true;

        }
        //下限に行くと、上行きのフラグをON
        else if (this.transform.position.y < lowest)
        {
            GoUp = true;
            GoDown = false;
        }

        //上行きフラグがONの時、上へ行く
        if (GoUp)
        {
            
            Invoke("GoingUp", 1.0f);
        }
        else if (GoDown)
        {
            Invoke("GoingDown", 1.0f);
        }


    }

    //上、下息のフラグがONになれば動く関数
    private void GoingUp()
    {
        this.transform.Translate(0, speed, 0); //上
    }

    private void GoingDown()
    {
        this.transform.Translate(0, -speed, 0); //下
    }
}
