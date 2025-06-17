using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structs
{
    public struct BulletType
    {
        public const string ROCK = "common";
        public const string PAPER = "paper";
        public const string SCISSORS = "scissors";
    }

    public static Quaternion CollisionRotation(Rigidbody2D rigidbody)
    {
        // Change rotation
        Vector2 velocity = rigidbody.linearVelocity;
        float Ax = velocity.x;
        float Ay = velocity.y;
        //float A = Mathf.Sqrt((Ax * Ax)  + (Ay * Ay));

        float angleRadians = Mathf.Atan2(Ay, Ax);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        return Quaternion.Euler(0, 0, angleDegrees - 90);

    }

    public static Vector2 CollisionVector(Rigidbody2D rigidbody, Collider2D collision)
    {
        Vector2 normal = collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Vector2 incidentDirection = rigidbody.linearVelocity.normalized;
        Vector2 reflectedDirection = Vector2.Reflect(incidentDirection, normal);

        return reflectedDirection * rigidbody.linearVelocity.magnitude;
    }
}
