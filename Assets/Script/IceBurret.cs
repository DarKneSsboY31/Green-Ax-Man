using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBurret : MonoBehaviour {

    //SEの設定
    private AudioSource audiosource;
    public AudioClip Damage; //地面に当たった時のSE

    // Use this for initialization
    void Start () {
        //AudioSourceコンポーネントを取得
        audiosource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {

        //ポーズしている間は、動かさない
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //地面に触れた時、削除
        if (collision.gameObject.tag == "Ground")
        {
            //音を鳴らす
            audiosource.PlayOneShot(Damage, 0.1f);
            Invoke("Death", 0.05f);
        }   
    }
    //消滅する時の関数
    private void Death()
    {

        Destroy(gameObject);
    }
}
