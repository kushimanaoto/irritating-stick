using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))] //typeofとは？型のチェックまたは型の変換ができる。ここではそういうものとして考える。

public class stageDisplay : MonoBehaviour
{   
    //ARRaycastManagerを使用しますと宣言。raycastManagerは変数。
    ARRaycastManager raycast;

    //ARRaycastHitは、@レイキャストのx情報を持ってる@構造体型で値型
    //ヒット情報を格納するリスト
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    bool isCalledOnce = false;

    //[SerializeField]シリアライゼーションするとUnity上で値をいじれるようになる。
    [SerializeField] private ARPlaneManager _arPlaneManager;

    [SerializeField] GameObject Stage1;
    void Start()
    {
        raycast = GetComponent<ARRaycastManager>();
    }

    void Update()
    {

        //ユーザーがボタンを押していない状態から押した時だけtrueを返す。
        if (Input.GetMouseButtonDown(0)) //なんで(0)？引数は0・1・2の3通りで、値が小さい順に左クリック・右クリック・中クリックに対応します。スマホでも0が反応する。
        {
            if (raycast.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.Planes))
            {   // 3Dオブジェクトの生成 Quaternion.identityは回転していないという意味
                if (!isCalledOnce)
                {
                    isCalledOnce = true; //一回だけ動作させるため
                    //GameObject Stage = (GameObject)Resources.Load("prefab/Stage");
                    GameObject Stage = Stage1;
                    Stage1.SetActive(true);
                    Instantiate(Stage, new Vector3(hitResults[0].pose.position.x, hitResults[0].pose.position.y + 1.8f, hitResults[0].pose.position.z + 1.0f), Quaternion.Euler(270, 0, 0));
                    //Instantiate(obj, transform.position, Quaternion.identity);
                    

                    //生成したARPlaneをオフにする。ARPlaneManagerのtrackablesからARPlaneを取り出して全て非表示にする。foreachはコレクションのすべての要素を１つ１つ取得する。
                    foreach (ARPlane plane in _arPlaneManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    //平面認識の機能をオフ
                    _arPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
                }
            }
        }
    }
}