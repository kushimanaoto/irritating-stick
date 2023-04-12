using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreenumberPlane : MonoBehaviour
{
    public bool isThreenumberGameOver = false;

    [SerializeField] GameObject gameOverUI;

    StagePrehub StagePrehubscript;
    public GameObject Stage;

    GameObject StageBGM;

    GameObject GameOverSound;

    void Start()
    {
        StagePrehubscript = Stage.GetComponent<StagePrehub>();

        StageBGM = GameObject.Find("StageBGM");
        GameOverSound = GameObject.Find("GameOverSounds");
    }

    void OnTriggerEnter(Collider other)
    {
        // もし触れた相手オブジェクトの名前が"Player"かつPlayStateがPlayなら
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            isThreenumberGameOver = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        // もし離れた相手オブジェクトの名前が"Player"かつPlayStateがPlayかつisGameOver == falseなら
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            //PlayStateをFinishにしてゲーム終了。
            StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
            StageBGM.GetComponent<StageBGM1>().BGMStop();
            GameOverSound.GetComponent<GameOverSounds>().GameOverSound();
            gameOverUI.SetActive(true);
        }
    }
}
