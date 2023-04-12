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
    [SerializeField]GameObject StartPlane;
    [SerializeField]GameObject EndPlane;
    //ARRaycastManager���g�p���܂��Ɛ錾�BraycastManager�͕ϐ��B
    ARRaycastManager raycastManager;

    //ARRaycastHit�́A@���C�L���X�g��x���������Ă�@�\���̌^�Œl�^
    //�q�b�g�����i�[���郊�X�g
    List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
    private ARPlaneManager _arPlaneManager;
    void Start()
    {   //ARRaycastManager��class�̃R���|�[�l���g������Ă���Bclass������<hoge>();
        raycastManager = GetComponent<ARRaycastManager>();
        
    }

    void Update()
    {

        //���[�U�[���{�^���������Ă��Ȃ���Ԃ��牟����������true��Ԃ��B
        if (Input.GetMouseButtonDown(0)) //�Ȃ��(0)�H������0�E1�E2��3�ʂ�ŁA�l�����������ɍ��N���b�N�E�E�N���b�N�E���N���b�N�ɑΉ����܂��B�X�}�z�ł���������B
        {
            //Raycast���\�b�h�Ƃ͉�ʂ��^�b�v�����(ray���ˈʒu, �ǂ��ɕۑ�����, ������ʂ̑Ώ�)
            //TrackableType.All�͑S�Ẵ^�C�v�����o�B�i�`��ɉ��������ʂ₷�ׂĂ̎�ނ̕��ʂȂǂ��������m���邱�Ƃ��ł���j
            //if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.All))
            if (raycastManager.Raycast(Input.GetTouch(0).position, hitResults, TrackableType.Planes))
            {   // 3D�I�u�W�F�N�g�̐��� 
                //Instantiate(�R�s�[�����������I�u�W�F�N�g�A�ʒu�A����)
                Instantiate(StartPlane, new Vector3(hitResults[0].pose.position.x - 0.3f, hitResults[0].pose.position.y + 0.7f, hitResults[0].pose.position.z), Quaternion.AngleAxis(270.0f, new Vector3(1.0f, 0.0f, 0.0f)));//y�͂������Ă��Ӗ��Ȃ�
                Instantiate(EndPlane, new Vector3(hitResults[0].pose.position.x  + 0.3f, hitResults[0].pose.position.y + 0.7f, hitResults[0].pose.position.z), Quaternion.AngleAxis(270.0f, new Vector3(1.0f, 0.0f, 0.0f)));//y�͂������Ă��Ӗ��Ȃ�
            }
            //��������ARPlane���I�t�ɂ���BARPlaneManager��trackables����ARPlane�����o���đS�Ĕ�\���ɂ���Bforeach�̓R���N�V�����̂��ׂĂ̗v�f���P�P�擾����B
            foreach (ARPlane plane in _arPlaneManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }
        }
    }
    
}

//struct(�\����)���ɕۂ����I�u�W�F�N�g�ƃf�[�^���܂Ƃ߂�̂ɓK���Ă���
//���C�L���X�g�Ƃ́A����ꏊ����A�����Ȃ������𔭎˂��A���m�������ʂƂԂ���ʒu�����邩�ǂ������m������́B�Ⴆ�΁A�X�v���g�D�[���őł����e������ɓ����������ǂ����ȂǂɎg�p�����B