using UnityEngine;

public class GravitationAbility : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _gravity;
    [SerializeField] private float _time;

    private Player _player;
    private PlayerData _data;

    public void InitData(Player player, PlayerData data)
    {
        _player = player;
        _data = data;
    }

    public void UseAbility()
    {
        _player.AudioEffects.PlaySound(_data.SoundData.GravicyChangeClip);
        var coliders = Physics2D.OverlapCircleAll(transform.position, _radius);
        foreach (var col in coliders)
        {
            if (col.TryGetComponent(out IForceControllable controllable))
            {
                controllable?.ChangeGravity(_gravity, _time);
            }
        }
    }
}
