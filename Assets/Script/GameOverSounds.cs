using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSounds : MonoBehaviour
{
    AudioSource Audio;//���y���Đ������邽�߂�AudioSource�R���|�[�l���g��Play���\�b�h���g�����߂ɒ�`����
    public void GameOverSound()
    {
        Audio = GetComponent<AudioSource>();//���̃X�N���v�g���A�^�b�`���ꂽ�I�u�W�F�N�g�ɕt�^����Ă���AudioSource�R���|�[�l���g��F��������
        Audio.Play();//�ϐ����\�����y���Đ�����
    }
}
