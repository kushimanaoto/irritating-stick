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
        // �����ڐG���Ă��鑊��I�u�W�F�N�g�̖��O��"Player"����PlayState��Play�Ȃ��
        if (other.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            //PlayState��Finish�ɂ��ăQ�[���I���B
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