using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Suriyun.MCS;

public class ControllerManager : MonoBehaviour
{
    public static ControllerManager instance;
    [SerializeField]private UniversalButton DpadInput;
    public static UniversalButton Dpad;

    [SerializeField] private UniversalButton attackButtonInput;
    public static UniversalButton attackbutton;

    [SerializeField] private UniversalButton switchButtonInput,pickupButtonInput;
    public static UniversalButton switchButton,pickupButton;

    [SerializeField] public UniversalButton[] skillbutton = new UniversalButton[] { };

    [SerializeField] private UniversalButton spellbuttonInput;
    public static UniversalButton spellbutton;

    [SerializeField] private SkillCanceller skillCancelinput;
    public static SkillCanceller skillCancel;
    void Start()
    {
        Dpad = DpadInput;
        attackbutton = attackButtonInput;
        switchButton = switchButtonInput;
        pickupButton = pickupButtonInput;
        skillCancel = skillCancelinput;
        spellbutton = spellbuttonInput;
    }
    private void Awake()
    {
        instance = this;
    }
}
