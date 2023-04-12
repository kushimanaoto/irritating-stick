using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePrehub : MonoBehaviour
{
    GameObject StageBGM;
    
    public enum PlayState
    {
        None,
        Ready,
        Play,
        Finish,
    }

    // 現在のステート
    public PlayState CurrentState = PlayState.None;

    // カウントダウンスタートタイム
    int countStartTime = 3;

    // カウントダウンテキスト
    [SerializeField] Text countdownText;
    // カウントダウンの現在値
    float currentCountDown = 3;

    //触れた時だけ、呼ばれる
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            // カウントダウンスタートタイム更新
            countStartTime = 3;
            currentCountDown = countStartTime;
        }
    }

    //触れている間、呼ばれる
    void OnTriggerStay(Collider other)
    {
        // もし接触していた相手オブジェクトの名前が"Player"ならば
        if (other.gameObject.name == "Player")
        {
            // ステートがReadyのとき
            if (CurrentState == PlayState.Ready)
            {
                // 時間を引いていく
                currentCountDown -= Time.deltaTime;

                int intNum = 0;
                // カウントダウン中
                if (currentCountDown <= (float)countStartTime && currentCountDown > 0)
                {
                    // Mathf.Ceil() : 小数点以下の切り上げ
                    // ToString() : 文字列へ変換
                    intNum = (int)Mathf.Ceil(currentCountDown);
                    countdownText.text = intNum.ToString();
                }
                else if (currentCountDown <= 0)
                {
                    // 開始
                    StartPlay();
                    intNum = 0;
                    countdownText.text = "Start!";
                    StageBGM.GetComponent<StageBGM1>().PlayBgm();

                    // Start表示を少しして消す
                    StartCoroutine(WaitErase());
                }
            }
        }
    }

    void Start()
    {
        StageBGM = GameObject.Find("StageBGM");
        CountDownStart();
    }

    // カウントダウンスタート
    void CountDownStart()
    {
        currentCountDown = (float)countStartTime;  
        SetPlayState(PlayState.Ready);　
        countdownText.gameObject.SetActive(true);
    }

    // ゲームスタート
    void StartPlay()
    {
        SetPlayState(PlayState.Play);
    }

    // 少し待ってからStart表示を消す
    IEnumerator WaitErase()
    {   //１秒待機して次の処理を行う
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    // 現在のステートの設定
    // <param name="state"> 設定するステート </param>
    void SetPlayState(PlayState state)
    {
        CurrentState = state;
    }

}
