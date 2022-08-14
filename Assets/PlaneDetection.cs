using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneDetection : MonoBehaviour
{
    ARRaycastManager raycastManager;
    [SerializeField] GameObject plane;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended || plane == null)
        {
            return;
        }

        var hits = new List<ARRaycastHit>();
        // TrackableType.PlaneWithinPolygon���w�肷�邱�Ƃɂ���Č��o�������ʂ�Ώۂɂł���
        if (raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            // �C���X�^���X��
            Instantiate(plane, hitPose.position, Quaternion.Euler(0, 0, 90));
        }
    }
}
//hitPose.position = new Vector3(0f, 0.5f, 0f);