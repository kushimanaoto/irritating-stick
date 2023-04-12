using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwonumberPlane : MonoBehaviour
{
    public bool isTwonumberGameOver = false;

    [SerializeField] GameObject gameOverUI;
    ThreenumberPlane ThreenumberPlanescript;
    public GameObject Plane3;
    StagePrehub StagePrehubscript;
    public GameObject Stage;

    GameObject StageBGM;

    GameObject GameOverSound;
    // Start is called before the first frame update
    void Start()
    {
        StagePrehubscript = Stage.GetComponent<StagePrehub>();
        ThreenumberPlanescript = Plane3.GetComponent<ThreenumberPlane>();
        StageBGM = GameObject.Find("StageBGM");
        GameOverSound = GameObject.Find("GameOverSounds");
    }

    void OnTriggerEnter(Collider other)
    {
        // �����G�ꂽ����I�u�W�F�N�g�̖��O��"Player"����PlayState��Play�Ȃ�
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play)
        {
            isTwonumberGameOver = true;
        }

    }
    // Update is called once per frame
     void OnTriggerExit(Collider other)
    {
        // �������ꂽ����I�u�W�F�N�g�̖��O��"Player"����PlayState��Play����isGameOver == false�Ȃ�
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play && ThreenumberPlanescript.isThreenumberGameOver == false)
        {
            //PlayState��Finish�ɂ��ăQ�[���I���B
            StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
            StageBGM.GetComponent<StageBGM1>().BGMStop();
            GameOverSound.GetComponent<GameOverSounds>().GameOverSound();
            gameOverUI.SetActive(true);
        }
    }

}
