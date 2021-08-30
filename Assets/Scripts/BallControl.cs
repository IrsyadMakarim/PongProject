using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public float BallXInitialForce;
    public float BallYInitialForce;

    private Vector2 _trajectoryOrigin;

    /// <summary>
    /// Reset posisi dan velocity bola jadi 0
    /// </summary>
    private void ResetBall()
    {
        transform.position = Vector2.zero;
        _rigidbody2D.velocity = Vector2.zero;
    }

    /// <summary>
    /// apply force dan direction kepada bolanya
    /// </summary>
    private void PushBall()
    {
        float yRandomInitialForce = Random.Range(-BallYInitialForce, BallYInitialForce);

        float randomDirection = Random.Range(0, 2);

        Vector2 leftDirection = new Vector2(-BallXInitialForce, yRandomInitialForce);

        Vector2 rightDirection = new Vector2(BallXInitialForce, yRandomInitialForce);

        if (randomDirection < 1.0f)
        {
            _rigidbody2D.AddForce(leftDirection);
        }
        else
        {
            _rigidbody2D.AddForce(rightDirection);
        }
    }

    /// <summary>
    /// restart game
    /// </summary>
    private void RestartGame()
    {
        ResetBall();
        Invoke("PushBall", 2);
    }

    ///<summary>
    /// ketika bola beranjak dari sebuah tumbukan, rekam titik tumbukan tersebut
    /// </summary>
    private void OnCollisionExit2D(Collision2D other) 
    {
        _trajectoryOrigin = transform.position;
    }

    /// <summary>
    /// untuk mengakses informasi titik asal lingkaran
    /// </summary>
    public Vector2 TrajectoryOrigin
    {
        get { return _trajectoryOrigin; }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _trajectoryOrigin = transform.position;

        RestartGame();
    }
}
