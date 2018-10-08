using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxControllers : MonoBehaviour
{

    //進むスピード
    private float speed = 3.0f;
    //Ax Manのアニメーションを取得
    public Animator AxMans;
    //AxManを取得
    public GameObject AxMan;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //AxManが右向きの時、右へ進ませる,左の時は左へ進ませる
        if (AxMans.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Ax Man Default"))
        {
            this.transform.Translate(speed,0,0);
            Debug.Log("aaa");
        }
        else if (AxMans.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk Re") || AxMans.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Default Re"))
        {
            this.transform.Translate(-speed, 0, 0);
            Debug.Log("bbb");
        }

        //一定の距離を超えたら、消す
        if (this.transform.position.x > AxMan.transform.position.x + 5)
        {
            Destroy(gameObject);
            Debug.Log("ccc");
        }
        else if (this.transform.position.x < AxMan.transform.position.x - 5)
        {
            Destroy(gameObject);
            Debug.Log("ddd");
        }

    }
}
