using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StageBGM1 : MonoBehaviour
{
    AudioSource AudioSource;//音楽を再生させるためにAudioSourceコンポーネントのPlayメソッドを使うために定義する
    public void PlayBgm()
    {
        AudioSource = GetComponent<AudioSource>();//このスクリプトがアタッチされたオブジェクトに付与されているAudioSourceコンポーネントを認識させる
        AudioSource.Play();//変数が表す音楽を再生する
    }
    public void BGMStop() 
    {
        AudioSource.Stop();
    }
}