using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//[RequireComponent]にRequireComponentでARRaycastManagerを指定すると自動的にARRaycastManagerもInspecterにセットされる。
//[ARRaycastManager]タップした場所にモデルを配置するため、ARRaycastManagerクラスの@Raycastメソッドを使用している。
[RequireComponent(typeof(ARRaycastManager))] //typeofとは？型のチェックまたは型の変換ができる。ここではそういうものとして考える。

/*Start,UpdateメソッドもUnityのシステムから呼び出されるイベント関数と呼ばれるMonobehaviourクラスのメソッド
ただのロジックを記載するだけのクラスなどは継承させる必要はない*/
public class ARTapToPlaceObject : MonoBehaviour
{   //[SerializeField]シリアライゼーションするとUnity上で値をいじれるようになる。
    [SerializeField]GameObject StartPlane;
    [SerializeField]GameObject EndPlane;
    //ARRaycastManagerを使用しますと宣言。raycastManagerは変数。
    ARRaycastManager raycastManager;

    //ARRaycastHitは、@レイキャストのx情報を持ってる@構造体型で値型
    //ヒット情報を格納するリスト
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    private ARPlaneManager _arPlaneManager;
    void Start()
    {   //ARRaycastManagerのclassのコンポーネントを取ってくる。classだから<hoge>();
        raycastManager = GetComponent<ARRaycastManager>();
        
    }

    void Update()
    {

        //ユーザーがボタンを押していない状態から押した時だけtrueを返す。
        if (Input.GetMouseButtonDown(0)) //なんで(0)？引数は0・1・2の3通りで、値が小さい順に左クリック・右クリック・中クリックに対応します。スマホでも反応する。
        {
            //Raycastメソッドとは画面をタップすると(ray発射位置, どこに保存する, 当たる面の対象)
            //TrackableType.Allは全てのタイプを検出。（形状に沿った平面やすべての種類の平面などだけを検知することもできる）
            //if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.All))
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.Planes))
            {   // 3Dオブジェクトの生成 
                //Instantiate(コピーしたい既存オブジェクト、位置、向き)
                Instantiate(StartPlane, new Vector3(hitResults[0].pose.position.x - 0.3f, hitResults[0].pose.position.y + 0.7f, hitResults[0].pose.position.z), Quaternion.AngleAxis(270.0f, new Vector3(1.0f, 0.0f, 0.0f)));//yはいじっても意味ない
                Instantiate(EndPlane, new Vector3(hitResults[0].pose.position.x  + 0.3f, hitResults[0].pose.position.y + 0.7f, hitResults[0].pose.position.z), Quaternion.AngleAxis(270.0f, new Vector3(1.0f, 0.0f, 0.0f)));//yはいじっても意味ない
            }
            //生成したARPlaneをオフにする。ARPlaneManagerのtrackablesからARPlaneを取り出して全て非表示にする。foreachはコレクションのすべての要素を１つ１つ取得する。
            foreach (ARPlane plane in _arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }
    
}

//struct(構造体)一定に保たれるオブジェクトとデータをまとめるのに適している
//レイキャストとは、ある場所から、見えない光線を発射し、検知した平面とぶつかる位置があるかどうか検知するもの。例えば、スプラトゥーンで打った弾が相手に当たったかどうかなどに使用される。