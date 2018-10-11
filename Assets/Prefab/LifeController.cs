using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {

    //AxManを取得
    public GameObject AxMan;
    //AxManControllerを取得
    public AxManController AxMancontroller;

    //アニメーション用のコンポーネントを入れる
    private Animator myAnimator;

    //他のスクリプトから貰ってきたライフの数値
    public int life;

    // Use this for initialization
    void Start () {

        //Ax　Manのオブジェクトを取得
        AxMan = GameObject.Find("Ax Man");
        //AxManControllerのコンポーネントを取得
        AxMancontroller = GetComponent<AxManController>();
        //Animatorコンポーネントの取得
        this.myAnimator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        life = AxMancontroller.Life;
        

        //lifeの数値によって、ライフを増減させる
        if (life == 3)
        {
            this.myAnimator.SetFloat("Life", 3.0f);
        }
        else if (life ==2)
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
