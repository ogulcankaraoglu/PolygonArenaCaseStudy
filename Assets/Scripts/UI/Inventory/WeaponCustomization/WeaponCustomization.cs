using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCustomization : MonoBehaviour
{
    public static WeaponCustomization Instance;

    [HideInInspector]
    public WeaponData CurrentWeaponData;
    [HideInInspector]
    public WeaponAttachmentData CurrentAttachmentData;
    [HideInInspector]
    public WeaponAttachmentManager CurrentWeaponManager;


    [Header("CUSTIMIZATION PANELS")]
    public WeaponStatsPanel StatsPanel;
    public WeaponAttachmentCategories AttachmentCategories;
    public WeaponSelectableAttachments SelectableAttachments;

    [Header("EQUIP BUTTON COMPONENTS")]
    public Button EquipButton;
    public Image EquipButtonImage;
    public TMP_Text EquipButtonText;
    public Color ButtonActiveColor;
    public Color ButtonPassiveColor;

    public AttachmentCameraManager CameraManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        EquipButton.onClick.AddListener(OnPressedEquipButton);
    }

    public void OnPressedEquipButton()
    {
        CurrentWeaponManager.SaveAttachmentData(CurrentAttachmentData.AttachmentType,CurrentAttachmentData.AttachmentID);
        SetEquipButtonState(CurrentWeaponManager.IsAttachmentEquipped(CurrentAttachmentData.AttachmentType,CurrentAttachmentData.AttachmentID));
        AttachmentCategories.UpdateCategorySprite(CurrentAttachmentData.AttachmentType);
        CameraManager.EquipCameraAnimation();
    }

    public void SetEquipButtonState(bool _isActive)
    {
        EquipButton.interactable = _isActive;
        if (_isActive)
        {
            EquipButtonText.text = "EQUIP";
            EquipButtonImage.color = ButtonActiveColor;
        }
        else
        {
            EquipButtonText.text = "EQUIPPED";
            EquipButtonImage.color = ButtonPassiveColor;
        }
    }

    public void SetCurrentWeaponStats()
    {
        CameraManager.CalculateOffset();
        StatsPanel.SetWeaponStatTexts(CurrentWeaponData);
    }

}
