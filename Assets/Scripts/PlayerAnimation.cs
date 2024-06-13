using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private string isMovingDownParameterName = "IsMovingDown";

    private int _isMovingDownHash;

    private void Start()
    {
        _isMovingDownHash = Animator.StringToHash(isMovingDownParameterName);
    }

    private void Update()
    {
        animator.SetBool(_isMovingDownHash, movement.IsMovingDown());
    }
}
