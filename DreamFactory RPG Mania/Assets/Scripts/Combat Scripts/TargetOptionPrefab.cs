using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TargetOptionPrefab : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] public TMP_Text optionName;
    [SerializeField] public Button optionButton;
    private ITargetUpdatable _updatable;
    private CombatEntityConfig _target;

    public void Subscribe(ITargetUpdatable updatable)
    {
        _updatable = updatable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_target != null)
            _updatable.SwitchTarget(_target);
    }

    public void Attach(CombatEntityConfig target)
    {
        _target = target;
    }
}
