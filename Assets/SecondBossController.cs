using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBossController : MonoBehaviour {


    //ポーズする画面のオブジェクトを取得
    public GameObject ClearCanvas;
    //死亡フラグ
    private bool isEnd = false;
    //左と右に行く為のフラグ
    private bool RightFlag = false;
    private bool LeftFlag = false;
    //無敵時間
    private float time;
    //ダメージを受けるフラグ
    private bool isDamaged = false;
    //HPの設定
    public int EnemyLife = 3;

    //弾(3種)の設定
    public GameObject AquaBurret;
    public GameObject IceBurret;
    public GameObject IceReBurret;

    // Use this for initialization
    void Start()
    {

        //最初に行動する順に処理をコルーチンで行う。
        StartCoroutine(StartFirst());

    }

    // Update is called once per frame
    void Update()
    {

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        //死亡フラグがONの時、色々光らせる
        if (isEnd)
        {
            time += Time.deltaTime;
            // 点滅の処理をさせる。
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            this.GetComponent<SpriteRenderer>().color = new Color(level, level, level, level);

            //時間が経てば元に戻る
            if (time > 3.0f)
            {
                time = 0;
            }
        }

        //LeftFlagがtrueになれば、左へ動く
        if (LeftFlag && this.transform.position.x > -4)
        {
            time += Time.deltaTime;
            this.transform.Translate(-0.5f, 0, 0);
            //時間が経てば元に戻る
            if (time > 2.0f)
            {
                time = 0;
            }
        }

        //RightFlagがtrueになれば、右へ動く
        if (RightFlag && this.transform.position.x < 3.9f)
        {
            time += Time.deltaTime;
            this.transform.Translate(0.5f, 0, 0);
            //時間が経てば元に戻る
            if (time > 2.0f)
            {
                time = 0;
            }
        }



        //ダメージを受けている時は点滅
        if (isDamaged)
        {
            time += Time.deltaTime;
            // 点滅の処理をさせる。
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, level);

            //時間が経てば元に戻る
            if (time > 4.0f)
            {
                time = 0;
            }
        }

    }

    //ダメージを受けた時のコルーチン
    IEnumerator Damage()
    {
        this.gameObject.layer = LayerMask.NameToLayer("EnemyDamage"); // 無敵状態にさせる

        yield return new WaitForSeconds(4.0f); //4秒待つ

        isDamaged = false;
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);// 元に戻る

        yield return new WaitForSeconds(1.0f); //1秒待つ

        // 分岐が必要
        if (EnemyLife == 3)
        {
            StartCoroutine(StartFirst());//ライフ変動なしなら、このコルーチンを繰り返し
        }
        //Hpが残2の時
        else if (EnemyLife == 2)
        {
            StartCoroutine(StartSecond());//パターン2になる
        }
        //Hpが残1の時
        else if (EnemyLife == 1)
        {
            StartCoroutine(StartThird());//パターン3になる
        }
        //ボス戦終了時
        else if (EnemyLife >= 0)
        {
            StartCoroutine(StartEnd());//負ける
        }

    }

    //ライフが3の時に行う、最初のコルーチン
    IEnumerator StartFirst()
    {
        // 無敵状態にさせる、自分を半透明にしてレイヤーを変える。
        this.gameObject.layer = LayerMask.NameToLayer("EnemyDamage");
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        yield return new WaitForSeconds(0.4f);//0.4秒待つ

        // 「球を出して0.7秒待つ」を３回繰り返す
        for (int i = 0; i < 3; i++)
        {
            GameObject Mizutama = Instantiate(AquaBurret) as GameObject;
            Mizutama.transform.position = new Vector2(this.transform.position.x -3.0f , this.transform.position.y -1.0f );

            yield return new WaitForSeconds(0.7f);//0.7秒待つ

        }

        // レイヤーを戻す。半透明から元に戻る。この隙間に攻撃可能。
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(2.0f);

        // 繰り返しの条件が必要
        if (EnemyLife == 3)
        {
            StartCoroutine(StartFirst());//ライフ変動なしなら、このコルーチンを繰り返し
        }
    }

    //ライフが2の時のコルーチン
    IEnumerator StartSecond()
    {
        // 無敵状態にさせる、自分を半透明にしてレイヤーを変える。
        this.gameObject.layer = LayerMask.NameToLayer("EnemyDamage");
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        // 「球を出して0.5秒待つ」を5回繰り返す
        for (int i = 0; i < 5; i++)
        {
            GameObject Mizutama = Instantiate(AquaBurret) as GameObject;
            Mizutama.transform.position = new Vector2(this.transform.position.x - 3.0f, this.transform.position.y - 1.0f);

            yield return new WaitForSeconds(0.5f);//0.5秒待つ

        }

        // 左に2秒進む
        LeftFlag = true;
        yield return new WaitForSeconds(2.0f);//2秒待つ

        LeftFlag = false;
        // 「球を出して0.5秒待つ」を5回繰り返す
        for (int i = 0; i < 5; i++)
        {
            GameObject Mizutama = Instantiate(AquaBurret) as GameObject;
            Mizutama.transform.position = new Vector2(this.transform.position.x + 3.0f, this.transform.position.y - 1.0f);

            yield return new WaitForSeconds(0.5f);//0.5秒待つ

        }

        //右に2秒進む
        RightFlag = true;
        yield return new WaitForSeconds(2.0f);//2秒待つ

        RightFlag = false;
        // レイヤーを戻す。半透明から元に戻る。この隙間に攻撃可能。
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(2.0f);//2秒待ち

        // 繰り返しの条件が必要、条件が合えばコルーチンを繰り返す
        if (EnemyLife == 2)
        {
            StartCoroutine(StartSecond());
        }
    }

    //ライフが1の時に行動する、最後のあがきのコルーチン
    IEnumerator StartThird()
    {
        // 無敵状態にさせる、自分を半透明にしてレイヤーを変える。
        this.gameObject.layer = LayerMask.NameToLayer("EnemyDamage");
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        //行動を3回繰り返す
        for (int n = 0; n < 3; n++)
        {

            //大きい氷の弾を3つ、一斉に出す。
            GameObject DekaiRe = Instantiate(IceReBurret) as GameObject;
            DekaiRe.transform.position = new Vector2(-3.0f, 2.5f);
            GameObject DekaiReOne = Instantiate(IceReBurret) as GameObject;
            DekaiReOne.transform.position = new Vector2(0, 2.5f);
            GameObject DekaiReO = Instantiate(IceReBurret) as GameObject;
            DekaiReO.transform.position = new Vector2(3.86f, 2.5f);

            // 「球を出して0.3秒待つ」を6回繰り返す
            for (int i = 0; i < 6; i++)
            {
                GameObject Mizutama = Instantiate(AquaBurret) as GameObject;
                Mizutama.transform.position = new Vector2(this.transform.position.x - 3.0f, this.transform.position.y - 1.0f);

                yield return new WaitForSeconds(0.3f);//0.3秒待つ

            }

            // 左に2秒進む
            LeftFlag = true;
            yield return new WaitForSeconds(2.0f);//2秒待つ

            LeftFlag = false;//フラグ消し

            //大きい氷の弾を3つ、一斉に出す。
            GameObject Dekai = Instantiate(IceBurret) as GameObject;
            Dekai.transform.position = new Vector2(-3.0f, 2.5f);
            GameObject DekaiOne = Instantiate(IceBurret) as GameObject;
            DekaiOne.transform.position = new Vector2(0, 2.5f);
            GameObject DekaiO = Instantiate(IceBurret) as GameObject;
            DekaiO.transform.position = new Vector2(3.86f, 2.5f);

            // 「球を出して0.3秒待つ」を6回繰り返す
            for (int i = 0; i < 6; i++)
            {
                GameObject Mizutama = Instantiate(AquaBurret) as GameObject;
                Mizutama.transform.position = new Vector2(this.transform.position.x + 3.0f, this.transform.position.y - 1.0f);

                yield return new WaitForSeconds(0.3f);//0.3秒待つ

            }

            //右に2秒進む
            RightFlag = true;
            yield return new WaitForSeconds(2.0f);//2秒待つ

            RightFlag = false;//フラグ消し

        }

        // レイヤーを戻す。半透明から元に戻る。この隙間に攻撃可能。
        this.gameObject.layer = LayerMask.NameToLayer("Enemy");
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

        yield return new WaitForSeconds(2.0f);//2秒待ち

        // 繰り返しの条件が必要、条件が合えばコルーチンを繰り返す
        if (EnemyLife == 1)
        {
            StartCoroutine(StartSecond());
        }

    }

    //EnemyLifeが0になった時に行う、散り際
    IEnumerator StartEnd()
    {
        isEnd = true;//死亡フラグON

        yield return new WaitForSeconds(3.0f);//3秒待ち

        isEnd = false;//フラグを消す
        ClearCanvas.SetActive(true);//クリア画面出す

        yield return new WaitForSeconds(1.0f);//1秒待ち


        this.gameObject.SetActive(false);//姿を消す
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "SpecialMan" || other.gameObject.tag == "Ax" || other.gameObject.tag == "Green" || other.gameObject.tag == "Red")
        {
            isDamaged = true;
            EnemyLife--;
            StopCoroutine(StartFirst());
            StopCoroutine(StartSecond());
            StopCoroutine(StartThird());
            StartCoroutine(Damage());

        }

    }
}