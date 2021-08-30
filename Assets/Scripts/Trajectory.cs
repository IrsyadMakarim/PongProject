using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    //script, collider dan rigidbody bola
    public BallControl Ball;
    private CircleCollider2D _ballCollider;
    private Rigidbody2D _ballRigidBody;

    // bola "bayangan" yang akan ditampilkan di titik tumbukan
    public GameObject BallAtCollision;



    // Start is called before the first frame update
    void Start()
    {
        _ballRigidBody = Ball.GetComponent<Rigidbody2D>();
        _ballCollider = Ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool drawBallAtCollision = false;

        Vector2 offSetHitPoint = new Vector2();

        RaycastHit2D[] circleCastHit2DArray = 
            Physics2D.CircleCastAll(_ballRigidBody.position, _ballCollider.radius, _ballRigidBody.velocity.normalized);
        
        foreach (RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            bool isCircleCastHitColliderNull = (circleCastHit2D.collider != null);
            bool isCircleCastHitBallControlNull = (circleCastHit2D.collider.GetComponent<BallControl>() == null);

            if (isCircleCastHitColliderNull && isCircleCastHitBallControlNull)
            {
                Vector2 hitPoint = circleCastHit2D.point;

                Vector2 hitNormal = circleCastHit2D.normal;

                offSetHitPoint = hitPoint + hitNormal * _ballCollider.radius;

                DottedLine.DottedLine.Instance.DrawDottedLine(Ball.transform.position, offSetHitPoint);
                
                bool isSidewallColliderNull = (circleCastHit2D.collider.GetComponent<SideWall>() == null);
                if (isSidewallColliderNull)
                {
                    Vector2 inVector = (offSetHitPoint - Ball.TrajectoryOrigin).normalized;

                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if (outDot > -1.0f && outDot < 1.0)
                    {
                        DottedLine.DottedLine.Instance.DrawDottedLine(
                            offSetHitPoint,
                            offSetHitPoint + outVector * 10.0f);
                        
                        drawBallAtCollision = true;
                    }
                }
            }
        }
        if (drawBallAtCollision)
        {
            BallAtCollision.transform.position = offSetHitPoint;
            BallAtCollision.SetActive(true);
        }else
        {
            BallAtCollision.SetActive(false);
        }
    }

}
