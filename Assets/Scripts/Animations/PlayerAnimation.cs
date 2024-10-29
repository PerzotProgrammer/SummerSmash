using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator Animator;
    private PlayerLogic PlayerLogic;
    private int Direction;


    private void Start()
    {
        Animator = GetComponent<Animator>();
        PlayerLogic = GetComponent<PlayerLogic>();
        Direction = Animator.StringToHash("Direction");
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        Animator.SetInteger(Direction, 0);

        if (MovingRight())
        {
            Animator.SetInteger(Direction, 1);
        }

        if (MovingLeft())
        {
            Animator.SetInteger(Direction, 2);
        }

        if (MovingUp())
        {
            Animator.SetInteger(Direction, 3);
        }

        if (MovingDown())
        {
            Animator.SetInteger(Direction, 4);
        }
    }

    private bool MovingRight()
    {
        return PlayerLogic.MovementVector.x > 0;
    }

    private bool MovingLeft()
    {
        return PlayerLogic.MovementVector.x < 0;
    }

    private bool MovingUp()
    {
        return PlayerLogic.MovementVector.y > 0;
    }

    private bool MovingDown()
    {
        return PlayerLogic.MovementVector.y < 0;
    }
}