using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator Animator;
    private PlayerMovement PlayerMovement;
    private int Direction;


    private void Start()
    {
        Animator = GetComponent<Animator>();
        PlayerMovement = GetComponent<PlayerMovement>();
        Direction = Animator.StringToHash("Direction");
    }

    private void Update()
    {
        Animate();
        // DEBUG DO PRINTU KIERUNKU
        // Debug.Log(Animator.GetInteger(Direction));
    }

    private void Animate()
    {
        // Kierunki działają tak jak są boole ustawione a 0 to idle
        // TODO: Dodać przejścia pomiędzy kierunkami w animatorze
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
        return PlayerMovement.MovementDirectionVector().x > 0;
    }

    private bool MovingLeft()
    {
        return PlayerMovement.MovementDirectionVector().x < 0;
    }

    private bool MovingUp()
    {
        return PlayerMovement.MovementDirectionVector().y > 0;
    }

    private bool MovingDown()
    {
        return PlayerMovement.MovementDirectionVector().y < 0;
    }
}