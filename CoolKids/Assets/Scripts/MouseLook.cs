using System;
using UnityEngine;


public class MouseLook: MonoBehaviour {
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;


    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;


    void Start(){
        Init(transform);
    }

    public void Init(Transform character){
        m_CharacterTargetRot = character.localRotation;
    }

    void Update(){
        LookRotation(transform);
    }

    public void LookRotation(Transform character){
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, -xRot);
        //m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

        if(clampVerticalRotation)
        //    m_CameraTargetRot = ClampXRotation (m_CameraTargetRot);

        if(smooth) {
            character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
            //camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,smoothTime * Time.deltaTime);
        }
        else {
            character.localRotation = m_CharacterTargetRot;
        //    camera.localRotation = m_CameraTargetRot;
        }
    }


    Quaternion ClampXRotation(Quaternion q){
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
