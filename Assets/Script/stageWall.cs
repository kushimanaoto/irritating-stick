using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class stageWall : MonoBehaviour
{
    StagePrehub StagePrehubscript;
    public GameObject Stage;
    [SerializeField] GameObject gameOverUI;
    GameObject GameOverSound;
    GameObject StageBGM;
    void Start()
    {
        StagePrehubscript = Stage.GetComponent<StagePrehub>();
        GameOverSound = GameObject.Find("GameOverSounds");
        StageBGM = GameObject.Find("StageBGM");
    }
    
    void OnTriggerStay(Collider other)
    {
        // もし接触している相手オブジェクトの名前が"Player"かつPlayStateがPlayならば
        if (other.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            //PlayStateをFinishにしてゲーム終了。
            StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
            if (StagePrehubscript.CurrentState == StagePrehub.PlayState.Finish)
            {
                StageBGM.GetComponent<StageBGM1>().BGMStop();
                GameOverSound.GetComponent<GameOverSounds>().GameOverSound();
                gameOverUI.SetActive(true);
            }
        }
    }
}