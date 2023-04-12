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
        // �����G�ꂽ����I�u�W�F�N�g�̖��O��"Player"����PlayState��Play�Ȃ�
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            isThreenumberGameOver = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        // �������ꂽ����I�u�W�F�N�g�̖��O��"Player"����PlayState��Play����isGameOver == false�Ȃ�
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            //PlayState��Finish�ɂ��ăQ�[���I���B
            StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
            StageBGM.GetComponent<StageBGM1>().BGMStop();
            GameOverSound.GetComponent<GameOverSounds>().GameOverSound();
            gameOverUI.SetActive(true);
        }
    }
}
