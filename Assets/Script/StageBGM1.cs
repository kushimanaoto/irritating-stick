using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageBGM1 : MonoBehaviour
{
    AudioSource AudioSource;//���y���Đ������邽�߂�AudioSource�R���|�[�l���g��Play���\�b�h���g�����߂ɒ�`����
    public void PlayBgm()
    {
        AudioSource = GetComponent<AudioSource>();//���̃X�N���v�g���A�^�b�`���ꂽ�I�u�W�F�N�g�ɕt�^����Ă���AudioSource�R���|�[�l���g��F��������
        AudioSource.Play();//�ϐ����\�����y���Đ�����
    }
    public void BGMStop() 
    {
        AudioSource.Stop();
    }
}