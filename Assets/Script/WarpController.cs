using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpController : MonoBehaviour {

    //SEの設定
    private AudioSource audiosource;
    public AudioClip Magic; //移動するときの時のSE

    //数字を設定。これによって移動する位置を変える
    public int Number;
    //AxManを取得
    private GameObject AxMan;
    //SpecialManのオブジェクトを取得
    private GameObject SpecialMan;

    // Use this for initialization
    void Start () {
        //AudioSourceコンポーネントを取得
        audiosource = GetComponent<AudioSource>();

        //Ax　ManとSpecialManのオブジェクトを取得
        AxMan = GameObject.Find("Ax Man");
        SpecialMan = GameObject.Find("Ax Man Special");

    }
	
	// Update is called once per frame
	void Update () {
  
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //AxManに当たったら、移動
        //設定された数字によって、AxManを移動させてしまう。
        if (collision.gameObject.tag == "AxMan" || collision.gameObject.tag == "SpecialMan" )
        {
            if (Number == 1)
            {
                //音を鳴らす
                audiosource.PlayOneShot(Magic, 2.0f);
                AxMan.transform.position = new Vector2(-5.35f, 3.94f);
                SpecialMan.transform.position = new Vector2(-5.35f, 3.94f);
            }
            else if (Number == 2)
            {
                //音を鳴らす
                audiosource.PlayOneShot(Magic, 1.0f);
                AxMan.transform.position = new Vector2(20.0f, 55.0f);
                SpecialMan.transform.position = new Vector2(20.0f, 55.0f);
            }
        }
    }
}
