using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRClimbController : MonoBehaviour
{
    public bool _enable = true;

    [SerializeField] private SimpleCapsuleWithStickMovement _playerController;
    //[SerializeField] private CharacterController _playerMovement;
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Collider _playerCollider;

    [Space(10)]

    [SerializeField] private float sensitivity;

    [Space(10)]

    [SerializeField] private VRHandModule rHand;
    [SerializeField] private VRHandModule lHand;

    private void Update()
    {
        if (_enable)
        {
            if (lHand.inUse)
            {
                Climb(lHand);
            }
            if (rHand.inUse)
            {
                Climb(rHand);
            }
            if (!lHand.inUse && !rHand.inUse)
            {
                _playerController.enabled = true;
                
                _playerRb.useGravity = true;
                _playerRb.isKinematic = false;
                _playerCollider.isTrigger = false;
            }

        }
    }

    /// Climbing calculations.
    private void Climb(VRHandModule hand)
    {
        _playerController.enabled = false;
        _playerRb.useGravity = false;
        _playerRb.isKinematic = true;
        _playerCollider.isTrigger = true;

        Vector3 movement = hand.force * Time.deltaTime * sensitivity;
        _playerRb.MovePosition(_playerRb.position + movement);
    }
}
