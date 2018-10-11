
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxManController : MonoBehaviour {

    //敵に当たったか判断
    private bool isDmaged = false;
    //無敵時間を設定
    private float DamageTime;
    //死亡ラインの設定
    private float DeadLine = -6.0f;
    //ライフの設定
    public int Life = 3;
    //アニメーション用のコンポーネントを入れる
    private Animator myAnimator;
    //RigidBodyコンポーネントを入れる
    private Rigidbody2D myrigidBody;  
    //進む力
    private float Force = 5.0f;
    //ジャンプする力
    private float Jump = 10;
    //ジャンプが出来る条件
    private int Jumps = 0;
    //各斧のコンポーネントを設定
    public GameObject RedAxPrefab;
    public GameObject BlueAxPrefab;
    public GameObject GreenAxPrefab;
    public GameObject YellowAxPrefab;
    //変身可能までの待機時間
    private float TranceTime = 0;

    // Use this for initialization
    void Start () {

        //Animatorコンポーネントの取得
        this.myAnimator = GetComponent<Animator>();
        //RigidBodyコンポーネントの取得
        this.myrigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Life);
        TranceTime += Time.deltaTime;

        //敵に当たると1.5秒点滅、レイヤー変えて一時無敵にする、時間が経つと元に戻る
        if (isDmaged)
        {
            DamageTime += Time.deltaTime;
            this.gameObject.layer = LayerMask.NameToLayer("AxManDamage");
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            if (DamageTime > 1.5f)
            {
                isDmaged = false;
                this.gameObject.layer = LayerMask.NameToLayer("Default");
                this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                DamageTime = 0;
            }
        }

        //AxManがDeadLineを超えると、死亡
        if (this.transform.position.y < DeadLine)
        {
            Destroy(gameObject);
        }

        //Lifeが1になったら、モアイの姿になる。0なら死亡。2以上なら普通の姿に戻る
        if (Life == 1)
        {
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AxManDefault") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk"))
            {
                this.myAnimator.SetFloat("Life0", 0.9f);
            }
            else if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Default Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump Re"))
            {
                this.myAnimator.SetFloat("Life0", 0.9f);
            }
        }
        else if (Life <= 0)
        {
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage") )
            {
                this.myAnimator.SetFloat("Life0", -0.1f);
                Destroy(gameObject, 0.5f);
            }
            else if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage Re") )
            {
                this.myAnimator.SetFloat("Life0", -0.1f);
                Destroy(gameObject, 0.5f);
            }
        }
        else if (Life >= 2)
        {
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                this.myAnimator.SetFloat("Life0", 2.0f);
            }
            else if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage Re"))
            {
                this.myAnimator.SetFloat("Life0", 2.0f);
            }
        }

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

        //地面に立っている時、上キー又はボタンを押されたらジャンプしアニメーションも起動
        if (Input.GetKey(KeyCode.UpArrow) && Jumps == 1) {
            this.myAnimator.SetBool("Jump", true);
            this.myrigidBody.velocity = new Vector2(0, this.Jump);
        }
        //上キー又はボタンを離したら、ジャンプモーション解除
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            this.myAnimator.SetBool("Jump", false);

        }

        //エンターキー又はボタンが押されたら、斧を生成
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //右向きの時、AxManの右側に生成
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AxManDefault") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                int num = Random.Range(1, 5);
                if (num == 1)
                {
                    //赤い斧
                    GameObject RedAx = Instantiate(RedAxPrefab) as GameObject;
                    RedAx.transform.position = new Vector2(this.transform.position.x + 1.0f, this.transform.position.y);
                }
                else if (num == 2)
                {
                    //青い斧
                    GameObject BlueAx = Instantiate(BlueAxPrefab) as GameObject;
                    BlueAx.transform.position = new Vector2(this.transform.position.x +0.2f, this.transform.position.y +0.7f);
                }
                else if (num == 3)
                {
                    //黄の斧
                    GameObject YellowAx = Instantiate(YellowAxPrefab) as GameObject;
                    YellowAx.transform.position = new Vector2(this.transform.position.x + 1.0f, this.transform.position.y);
                }
                else if (num >= 4)
                {
                    //緑の斧
                    GameObject GreenAx = Instantiate(GreenAxPrefab) as GameObject;
                    GreenAx.transform.position = new Vector2(this.transform.position.x + 1.0f, this.transform.position.y);
                }
            }
            else if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Default Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage Re"))
            {
                //左向きの時、AxManの左側に生成
                int num = Random.Range(1, 5);
                if (num == 1)
                {
                    //赤い斧
                    GameObject RedAx = Instantiate(RedAxPrefab) as GameObject;
                    RedAx.transform.position = new Vector2(this.transform.position.x - 1.0f, this.transform.position.y);
                }
                else if (num == 2)
                {
                    //青い斧
                    GameObject BlueAx = Instantiate(BlueAxPrefab) as GameObject;
                    BlueAx.transform.position = new Vector2(this.transform.position.x - 0.2f, this.transform.position.y +0.7f);
                }
                else if (num == 3)
                {
                    //黄の斧
                    GameObject YellowAx = Instantiate(YellowAxPrefab) as GameObject;
                    YellowAx.transform.position = new Vector2(this.transform.position.x - 1.0f, this.transform.position.y);
                }
                else if (num >= 4)
                {
                    //緑の斧
                    GameObject GreenAx = Instantiate(GreenAxPrefab) as GameObject;
                    GreenAx.transform.position = new Vector2(this.transform.position.x - 1.0f, this.transform.position.y);
                }
            }
        }

        //spaceキー又はボタンを押せば、ライフを3に戻す TranceTimeも戻す
        if (Input.GetKey(KeyCode.Space) && TranceTime >= 20 && Life > 0)
        {
            Life = 3;
            TranceTime = 0;
        }
    }

    //何かに接触し続けた時
    private void OnCollisionStay2D(Collision2D other)
    {
        //地面に触れている時、ジャンプ条件を満たす
        if (other.gameObject.tag == "Ground" || other.gameObject.tag == "Wall")
        {
            Jumps = 1;
        }


    }
    //何かに接触した時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //地面、ブロックに触れている時、ジャンプ条件を満たす
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            Jumps = 1;
        }

        //敵に触れた時
        if (collision.gameObject.tag == "Enemy")
        {
            //攻撃をくらった判定を付け、ライフを1削る
            isDmaged = true;
            Life--;
            //右向きの時、左にのけぞる
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AxManDefault") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                this.myrigidBody.velocity = new Vector2(-this.Force, Jump);
            }

            //左向きの時、右にのけぞる
            else if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Default Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage Re"))
            {
                this.myrigidBody.velocity = new Vector2(this.Force, Jump);
            }
        }
    }

    //何かから離れた時
    private void OnCollisionExit2D(Collision2D collision)
    {
        //地面を離れる時、ジャンプ条件を消す
        Jumps = 0;
    }

}