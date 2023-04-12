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

    // ���݂̃X�e�[�g
    public PlayState CurrentState = PlayState.None;

    // �J�E���g�_�E���X�^�[�g�^�C��
    int countStartTime = 3;

    // �J�E���g�_�E���e�L�X�g
    [SerializeField] Text countdownText;
    // �J�E���g�_�E���̌��ݒl
    float currentCountDown = 3;

    //�G�ꂽ�������A�Ă΂��
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            // �J�E���g�_�E���X�^�[�g�^�C���X�V
            countStartTime = 3;
            currentCountDown = countStartTime;
        }
    }

    //�G��Ă���ԁA�Ă΂��
    void OnTriggerStay(Collider other)
    {
        // �����ڐG���Ă�������I�u�W�F�N�g�̖��O��"Player"�Ȃ��
        if (other.gameObject.name == "Player")
        {
            // �X�e�[�g��Ready�̂Ƃ�
            if (CurrentState == PlayState.Ready)
            {
                // ���Ԃ������Ă���
                currentCountDown -= Time.deltaTime;

                int intNum = 0;
                // �J�E���g�_�E����
                if (currentCountDown <= (float)countStartTime && currentCountDown > 0)
                {
                    // Mathf.Ceil() : �����_�ȉ��̐؂�グ
                    // ToString() : ������֕ϊ�
                    intNum = (int)Mathf.Ceil(currentCountDown);
                    countdownText.text = intNum.ToString();
                }
                else if (currentCountDown <= 0)
                {
                    // �J�n
                    StartPlay();
                    intNum = 0;
                    countdownText.text = "Start!";
                    StageBGM.GetComponent<StageBGM1>().PlayBgm();

                    // Start�\�����������ď���
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

    // �J�E���g�_�E���X�^�[�g
    void CountDownStart()
    {
        currentCountDown = (float)countStartTime;  
        SetPlayState(PlayState.Ready);�@
        countdownText.gameObject.SetActive(true);
    }

    // �Q�[���X�^�[�g
    void StartPlay()
    {
        SetPlayState(PlayState.Play);
    }

    // �����҂��Ă���Start�\��������
    IEnumerator WaitErase()
    {   //�P�b�ҋ@���Ď��̏������s��
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    // ���݂̃X�e�[�g�̐ݒ�
    // <param name="state"> �ݒ肷��X�e�[�g </param>
    void SetPlayState(PlayState state)
    {
        CurrentState = state;
    }

}
