using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpecialManControler : MonoBehaviour {

    //死亡ラインの設定
    private float DeadLine = -7.5f;
    //ジャンプが出来る条件
    private int Jumps = 0;
    //RigidBodyコンポーネントを入れる
    private Rigidbody2D myrigidBody;
    //進む力
    private float Force = 8.0f;
    //ジャンプする力
    private float Jump = 15;

    //左、右、ジャンプのボタンをそれぞれ押した時のトリガー
    private bool isLBdown2 = false;
    private bool isRBdown2 = false;
    private bool isJBdown2 = false;

    // Use this for initialization
    void Start () {
       
        //RigidBodyコンポーネントの取得
        this.myrigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //SpecialManがDeadLineを超えると、死亡、シーン移動
        if (this.transform.position.y < DeadLine)
        {
            Invoke("SceneMove", 0.1f);
        }

        //矢印キーまたは各ボタンで左右に移動
        if (Input.GetKey(KeyCode.LeftArrow) || isLBdown2)
        {
            //左へ移動
            this.myrigidBody.velocity = new Vector2(-this.Force, this.myrigidBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.RightArrow) || isRBdown2)
        {
            //右へ移動
            this.myrigidBody.velocity = new Vector2(this.Force, this.myrigidBody.velocity.y);

        }
        
        //地面に立っている時、上キー又はボタンを押されたらジャンプ
        if ((Input.GetKey(KeyCode.UpArrow) || isJBdown2) && Jumps == 1)
        {
            this.myrigidBody.velocity = new Vector2(0, this.Jump);

        }        

    }

    private void OnCollisionStay2D(Collision2D other)
    {       
        if (other.gameObject.tag =="Ground")
        {
            //ジャンプ条件を満たす
            Jumps = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.tag == "Ground")
        {
            //ジャンプ条件を満たす
            Jumps = 1;
        }

        //ゴールオブジェクトに当たった時、次のステージへ
        if (collision.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Stage1-Boss");
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //地面を離れる時、ジャンプ条件を消す
        Jumps = 0;
    }

    //左ボタンを押し続けた時、トリガーON
    public void GetMyLeftBottunDown2()
    {
        this.isLBdown2 = true;
    }
    //左ボタンを離した時、トリガーOFF
    public void GetMyLeftBottunUp2()
    {
        this.isLBdown2 = false;
    }
    //右ボタンを押し続けた時、トリガーON
    public void GetMyRightButtonDown2()
    {
        this.isRBdown2 = true;
    }
    //右ボタンを離した時、トリガーOFF
    public void GetMyRightButtonUp2()
    {
        this.isRBdown2 = false;
    }
    //ジャンプボタンを押し続けた時、トリガーON
    public void GetMyJumpButtonDown2()
    {
        this.isJBdown2 = true;
    }
    //ジャンプボタンを離した時、トリガーOFF
    public void GetMyJumpButtonUp2()
    {
        this.isJBdown2 = false;
    }

    //ゲームオーバー画面へ行く処理
    private void SceneMove()
    {
        SceneManager.LoadScene("Game Over");
    }

}

