using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameClear : MonoBehaviour
{
    StagePrehub StagePrehubscript;
    public GameObject Stage;
    public GameObject Plane2;
    [SerializeField]
    GameObject gameClearUI;

    GameObject StageBGM;

    AudioSource Audio;//音楽を再生させるためにAudioSourceコンポーネントのPlayメソッドを使うために定義する

    TwonumberPlane TwonumberPlanescript;
    public void GameClearSound()
    {
        Audio = GetComponent<AudioSource>();//このスクリプトがアタッチされたオブジェクトに付与されているAudioSourceコンポーネントを認識させる
        Audio.Play();//変数が表す音楽を再生する
    }
    void Start()
    {
        StagePrehubscript = Stage.GetComponent<StagePrehub>();

        StageBGM = GameObject.Find("StageBGM");

        TwonumberPlanescript = Plane2.GetComponent<TwonumberPlane>();
    }

    void OnTriggerStay(Collider other)
    {
        // もし接触している相手オブジェクトの名前が"Player"かつPlayStateがPlayかつisTwonumberGameOverがtrueならば
        if (other.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play && TwonumberPlanescript.isTwonumberGameOver == true)
        {
            //PlayStateをFinishにしてゲーム終了。
            StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
            StageBGM.GetComponent<StageBGM1>().BGMStop();

            if (StagePrehubscript.CurrentState == StagePrehub.PlayState.Finish)
            {
                GameClearSound();
                gameClearUI.SetActive(true);
            }

        }
    }
}