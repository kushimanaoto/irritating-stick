using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CylinderStart : MonoBehaviour
{
    //カウントアップ
    private float countup = 0.0f;
    public float timeLimit = 2.0f;
    void OnTriggerStay(Collider other)
    {
        countup += Time.deltaTime;

        // もし接触している（重なっている）相手オブジェクトの名前が"Player"ならば
        if (other.gameObject.name == "Player")
        {
            if (countup >= timeLimit) {
                SceneManager.LoadScene("playScene");
            }
        }
    }
}
