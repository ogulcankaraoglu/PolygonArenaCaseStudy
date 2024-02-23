using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectableAttachment : MonoBehaviour
{
    [HideInInspector]
    public WeaponAttachmentData CurrentAttachmentData;
    public Image AttachmentImage;
    public TMP_Text AttachmentText;
    public Image FrameImage;
    public Image BackgroundImage;
    public GameObject SelectionFrame;
    public Button Button;

    private void Start()
    {
        Button.onClick.AddListener(SetAttachmentVisual);
    }

    public void SetAttachmentVisual()
    {
        WeaponCustomization.Instance.CurrentWeaponManager.SetAttachmentVisual(CurrentAttachmentData.AttachmentType,CurrentAttachmentData.AttachmentID);
        WeaponCustomization.Instance.SelectableAttachments.UpdateSelectedAttachment(CurrentAttachmentData.AttachmentType,CurrentAttachmentData.AttachmentID);
        WeaponCustomization.Instance.CurrentAttachmentData = CurrentAttachmentData;
    }
}
