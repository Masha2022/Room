using System;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public Action OnSetFocus;
    public Action OnRemoveFocus;
    
    [SerializeField]
    private int _highlightIntensity = 4;    
    private Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        OnSetFocus += SetFocus;
        OnRemoveFocus += RemoveFocus;
    }

    private void SetFocus()
    {
        _outline.OutlineWidth = _highlightIntensity;
    }
    
    private void RemoveFocus()
    {
        _outline.OutlineWidth = 0;
    }
}