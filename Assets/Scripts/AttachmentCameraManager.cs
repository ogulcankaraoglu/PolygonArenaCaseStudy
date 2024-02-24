using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttachmentCameraManager : MonoBehaviour
{
    private Vector3 _offset;
    public ParticleSystem Sparkles;

    public void CalculateOffset()
    {
        _offset = WeaponCustomization.Instance.CurrentWeaponManager.transform.position - transform.position;
    }

    public void SetAttachmentView(Transform _transform)
    {
        Sparkles.transform.position = _transform.position;
        transform.DOMove(_transform.position - _offset, 0.5f);
    }

    public void EquipCameraAnimation()
    {
        Sparkles.Play();
        transform.DOShakeRotation(0.1f, 2, 90, 90, true);
    }
}
