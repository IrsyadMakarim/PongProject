using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl Player1;
    private Rigidbody2D _playerRigidbody;

    public PlayerControl Player2;
    private Rigidbody2D _player2Rigidbody;

    public BallControl Ball;
    private Rigidbody2D _ballRigidbody;
    private CircleCollider2D _ballCollider;

    public int MaxScore;

    private bool _isDebugWindowShown = false;

    public Trajectory Trajectory;

    // Start is called before the first frame update
    private void Start()
    {
        _playerRigidbody = Player1.GetComponent<Rigidbody2D>();
        _player2Rigidbody = Player2.GetComponent<Rigidbody2D>();
        _ballRigidbody = Ball.GetComponent<Rigidbody2D>();
        _ballCollider = Ball.GetComponent<CircleCollider2D>();
    }

    /// <summary>
    /// set UI, text dan button di tengah2 layar
    /// </summary>
    private void OnGUI() 
    {
        GUI.Label(new Rect(Screen.width / 2 - 150 - 12, 20, 100,  100), "" + Player1.Score);
        GUI.Label(new Rect(Screen.width / 2 + 150 + 12, 20, 100,  100), "" + Player2.Score);

        ShowRestartButton();

        if (Player1.Score == MaxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 10, 2000, 1000), "PLAYER ONE WINS");

            Ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        else if (Player2.Score == MaxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 + 30, Screen.height / 2 - 10, 2000, 1000), "PLAYER TWO WINS");

            Ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }
        ShowDebugWindow();
    }

    private void ShowBallTrajectory()
    {
        Trajectory.enabled = !Trajectory.enabled;
    }

    /// <summary>
    /// membuat button untuk restart permainan
    /// </summary>
    private void ShowRestartButton()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            Player1.ResetScore();
            Player2.ResetScore();

            Ball.SendMessage("RestartGame", 0.5f);
        }
    }

    /// <summary>
    /// memperlihatkan window untuk debug, serta membuat variable fisika untuk ditampilkan di dalam debug
    /// </summary>
    private void ShowDebugWindow()
    {
        if(_isDebugWindowShown)
        {
            //simpan warna lama
            Color oldColor = GUI.backgroundColor;

            //beri warna merah
            GUI.backgroundColor = Color.red;

            //Simpan variabel-variabel fisika yang akan ditampilkan
            float ballMass = _ballRigidbody.mass;
            Vector2 ballVelocity = _ballRigidbody.velocity;
            float ballSpeeed = _ballRigidbody.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity;
            float ballFriction = _ballCollider.friction;

            float impulsePlayer1X = Player1.LastContactPoint.normalImpulse;
            float impulsePlayer1Y = Player1.LastContactPoint.tangentImpulse;
            float impulsePlayer2X = Player2.LastContactPoint.normalImpulse;
            float impulsePlayer2Y = Player2.LastContactPoint.tangentImpulse;

            //Tentukan text debug-nya
            string debugText =
                "Ball mass = " + ballMass + "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            //Tampikan debug window
            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            //Kembalikan warna ke default
            GUI.backgroundColor = oldColor;
        }

        //Toggle nilai debug window ketika pemain klik tombol nya
        if (GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53), "TOOGLE\nDEBUG INFO"))
        {
            _isDebugWindowShown = !_isDebugWindowShown;
            ShowBallTrajectory();
        }
    }
}
