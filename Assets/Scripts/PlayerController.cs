using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    [SerializeField] private InteractableItem[] _items;
    [SerializeField] private GameObject _inventoryHolder;
    [SerializeField] private float _force = 10;
    
    private GameObject _previousHitObject;
    private GameObject _item;
    private GameObject _interactableItem;
    private bool _areHandsFree = true;

    private void Update()
    {
        CheckRaycast();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ThrowItem();
        }
    }

    private void ThrowItem()
    {
        if (_item != null)
        {
            _item.GetComponent<Rigidbody>().isKinematic = false;
            _item.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * _force, ForceMode.Impulse);
            _item.transform.parent = null;
            _item = null;
            _areHandsFree = true;
        }
    }

    private void Initialize(GameObject gameObject)
    {
        gameObject.transform.SetParent(_inventoryHolder.transform);
        gameObject.transform.position = _inventoryHolder.transform.position;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        _item = gameObject;
        _areHandsFree = false;
    }

    private void RemoveSelection()
    {
        foreach (var item in _items)
        {
            item.OnRemoveFocus.Invoke();
        }

        _previousHitObject = null;
    }

    private void CheckRaycast()
    {
        var ray = Camera.main.ScreenPointToRay(_aim.position);
        if (Physics.Raycast(ray, out RaycastHit hitInfo) &&
            hitInfo.collider.gameObject.CompareTag("InteractableInventory"))
        {
            var hitObject = hitInfo.collider.gameObject;
            foreach (var _item in _items)
            {
                if (hitObject == _item.gameObject)
                {
                    _item.OnSetFocus.Invoke();
                    if (Input.GetKeyDown(KeyCode.E) && _areHandsFree)
                    {
                        Initialize(hitObject);
                    }
                }
                else if (_previousHitObject == _item.gameObject)
                {
                    RemoveSelection();
                }
            }

            _previousHitObject = hitObject;
        }
        else if (_previousHitObject != null)
        {
            RemoveSelection();
        }
    }
}