using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeControllerSecond : MonoBehaviour {

    //Bossを取得
    public GameObject Boss;
    //AxManControllerを取得
    public SecondBossController SecondBosscontroller;

    //アニメーション用のコンポーネントを入れる
    private Animator myAnimator;

    //他のスクリプトから貰ってきたライフの数値
    private int life;

    // Use this for initialization
    void Start()
    {

        //Boss2のオブジェクトを取得
        Boss = GameObject.Find("Boss2");
        //AxManControllerのコンポーネントを取得
        SecondBosscontroller = Boss.GetComponent<SecondBossController>();
        //Animatorコンポーネントの取得
        this.myAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        life = SecondBosscontroller.EnemyLife;


        //lifeの数値によって、ライフを増減させる
        if (life == 3)
        {
            this.myAnimator.SetFloat("Life", 3.0f);
        }
        else if (life == 2)
        {
            this.myAnimator.SetFloat("Life", 2.0f);
        }
        else if (life == 1)
        {
            this.myAnimator.SetFloat("Life", 1.0f);
        }
        else if (life == 0)
        {
            this.myAnimator.SetFloat("Life", 0.0f);
        }

    }
}
