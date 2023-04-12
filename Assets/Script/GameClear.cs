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

    AudioSource Audio;//���y���Đ������邽�߂�AudioSource�R���|�[�l���g��Play���\�b�h���g�����߂ɒ�`����

    TwonumberPlane TwonumberPlanescript;
    public void GameClearSound()
    {
        Audio = GetComponent<AudioSource>();//���̃X�N���v�g���A�^�b�`���ꂽ�I�u�W�F�N�g�ɕt�^����Ă���AudioSource�R���|�[�l���g��F��������
        Audio.Play();//�ϐ����\�����y���Đ�����
    }
    void Start()
    {
        StagePrehubscript = Stage.GetComponent<StagePrehub>();

        StageBGM = GameObject.Find("StageBGM");

        TwonumberPlanescript = Plane2.GetComponent<TwonumberPlane>();
    }

    void OnTriggerStay(Collider other)
    {
        // �����ڐG���Ă��鑊��I�u�W�F�N�g�̖��O��"Player"����PlayState��Play����isTwonumberGameOver��true�Ȃ��
        if (other.name == "Player" && StagePrehubscript.CurrentState == StagePrehub.PlayState.Play && TwonumberPlanescript.isTwonumberGameOver == true)
        {
            //PlayState��Finish�ɂ��ăQ�[���I���B
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