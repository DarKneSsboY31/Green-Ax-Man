using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBurret : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
            Destroy(gameObject);
        }
      
    }
}
