
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AxManController : MonoBehaviour
{

    //AxManがどのシーンにいるか、判定。これを使って、コンティニュー先を決める。何か起きた時にAxManの動きを止める
    public static int Scene;
    //斧を投げた後のインターバル
    private float interval = 0.2f;
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

    //左、右、ジャンプ、攻撃、変身のボタンをそれぞれ押した時のトリガー
    private bool isLBdown = false;
    private bool isRBdown = false;
    private bool isJBdown = false;
    private bool isABdown = false;
    private bool isTBdown = false;

    // Use this for initialization
    void Start()
    {
        //AxManがどのシーンにいるか判定する。
        if (SceneManager.GetActiveScene().name == "Stage1")
        {
            Scene = 1;
        }
        else if (SceneManager.GetActiveScene().name == "Stage1-Boss")
        {
            Scene = 2;
        }
        else if (SceneManager.GetActiveScene().name == "Stage2")
        {
            Scene = 3;
        }//２面ボスが作れたら、この先を作成


        //Animatorコンポーネントの取得
        this.myAnimator = GetComponent<Animator>();
        //RigidBodyコンポーネントの取得
        this.myrigidBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update() 
    {
       
        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

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

        //AxManがDeadLineを超えると、死亡、シーン移動
        if (this.transform.position.y < DeadLine)
        {
            Life = 0;
            Invoke("SceneMove", 1.0f);
        }

        //Lifeが1になったら、モアイの姿になる。0なら死亡、シーン移動。2以上なら普通の姿に戻る
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
            if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
            {
                this.myAnimator.SetFloat("Life0", -0.1f);
                Invoke("SceneMove", 0.2f);
            }
            else if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage Re"))
            {
                this.myAnimator.SetFloat("Life0", -0.1f);
                Invoke("SceneMove", 0.2f);
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
        if (Input.GetKey(KeyCode.LeftArrow) || isLBdown)
        {
            //左へ移動、左移動のアニメーションを起動、右移動のステートを消す
            this.myAnimator.SetBool("Left", true);
            this.myrigidBody.velocity = new Vector2(-this.Force, this.myrigidBody.velocity.y);
            this.myAnimator.SetBool("Right", false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || !isLBdown)
        {
            //左キー又はボタンが離されたら、左アニメーションを変更
            this.myAnimator.SetBool("Left", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || isRBdown)
        {
            //右へ移動、右移動のアニメーションを起動、左移動のステートを消す
            this.myAnimator.SetBool("Right", true);
            this.myrigidBody.velocity = new Vector2(this.Force, this.myrigidBody.velocity.y);
            this.myAnimator.SetBool("Left", false);

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || !isRBdown)
        {
            //右キー又はボタンが離されたら、右アニメーションを変更
            this.myAnimator.SetBool("Right", false);
        }

        //地面に立っている時、上キー又はボタンを押されたらジャンプしアニメーションも起動
        if ((Input.GetKey(KeyCode.UpArrow) || isJBdown) && Jumps == 1)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myrigidBody.velocity = new Vector2(0, this.Jump);
        }
        //上キー又はボタンを離したら、ジャンプモーション解除
        else if (Input.GetKeyUp(KeyCode.UpArrow) || !isJBdown)
        {
            this.myAnimator.SetBool("Jump", false);

        }

        //エンターキー又はボタンが押されたら、斧を生成
        if (Input.GetKey(KeyCode.KeypadEnter) || isABdown)
        {
            interval -= Time.deltaTime;
            //斧を0.1秒ごとに生成
            //右向きの時、AxManの右側に生成
            if (interval > 0.18f && (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.AxManDefault") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage")))
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
                    BlueAx.transform.position = new Vector2(this.transform.position.x + 0.2f, this.transform.position.y + 0.7f);
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
            else if (interval > 0.18f && (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Default Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump Re") || this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Damage Re")))
            {
                //斧を0.3秒ごとに生成
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
                    BlueAx.transform.position = new Vector2(this.transform.position.x - 0.2f, this.transform.position.y + 0.7f);
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
            //intervalを元に戻す処理
            if (interval < 0)
            {
                interval = 0.2f;
            }
        }
        else if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            //intervalを元に戻す処理
            interval = 0.3f;
        }
    

        //spaceキー又はボタンを押せば、ライフを3に戻す TranceTimeも戻す
        if ((Input.GetKey(KeyCode.Space) || isTBdown) && TranceTime >= 20 && Life > 0)
        {
            Life = 3;
            TranceTime = 0;
            isTBdown = false;
        }
    }

    //何かに接触し続けた時
    private void OnCollisionStay2D(Collision2D other)
    {
        //地面に触れている時、ジャンプ条件を満たす
        if (other.gameObject.tag == "Ground")
        {
            Jumps = 1;
        }


    }
    //何かに接触した時
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //地面に触れている時、ジャンプ条件を満たす
        if (collision.gameObject.tag == "Ground" )
        {
            Jumps = 1;
        }

        //敵に触れた時
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Strong Enemy" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Strong Boss" || collision.gameObject.tag == "Last Boss")
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

        //ゴールオブジェクトに当たった時、次のステージへ
        if (collision.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Stage1-Boss");
        }
    }

    //何かから離れた時
    private void OnCollisionExit2D(Collision2D collision)
    {
        //地面を離れる時、ジャンプ条件を消す
        Jumps = 0;
    }

    //左ボタンを押し続けた時、トリガーON
    public void GetMyLeftBottunDown()
    {
        this.isLBdown = true;
    }
    //左ボタンを離した時、トリガーOFF
    public void GetMyLeftBottunUp()
    {
        this.isLBdown = false;
    }
    //右ボタンを押し続けた時、トリガーON
    public void GetMyRightButtonDown()
    {
        this.isRBdown = true;
    }
    //右ボタンを離した時、トリガーOFF
    public void GetMyRightButtonUp()
    {
        this.isRBdown = false;
    }
    //ジャンプボタンを押し続けた時、トリガーON
    public void GetMyJumpButtonDown()
    {
        this.isJBdown = true;
    }
    //ジャンプボタンを離した時、トリガーOFF
    public void GetMyJumpButtonUp()
    {
        this.isJBdown = false;
    }

    //攻撃ボタンを押した時、トリガーON
    public void GetmyAttackButtonDown()
    {
        this.isABdown = true;                      
    }

    //攻撃ボタンを離した時、トリガーOFF
    public void GetmyAttackButtonUp()
    {
        this.isABdown = false;
        interval = 0.3f;
    }
    //変身ボタンを押した時の処理
    public void GetMyTranceButtonDown()
    {
        isTBdown = true;    
    }

    //ゲームオーバー画面へ行く
    private void SceneMove()
    {
        SceneManager.LoadScene("Game Over");
    }
}