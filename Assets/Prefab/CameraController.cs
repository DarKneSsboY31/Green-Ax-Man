using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //AxManとSpecialManのオブジェクトを取得
    public GameObject AxMan;
    public GameObject SpecialMan;

	// Use this for initialization
	void Start () {

        //Ax　ManとSpecialManのオブジェクトを取得
        AxMan = GameObject.Find("Ax Man");
        SpecialMan = GameObject.Find("Ax Man Special");
    }
	
	// Update is called once per frame
	void Update () {

        //AxManがいるときはカメラはAxManを中心、SpesialManがいるときはカメラをSpecialManを中心にする。
        if (AxMan.activeInHierarchy == true)
        {
            this.transform.position = new Vector3(AxMan.transform.position.x, AxMan.transform.position.y, -10);
        }
        else if (SpecialMan.activeInHierarchy == true)
        {
            this.transform.position = new Vector3(SpecialMan.transform.position.x, SpecialMan.transform.position.y, -10);
        }
	}
}
