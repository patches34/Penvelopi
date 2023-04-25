using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Agent
{
    [SerializeField]
    float angleStepRange, maxAngle;

    float angle = -1;

    [SerializeField]
    float wanderScalar = 1f, boundsScalar = 1f, separateScalar = 1f;

    public override void CalculateSteeringForces()
    {
        if(angle == -1)
        {
            angle = Random.Range(0, 360);
        }
        else
        {
            angle += Random.Range(-angleStepRange, angleStepRange);

            if(angle > maxAngle)
            {
                angle = maxAngle;
            }
            else if(angle < -maxAngle)
            {
                angle = -maxAngle;
            }
        }

        //  Seek a target
        totalForces += Wander(angle);

        totalForces += StayInBounds(2f);

        totalForces += Separate();
    }
}
