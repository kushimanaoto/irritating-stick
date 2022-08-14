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
    [SerializeField]GameObject objectPrefab;

    //ARRaycastManagerを使用しますと宣言。raycastManagerは変数。
    ARRaycastManager raycastManager;

    //ARRaycastHitは、@レイキャストの情報を持ってる@構造体型で値型
    //ヒット情報を格納するリスト
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    void Start()
    {   //ARRaycastManagerのclassのコンポーネントを取ってくる。classだから<hoge>();
        raycastManager = GetComponent<ARRaycastManager>();
        
    }

    void Update()
    {
        //Vector3 placePosition = new Vector3(2, 0, 0);

        //ユーザーがボタンを押していない状態から押した時だけtrueを返す。
        if (Input.GetMouseButtonDown(0)) //なんで(0)？引数は0・1・2の3通りで、値が小さい順に左クリック・右クリック・中クリックに対応します。スマホでも反応する。
        {
            //Raycastメソッドとは画面をタップすると(ray発射位置, どこに保存する, 当たる面の対象)
            //TrackableType.Allは全てのタイプを検出。（形状に沿った平面やすべての種類の平面などだけを検知することもできる）

            //if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.All))
            raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.All);
            {   // 3Dオブジェクトの生成 Quaternion.identityは回転していないという意味
                //Instantiate(コピーしたい既存オブジェクト、位置、向き)
                //Instantiate(objectPrefab, hitResults[0].pose.position, Quaternion.identity);
                Debug.Log(hitResults[0].pose.position +":position");
                Debug.Log(Quaternion.identity + ":quaternion");

                Instantiate(objectPrefab, new Vector3(hitResults[0].pose.position.x + 0.2f, hitResults[0].pose.position.y , hitResults[0].pose.position.z), Quaternion.AngleAxis(90.0f, new Vector3(0.0f, 0.0f, 1.0f)));
                Instantiate(objectPrefab, hitResults[0].pose.position, Quaternion.identity);

                //placePosition.x += objectPrefab.transform.localScale.x;
                //Instantiate(objectPrefab, placePosition, Quaternion.Euler(0f, 90.0f, 90.0f));
            }
        }
    }
}

//struct(構造体)一定に保たれるオブジェクトとデータをまとめるのに適している
//レイキャストとは、ある場所から、見えない光線を発射し、検知した平面とぶつかる位置があるかどうか検知するもの。例えば、スプラトゥーンで打った弾が相手に当たったかどうかなどに使用される。


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using System;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObject : MonoBehaviour
{
    
    [SerializeField] private GameObject placementIndicator;
    [SerializeField] private GameObject objectToPlace;
    private GameObject previousObject;

    //private ARSessionOrigin arOrigin;
    private Pose placementPose;
    private ARRaycastManager raycastManager;
    private bool placementPoseIsValid = false;

    // Start is called before the first frame update
    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        raycastManager = GetComponent<ARRaycastManager>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

            if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                TapToPlaceObject();
            }
        }
        else
        {
            placementIndicator.SetActive(false);
        }

    }

    private void TapToPlaceObject()
    {
        Destroy(previousObject);
        previousObject = Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
        placementIndicator.SetActive(false);
        GetComponent<ARTapToPlaceObject>().enabled = false;
        GetComponent<ARPlaneManager>().enabled = false;
        //var clones = GameObject.FindGameObjectsWithTag("Plane");
        //foreach (var clone in clones)
        //{
        //    Destroy(clone);
        //}
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        raycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.  );

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}*/
