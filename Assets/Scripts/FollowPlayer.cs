using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [Range(0, 1)]
    [SerializeField]
    private float _smooth;

    private void LateUpdate()
    {
        var position = transform.position;
        position.x = _player.position.x;

        transform.position = position;
    }
}
