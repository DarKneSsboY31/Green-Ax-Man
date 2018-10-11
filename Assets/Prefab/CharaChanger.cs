using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaChanger : MonoBehaviour {

    //AxManとSpecialManのオブジェクトを取得
    public GameObject AxMan;
    public GameObject SpecialMan;
    //変身している時間
    private float JudgementTiMe = 0;
    //変身可能までの待機時間
    private float TranceTime = 0;


	// Use this for initialization
	void Start () {
        //Ax　ManとSpecialManのオブジェクトを取得
        AxMan = GameObject.Find("Ax Man");
        SpecialMan = GameObject.Find("Ax Man Special");

        //SpecialManを見えなくする
        SpecialMan.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        //時間を、設定
        TranceTime += Time.deltaTime;
        JudgementTiMe += Time.deltaTime;

        //TranceTimeが20秒を超える時、spaceキー又はボタンを押すとSpecialManを出現させ、AxManを見えなくする。JudgementTimeが8秒を超えるまで暴れさせる
        if (Input.GetKey(KeyCode.Space) && TranceTime > 20 && AxMan.activeInHierarchy == true)
        {

            JudgementTiMe = 0;
            TranceTime = 0;
            SpecialMan.SetActive(true);
            SpecialMan.transform.position = new Vector2(AxMan.transform.position.x, AxMan.transform.position.y + 1.0f);
            AxMan.SetActive(false);

        }
        else if (Input.GetKey(KeyCode.Space) && TranceTime < 20)
        {
            Debug.Log("まだ早い");
        }
        
        //JudgementTiMeが8秒を超えたら、変身を解除。
        if (AxMan.activeInHierarchy == false && JudgementTiMe > 8 && SpecialMan.activeInHierarchy == true)
        {
            TranceTime = 0;
            JudgementTiMe = 0;
            AxMan.SetActive(true);
            AxMan.transform.position = new Vector2(SpecialMan.transform.position.x, SpecialMan.transform.position.y);
            SpecialMan.SetActive(false);
        }

    }
}
