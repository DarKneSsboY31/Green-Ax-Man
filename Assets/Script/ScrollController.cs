using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour {

    //進むスピード
    public float Speed = 0.1f;
    //雲が到達したら移動させるライン
    private float CloudLine = -9.0f;
    //タイトルの位置を止めるライン
    private float TitleLine = 2.09f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        //雲を動かす。CloudLineに到達したら、一定の位置まで戻す。
        if (this.gameObject.tag == "Wall")
        {
            this.transform.Translate(-Speed, 0, 0);
            if (this.transform.position.x < CloudLine)
            {
                this.transform.position = new Vector2(10.3f, this.transform.position.y);
            }
        }

        //タイトルを動かす。TitleLineに到達したら、ストップ。
        if (this.gameObject.tag == "Enemy" && this.transform.position.y >= TitleLine)
        {
            this.transform.Translate(0, -Speed /10, 0);
        }
	}
}
