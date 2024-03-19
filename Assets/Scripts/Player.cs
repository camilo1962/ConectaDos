using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Speed { get; set; }

    private float _timeLeft;

    public event Action<Vector3> OnPlayerTryMove;

    public int Col => Mathf.RoundToInt(transform.position.x);
    public int Row => Mathf.RoundToInt(transform.position.y);
    //public Joystick joyHorizontal;
    //public Joystick joyVertical;

   

    private void Start()
    {
        Speed = 0.2f;
        _timeLeft = 0f;
       
    }

    private void Update()
    {
        //MueveArriba();
        //MueveAbajo();
        //MueveDerecha();
        //MueveIzquierda();

        //float movHorizontal ;
        //float movVertical;
        //
        //movHorizontal = Input.GetAxisRaw("Horizontal") ;
        //movVertical = Input.GetAxisRaw("Vertical") ;
        //
        //if (_timeLeft <= 0)
        //{
        //    _timeLeft = Speed;
        //
        //    if (movVertical > 0)
        //        TryMove(Vector3.up);
        //    if (Input.touchCount > 0)
        //        TryMove(Vector3.up);
        //
        //    else if (movHorizontal > 0)
        //        TryMove(Vector3.right);
        //    else if (Input.touchCount > 0)
        //        TryMove(Vector3.right);
        //
        //    else if (movHorizontal < 0)
        //        TryMove(Vector3.left);
        //    else if (Input.touchCount >0)
        //        TryMove(Vector3.left);
        //
        //    else if (movVertical < 0)
        //        TryMove(Vector3.down);
        //    else if (Input.touchCount > 0)
        //        TryMove(Vector3.down);
        //}
        //else
        //{
        //    _timeLeft -= Time.deltaTime;
        //}
        //
       
    }
    public void MueveArriba()
    {
       
      
        if (_timeLeft >= 0)
        {
            _timeLeft = Speed;

            if (Input.GetAxisRaw("Vertical") > 0)
                TryMove(Vector3.up);
            if (Input.touchCount > 0)
                TryMove(Vector3.up);
        }
        else
        {
            _timeLeft -= Time.deltaTime;
        }
        return;
    }
    public void MueveAbajo()
    {
             
        if (_timeLeft >= 0)
        {
            _timeLeft = Speed;

           if  (Input.GetAxisRaw("Vertical") < 0)
                TryMove(Vector3.down);
           if (Input.touchCount > 0)
                TryMove(Vector3.down);
        }
        else
        {
            _timeLeft -= Time.deltaTime;
        }
        return;
    }
    public void MueveDerecha()
    {
             
        if (_timeLeft >= 0)
        {
            _timeLeft = Speed;
            if (Input.GetAxisRaw("Horizontal") < 0)
                TryMove(Vector3.left);
            if (Input.touchCount > 0)
                TryMove(Vector3.left);
        }
        else
        {
            _timeLeft -= Time.deltaTime;
        }
        return;
    }
    public void MueveIzquierda()
    {
               

        if (_timeLeft >= 0)
        {
            _timeLeft = Speed;
            if (Input.GetAxisRaw("Horizontal") > 0)
                TryMove(Vector3.right);
            if (Input.touchCount > 0)
                TryMove(Vector3.right);
        }
        else
        {
            _timeLeft -= Time.deltaTime;
        }
        return;
    }


    private void TryMove(Vector3 direction)
    {
        var newPosition = transform.position + direction * 3;

        if (newPosition.y > 9 || newPosition.y < -3)
            return;

        OnPlayerTryMove?.Invoke(newPosition);
    }
}
