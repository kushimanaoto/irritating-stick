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

    // �^�C�}�[�e�L�X�g
    [SerializeField] Text timerText = null;
    // �Q�[���o�ߎ��Ԍ��ݒl
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
        // �������ꂽ����I�u�W�F�N�g�̖��O��"Player"����PlayState��Play����isTwonumberGameOver == false�Ȃ�
        if (other.gameObject.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play && TwonumberPlanescript.isTwonumberGameOver == false)
        {
            StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
            if (StagePrehubscript.CurrentState == StagePrehub.PlayState.Finish)
            {
                //PlayState��Finish�ɂ��ăQ�[���I���B
                StagePrehubscript.CurrentState = StagePrehub.PlayState.Finish;
                StageBGM.GetComponent<StageBGM1>().BGMStop();
                GameOverSound.GetComponent<GameOverSounds>().GameOverSound();
                gameOverUI.SetActive(true);
            }
        }
    }
}
