using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderEnd : MonoBehaviour
{
    //�Ȃ���timeLimit��1�b�ɂ��Ă��A5�b�o���Ȃ��ƃA�v�����I�����Ȃ����߁A5-3��2�b�o�ƏI������悤�ɂ���B
    private float countup = 3.0f;
    public float timeLimit = 5.0f;
    void OnTriggerStay(Collider other)
    {
        countup += Time.deltaTime;

        // �����ڐG���Ă���i�d�Ȃ��Ă���j����I�u�W�F�N�g�̖��O��"Player"�Ȃ��
        if (other.gameObject.name == "Player")
        {
            Debug.Log(countup);
            if (countup >= timeLimit) {
                Application.runInBackground = false;
                Application.Quit();
            }
        }
    }
    //�ϐ��̎��Ԃ�߂�
}
