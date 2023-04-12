using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverSounds : MonoBehaviour
{
    AudioSource Audio;//音楽を再生させるためにAudioSourceコンポーネントのPlayメソッドを使うために定義する
    public void GameOverSound()
    {
        Audio = GetComponent<AudioSource>();//このスクリプトがアタッチされたオブジェクトに付与されているAudioSourceコンポーネントを認識させる
        Audio.Play();//変数が表す音楽を再生する
    }
}
