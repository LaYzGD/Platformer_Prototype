using UnityEngine;

public class CollisionButton : MonoBehaviour
{
    [SerializeField] private ITriggerable _triggerableObject;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _boolName;

    private int _objectsOnTheButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _objectsOnTheButton++;

        if (_objectsOnTheButton > 1)
        {
            return;
        }

        _triggerableObject.Trigger();
        _animator.SetBool(_boolName, true);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _objectsOnTheButton--;

        if (_objectsOnTheButton == 0)
        {
            _triggerableObject.UnTrigger();
            _animator.SetBool(_boolName, false);
        }
    }
}
