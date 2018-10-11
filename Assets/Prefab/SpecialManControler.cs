using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start () {
       
        //RigidBodyコンポーネントの取得
        this.myrigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        //SpecialManがDeadLineを超えると、死亡
        if (this.transform.position.y < DeadLine)
        {
            Destroy(gameObject);
        }

        //矢印キーまたは各ボタンで左右に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //左へ移動
            this.myrigidBody.velocity = new Vector2(-this.Force, this.myrigidBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //右へ移動
            this.myrigidBody.velocity = new Vector2(this.Force, this.myrigidBody.velocity.y);

        }
        
        //地面に立っている時、上キー又はボタンを押されたらジャンプ
        if (Input.GetKey(KeyCode.UpArrow) && Jumps == 1)
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

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //地面を離れる時、ジャンプ条件を消す
        Jumps = 0;
    }
}

