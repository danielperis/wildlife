﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jintori.Game
{
    // --- Class Declaration ------------------------------------------------------------------------
    public abstract class Bouncy : Enemy
    {
        // --- Events -----------------------------------------------------------------------------------
        // --- Constants --------------------------------------------------------------------------------
        static readonly Vector2[] Directions = new Vector2[]
        {
            (new Vector2(-1, -1)).normalized,
            (new Vector2( 1, -1)).normalized,
            (new Vector2( 1,  1)).normalized,
            (new Vector2(-1,  1)).normalized,
        };

        // --- Static Properties ------------------------------------------------------------------------
        // --- Static Methods ---------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------
        // --- Inspector --------------------------------------------------------------------------------
        // --- Properties -------------------------------------------------------------------------------
        /// <summary> Velocity vector </summary>
        protected Vector2 velocity { get; private set; }

        /// <summary> Fixed speed at which the bouncy moves </summary>
        float speed;

        /// <summary> If true, it will always move in a 45 degree angle </summary>
        bool lock45;

        /// <summary> Position in float, as not to accumulate rounding errors from x, y </summary>
        Vector2 position;

        // --- MonoBehaviour ----------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        // --- Methods ----------------------------------------------------------------------------------
        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Initializes bouncy with a given velocity (direction and speed)
        /// </summary>
        public void Initialize(Vector2 velocity, bool lock45 = true)
        {
            this.lock45 = lock45;
            this.velocity = velocity;
            position = new Vector2(x, y);
            speed = velocity.magnitude;

            if (lock45)
                ClampVelocity45();
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Initializes this bouncy. Call during Setup()
        /// </summary>
        public void Initialize(float speed, bool lock45 = true)
        {
            this.lock45 = lock45;
            position = new Vector2(x, y);
            this.speed = speed;

            if (lock45)
                velocity = Directions[Random.Range(0, 4)] * speed;
            else
                velocity = Random.insideUnitCircle.normalized * speed;
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Clamps the velocity so it always bounces at a 45 degree angle
        /// </summary>
        void ClampVelocity45()
        {
            // find the direction that gives out
            // the smallest angle to a 45 degree direction
            float smallest = Vector2.Angle(velocity, Directions[0]);

            int idx = 0;
            for (int dir = 1; dir < 4; dir++)
            {
                float angle = Vector2.Angle(velocity, Directions[dir]);
                if (smallest > angle)
                {
                    idx = dir;
                    smallest = angle;
                }
            }

            velocity = Directions[idx] * speed;
        }

        // -----------------------------------------------------------------------------------	
        /// <summary>
        /// Moves the object and bounces it at exactly so it comes out at a 45
        /// degree angle. Optionally, may add a little noise to the bounce
        /// </summary>
        protected void Move()
        {
            // update position
            position += velocity * Time.fixedDeltaTime;
            x = Mathf.RoundToInt(position.x);
            y = Mathf.RoundToInt(position.y);

            // does it overlap any obstacle?
            Collider2D[] overlaps = new Collider2D[1];
            bool bounce = collider.OverlapCollider(PlayArea.ObstacleFilter, overlaps) > 0;

            // nope
            if (!bounce)
                return;

            // rewind until there's no more overlap
            Vector2 dv = velocity * Time.fixedDeltaTime * 0.1f;
            do
            {
                position -= dv;
                x = Mathf.RoundToInt(position.x);
                y = Mathf.RoundToInt(position.y);
            } while (collider.OverlapCollider(PlayArea.ObstacleFilter, overlaps) > 0);

            position -= dv;
            x = Mathf.RoundToInt(position.x);
            y = Mathf.RoundToInt(position.y);

            
            // check where do we hit the obstable to calculate normal
            float distance = speed * Time.fixedDeltaTime;
            RaycastHit2D[] hits = new RaycastHit2D[8];
            int hitCount = collider.Cast(velocity, PlayArea.ObstacleFilter, hits, distance);

            // obtain normal from contact points
            Vector2 normal = Vector3.zero;
            for (int i = 0; i < hitCount; i++)
                normal += hits[i].normal;
            normal.Normalize();

            // reflect velocity
            Vector2 before = velocity;
            velocity = Vector2.Reflect(velocity, normal).normalized * speed;

            // clamp if necessary
            if (lock45)
                ClampVelocity45();

            // velocity hasn't changed enough!
            if (Vector2.Angle(before, velocity) < 2.5f)
                velocity *= -1;
        }
    }

}