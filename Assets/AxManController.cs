
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxManController : MonoBehaviour {

    //ライフの設定
    private int Life = 3;
    //アニメーション用のコンポーネントを入れる
    private Animator myAnimator;
    //RigidBodyコンポーネントを入れる
    private Rigidbody2D myrigidBody;  
    //進む力
    private float Force = 5.0f;
    //ジャンプする力
    private float Jump = 10;
    //地面
    private float groundLevel = -0.3f;


	// Use this for initialization
	void Start () {

        //Animatorコンポーネントの取得
        this.myAnimator = GetComponent<Animator>();
        //RigidBodyコンポーネントの取得
        this.myrigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;

        //矢印キーまたは各ボタンで左右に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //左へ移動、左移動のアニメーションを起動、右移動のステートを消す
            this.myAnimator.SetBool("Left", true);
            this.myrigidBody.velocity = new Vector2(-this.Force, this.myrigidBody.velocity.y);
            this.myAnimator.SetBool("Right", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {   
            //左キー又はボタンが離されたら、左アニメーションを変更
            this.myAnimator.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //右へ移動、右移動のアニメーションを起動、左移動のステートを消す
            this.myAnimator.SetBool("Right", true);
            this.myrigidBody.velocity = new Vector2(this.Force, this.myrigidBody.velocity.y);
            this.myAnimator.SetBool("Left", false);

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //右キー又はボタンが離されたら、右アニメーションを変更
            this.myAnimator.SetBool("Right", false);
        }

        //上キー又はボタンを押されたらジャンプしアニメーションも起動
        if (Input.GetKey(KeyCode.UpArrow)&& isGround) {
            this.myAnimator.SetBool("Jump", true);
            this.myrigidBody.velocity = new Vector2(0, this.Jump);

        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            this.myAnimator.SetBool("Jump", false);
            
        }

    }
}