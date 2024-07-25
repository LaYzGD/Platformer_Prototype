using UnityEngine;

public class CollisionButton : MonoBehaviour
{
    [SerializeField] private ITriggerable _triggerableObject;

    private int _objectsOnTheButton;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _objectsOnTheButton++;

        if (_objectsOnTheButton > 1)
        {
            return;
        }

        _triggerableObject.Trigger();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _objectsOnTheButton--;

        if (_objectsOnTheButton == 0)
        {
            _triggerableObject.UnTrigger();
        }
    }
}
