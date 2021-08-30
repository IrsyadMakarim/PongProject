using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode UpButton = KeyCode.W;

    public KeyCode DownButton = KeyCode.S;

    public float Speed = 10.0f;

    public float ScreenYBoundary = 9.0f;

    private Rigidbody2D _rigidbody2D;

    private int _score;

    // Titik tumbukan terakhir dengan bola, untuk menampilkan variabel fisika terkait tumbukan tersebut
    private ContactPoint2D _lastContactPoint;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        GetInputPlayer();
        ApplyScreenBoundary();
    }

    ///<summary>
    /// Mengambil input player dan apply velocity berdasarkan arah inputnya
    /// </summary>
    private void GetInputPlayer()
    {
        Vector2 velocity = _rigidbody2D.velocity;

        if (Input.GetKey(UpButton))
        {
            velocity.y = Speed;
        }
        else if (Input.GetKey(DownButton))
        {
            velocity.y = -Speed;
        }
        else
        {
            velocity.y = 0.0f;
        }

        _rigidbody2D.velocity = velocity;
    }

    ///<summary>
    /// menambahkan batas kepada player jadi tidak keluar layar
    /// </summary>
    private void ApplyScreenBoundary()
    {
        Vector3 position = transform.position;

        if(position.y > ScreenYBoundary)
        {
            position.y = ScreenYBoundary;
        }
        else if(position.y < -ScreenYBoundary)
        {
            position.y = -ScreenYBoundary;
        }

        transform.position = position;
    }

    /// <summary> 
    /// menambah score 
    /// </summary> 
    public void IncrementScore()
    {
        _score++;
    }

    ///  <summary>  
    /// reset score menjadi 0 
    /// </summary> 
    public void ResetScore()
    {
        _score = 0;
    }

    /// <summary>
    /// mengembalikan variable score
    /// </summary>
    public int Score
    {
        get {return _score;}
    }

    /// <summary>
    /// untuk mengakses informasi titik kontak dari kelas lain
    /// </summary>
    public ContactPoint2D LastContactPoint
    {
        get {return _lastContactPoint;}
    }

    /// <summary>
    /// ketika terjadi tumbukan dengan bola, rekam titik kontaknya
    /// </summary>
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.name.Equals("Ball"))
        {
            _lastContactPoint = other.GetContact(0);
        }
    }
}
