using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    public PlayerControl Player;
    
    [SerializeField]
    public GameManager GameManager;

    /// <summary>
    /// jika object bola masuk ke trigger, maka collider akan mengirim pesan kepada game object bola untuk restart game
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.name == "Ball")
        {
            Player.IncrementScore();

            if (Player.Score < GameManager.MaxScore)
            {
                other.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
