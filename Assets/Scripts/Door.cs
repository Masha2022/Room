using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpen;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SwitchDoorState()
    {
        _isOpen = !_isOpen;
        _animator.SetBool("isOpen", _isOpen);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "Door")
                {
                    SwitchDoorState();
                }
            }
        }
        
    }
}