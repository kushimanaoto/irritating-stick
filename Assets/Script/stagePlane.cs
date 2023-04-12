using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stagePlane : MonoBehaviour
{
    StagePrehub StagePrehubscript;
    public GameObject Stage;
    public GameObject Plane2;

    TwonumberPlane TwonumberPlanescript;

    // タイマーテキスト
    [SerializeField] Text timerText = null;
    // ゲーム経過時間現在値
    float timer = 0;

    [SerializeField] GameObject gameOverUI;
    GameObject StageBGM;

    GameObject GameOverSound;
    void Start()
    {
        StagePrehubscript = Stage.GetComponent<StagePrehub>();
        TwonumberPlanescript = Plane2.GetComponent<TwonumberPlane>();
        StageBGM = GameObject.Find("StageBGM");
        GameOverSound = GameObject.Find("GameOverSounds");
    }

    // Update is called once per frame
    void Update()
    {
        if (StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            timer += Time.deltaTime;
            timerText.text = "Time : " + timer.ToString("000.0") + " s";
        }
        else
        {
            return;
        }
    }
    void OnTriggerExit(Collider other)
    {
        // もし離れた相手オブジェクトの名前が"Player"かつPlayStateがPlayかつisTwonumberGameOver == falseなら
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play && TwonumberPlanescript.isTwonumberGameOver == false)
        {
            StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
            if (StagePrehubscript.CurrentState == StagePrehub.PlayState.Finish)
            {
                //PlayStateをFinishにしてゲーム終了。
                StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
                StageBGM.GetComponent<StageBGM1>().BGMStop();
                GameOverSound.GetComponent<GameOverSounds>().GameOverSound();
                gameOverUI.SetActive(true);
            }
        }
    }
}
