using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//[RequireComponent]��RequireComponent��ARRaycastManager���w�肷��Ǝ����I��ARRaycastManager��Inspecter�ɃZ�b�g�����B
//[ARRaycastManager]�^�b�v�����ꏊ�Ƀ��f����z�u���邽�߁AARRaycastManager�N���X��@Raycast���\�b�h���g�p���Ă���B
[RequireComponent(typeof(ARRaycastManager))] //typeof�Ƃ́H�^�̃`�F�b�N�܂��͌^�̕ϊ����ł���B�����ł͂����������̂Ƃ��čl����B

/*Start,Update���\�b�h��Unity�̃V�X�e������Ăяo�����C�x���g�֐��ƌĂ΂��Monobehaviour�N���X�̃��\�b�h
�����̃��W�b�N���L�ڂ��邾���̃N���X�Ȃǂ͌p��������K�v�͂Ȃ�*/
public class ARTapToPlaceObject : MonoBehaviour
{   //[SerializeField]�V���A���C�[�[�V���������Unity��Œl���������悤�ɂȂ�B
    [SerializeField]GameObject objectPrefab;

    //ARRaycastManager���g�p���܂��Ɛ錾�BraycastManager�͕ϐ��B
    ARRaycastManager raycastManager;

    //ARRaycastHit�́A@���C�L���X�g�̏��������Ă�@�\���̌^�Œl�^
    //�q�b�g�����i�[���郊�X�g
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();

    void Start()
    {   //ARRaycastManager��class�̃R���|�[�l���g������Ă���Bclass������<hoge>();
        raycastManager = GetComponent<ARRaycastManager>();
        
    }

    void Update()
    {
        //Vector3 placePosition = new Vector3(2, 0, 0);

        //���[�U�[���{�^���������Ă��Ȃ���Ԃ��牟����������true��Ԃ��B
        if (Input.GetMouseButtonDown(0)) //�Ȃ��(0)�H������0�E1�E2��3�ʂ�ŁA�l�����������ɍ��N���b�N�E�E�N���b�N�E���N���b�N�ɑΉ����܂��B�X�}�z�ł���������B
        {
            //Raycast���\�b�h�Ƃ͉�ʂ��^�b�v�����(ray���ˈʒu, �ǂ��ɕۑ�����, ������ʂ̑Ώ�)
            //TrackableType.All�͑S�Ẵ^�C�v�����o�B�i�`��ɉ��������ʂ₷�ׂĂ̎�ނ̕��ʂȂǂ��������m���邱�Ƃ��ł���j

            //if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.All))
            raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.All);
            {   // 3D�I�u�W�F�N�g�̐��� Quaternion.identity�͉�]���Ă��Ȃ��Ƃ����Ӗ�
                //Instantiate(�R�s�[�����������I�u�W�F�N�g�A�ʒu�A����)
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

//struct(�\����)���ɕۂ����I�u�W�F�N�g�ƃf�[�^���܂Ƃ߂�̂ɓK���Ă���
//���C�L���X�g�Ƃ́A����ꏊ����A�����Ȃ������𔭎˂��A���m�������ʂƂԂ���ʒu�����邩�ǂ������m������́B�Ⴆ�΁A�X�v���g�D�[���őł����e������ɓ����������ǂ����ȂǂɎg�p�����B


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
