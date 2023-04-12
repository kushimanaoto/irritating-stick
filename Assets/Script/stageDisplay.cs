using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))] //typeof�Ƃ́H�^�̃`�F�b�N�܂��͌^�̕ϊ����ł���B�����ł͂����������̂Ƃ��čl����B

public class stageDisplay : MonoBehaviour
{   
    //ARRaycastManager���g�p���܂��Ɛ錾�BraycastManager�͕ϐ��B
    ARRaycastManager raycast;

    //ARRaycastHit�́A@���C�L���X�g��x���������Ă�@�\���̌^�Œl�^
    //�q�b�g�����i�[���郊�X�g
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    bool isCalledOnce = false;

    //[SerializeField]�V���A���C�[�[�V���������Unity��Œl���������悤�ɂȂ�B
    [SerializeField] private ARPlaneManager _arPlaneManager;

    [SerializeField] GameObject Stage1;
    void Start()
    {
        raycast = GetComponent<ARRaycastManager>();
    }

    void Update()
    {

        //���[�U�[���{�^���������Ă��Ȃ���Ԃ��牟����������true��Ԃ��B
        if (Input.GetMouseButtonDown(0)) //�Ȃ��(0)�H������0�E1�E2��3�ʂ�ŁA�l�����������ɍ��N���b�N�E�E�N���b�N�E���N���b�N�ɑΉ����܂��B�X�}�z�ł�0����������B
        {
            if (raycast.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.Planes))
            {   // 3D�I�u�W�F�N�g�̐��� Quaternion.identity�͉�]���Ă��Ȃ��Ƃ����Ӗ�
                if (!isCalledOnce)
                {
                    isCalledOnce = true; //��񂾂����삳���邽��
                    //GameObject Stage = (GameObject)Resources.Load("prefab/Stage");
                    GameObject Stage = Stage1;
                    Stage1.SetActive(true);
                    Instantiate(Stage, new Vector3(hitResults[0].pose.position.x, hitResults[0].pose.position.y + 1.8f, hitResults[0].pose.position.z + 1.0f), Quaternion.Euler(270, 0, 0));
                    //Instantiate(obj, transform.position, Quaternion.identity);
                    

                    //��������ARPlane���I�t�ɂ���BARPlaneManager��trackables����ARPlane�����o���đS�Ĕ�\���ɂ���Bforeach�̓R���N�V�����̂��ׂĂ̗v�f���P�P�擾����B
                    foreach (ARPlane plane in _arPlaneManager.trackables)
                    {
                        plane.gameObject.SetActive(false);
                    }
                    //���ʔF���̋@�\���I�t
                    _arPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
                }
            }
        }
    }
}