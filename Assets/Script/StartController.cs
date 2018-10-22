using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartController : MonoBehaviour {

	// Use this for initialization
	void Start () {
     
        

    }
	
	// Update is called once per frame
	void Update () {
        //点滅して一定時間経過後、このオブジェクトを消す。時間も元に戻す。
        float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
        this.GetComponent<Text>().color = new Color(1f, 1f, 1f, level);

        Invoke("StartMesod", 1.5f);

    }

    private void StartMesod()
    {
        Destroy(gameObject);
    }
}
