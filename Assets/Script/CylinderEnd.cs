using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderEnd : MonoBehaviour
{
    //なぜかtimeLimitを1秒にしても、5秒経たないとアプリが終了しないため、5-3で2秒経つと終了するようにする。
    private float countup = 3.0f;
    public float timeLimit = 5.0f;
    void OnTriggerStay(Collider other)
    {
        countup += Time.deltaTime;

        // もし接触している（重なっている）相手オブジェクトの名前が"Player"ならば
        if (other.gameObject.name == "Player")
        {
            Debug.Log(countup);
            if (countup >= timeLimit) {
                Application.runInBackground = false;
                Application.Quit();
            }
        }
    }
    //変数の時間を戻す
}
