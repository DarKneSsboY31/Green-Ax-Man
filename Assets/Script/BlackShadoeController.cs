using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackShadoeController : MonoBehaviour {

    //変身可能までの待機時間、これを元にボタンを隠す時間を測定
    private float TranceTime = 0;
    private float ShadowTime = 0;

    //ボタン隠し中のフラグ
    private int Flag = 0;
    //スペースボタン又は変身ボタンを押したときのフラグ
    private bool isTBdown3 = false;

    // Use this for initialization
    void Start () {

        //初期状態のレイヤー位置を設定
        this.transform.SetSiblingIndex(1);
    }
	
	// Update is called once per frame
	void Update () {
        //時間計測
        TranceTime += Time.deltaTime;
        ShadowTime += Time.deltaTime;

        //TranceTimeが20秒を超えた時、変身ボタンの表示をする
        if (TranceTime > 20)
        {
            this.transform.SetSiblingIndex(0);
        }

        //TranceTimeが20秒を超える時、spaceキー又はボタンを押すとボタンを隠す
        if ((Input.GetKey(KeyCode.Space) || isTBdown3) && TranceTime > 20 )
        {
            TranceTime = 0;
            ShadowTime = 0;
            Flag = 1;
            this.transform.SetSiblingIndex(6);
            isTBdown3 = false;
        }
        else if ((Input.GetKey(KeyCode.Space) || isTBdown3) && TranceTime < 20)
        {
            isTBdown3 = false;
        }

        //隠し中で、８秒経つと、ボタンは元に戻る
        if (Flag == 1 && ShadowTime > 8 )
        {
            TranceTime = 0;
            ShadowTime = 0;
            Flag = 0;
            this.transform.SetSiblingIndex(1);
        }
    }
    //変身ボタンを押した時の処理
    public void GetMyTranceButtonDown3()
    {
        this.isTBdown3 = true;
    }
}
