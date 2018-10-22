using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

    //ステージクリア時、タイトルに戻るフラグ
    private bool isClear = false;
    //ポーズボタンを押したときのフラグ
    private bool isPauseBD = false;

    //AxManとSpecialManのオブジェクトを取得
    public GameObject AxMan;
    public GameObject SpecialMan;

    //ポーズする画面のオブジェクトを取得
    public GameObject PauseUI;

    //映さないラインの設定
    public float DeadLine = -3.0f;
    public float DeathLine = -4.0f;

    // Use this for initialization
    void Start () {

        //Ax　ManとSpecialManのオブジェクトを取得
        AxMan = GameObject.Find("Ax Man");
        SpecialMan = GameObject.Find("Ax Man Special");
    }
	
	// Update is called once per frame
	void Update () {

        //クリアフラグがONの時、シーン移動
        if (isClear)
        {
            SceneManager.LoadScene("Title");
            isClear = false;
        }

        //AxManがいるときはカメラはAxManを中心、SpesialManがいるときはカメラをSpecialManを中心にする。特定の下の位置まで行くと動かない
        if (AxMan.activeInHierarchy == true)
        {
            if (AxMan.transform.position.y > DeadLine && AxMan.transform.position.x > DeathLine)
            {
                this.transform.position = new Vector3(AxMan.transform.position.x, AxMan.transform.position.y, -10);
            }
        }
        else if (SpecialMan.activeInHierarchy == true)
        {
            if (SpecialMan.transform.position.y > DeadLine && SpecialMan.transform.position.x > DeathLine)
            {
                this.transform.position = new Vector3(SpecialMan.transform.position.x, SpecialMan.transform.position.y- 1, -10);
            }
        }

        //下キーを押すか、Pauseボタンを押せば、ポーズ画面を出して、止まる。
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PauseUI.SetActive(!PauseUI.activeInHierarchy);
            if (PauseUI.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                isPauseBD = false;
            }
        }

        //ポーズボタンを押した後、画面内のボタンをタッチすれば、元に戻る
        if (isPauseBD)
        {
            PauseUI.SetActive(!PauseUI.activeInHierarchy);
            isPauseBD = false;
            if (PauseUI.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                isPauseBD = false;
            }
        }
    }

    //ポーズボタンが押された時、トリガーON
    public void GetMyPauseButtonDown()
    {
        isPauseBD = true;
    }

    public void GetClearButtonDown()
    {
        isClear = true;
    }
}
