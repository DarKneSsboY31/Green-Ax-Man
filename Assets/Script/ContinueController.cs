using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueController : MonoBehaviour {

    //コンティニューと、ゲームオーバーのフラグ、各ステージへ行くフラグを立てる
    private bool ContinueFlag = false;
    private bool GameoverFlag = false;
    private bool FirstStageFlag = false;
    private bool LastStageFlag = false;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        //ゲームオーバー画面にて使用。AxManがどのシーンにいるか判定する引数（AxManControllerから引用）
        int Number = AxManController.Scene;
        
        //コンティニューのフラグが立てば、Numberを参照して各ステージへ戻る
        if (ContinueFlag)
        {
            ContinueFlag = false;
            if (Number == 1)
            {
                SceneManager.LoadScene("Stage1");
            }
            else if (Number == 2)
            {
                SceneManager.LoadScene("Stage1-Boss");
            }
            else if (Number == 3)
            {
                SceneManager.LoadScene("Stage2");
            }
            else if (Number == 4)
            {
                SceneManager.LoadScene("Stage2-Boss");
            }
          
        }
        //ゲームオーバーのフラグが立てば、タイトルへ戻る
        else if (GameoverFlag)
        {
            GameoverFlag = false;
            SceneManager.LoadScene("Title");
        }
        //ここからはタイトル画面用。1stステージ行きのフラグが立てば、1stステージへ。
        else if (FirstStageFlag)
        {
            FirstStageFlag = false;
            SceneManager.LoadScene("Stage1");
        }
        //FinalStageへのフラグが立てば、FinalStageへ。(未完成の為、1stステージへ行く設定)
        else if (LastStageFlag)
        {
           LastStageFlag = false;
            SceneManager.LoadScene("Stage2");
        }

    }

    public void ContineBdown() {

        //コンティニューボタンが押されたら、トリガーON
        this.ContinueFlag = true;

    }

    public void GameoverBdown() {

        //GiveUpボタンが押されたら、トリガーON
        this.GameoverFlag = true;
    }

    public void FirstBdown() {

        //1stステージボタンが押されたら、トリガーON
        this.FirstStageFlag = true;
    }

    public void LastBdown()
    {

        //Finalステージボタンが押されたら、トリガーON
        this.LastStageFlag = true;
    }


}
