using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CylinderStart : MonoBehaviour
{
    //�J�E���g�A�b�v
    private float countup = 0.0f;
    public float timeLimit = 2.0f;
    void OnTriggerStay(Collider other)
    {
        countup += Time.deltaTime;

        // �����ڐG���Ă���i�d�Ȃ��Ă���j����I�u�W�F�N�g�̖��O��"Player"�Ȃ��
        if (other.gameObject.name == "Player")
        {
            if (countup >= timeLimit) {
                SceneManager.LoadScene("playScene");
            }
        }
    }
}
