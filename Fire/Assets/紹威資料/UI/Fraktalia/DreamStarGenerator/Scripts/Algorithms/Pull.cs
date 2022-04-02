using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fraktalia.DreamStarGen.Algorithms
{
    public class Pull : BasicAlgorithm
    {
        public Transform Target;
        public Vector3 TargetPoint;
        public AnimationCurve ImpactCurve;
        public float MaxDistance;


        public override void Initialize(DreamStarGenerator_Mixer generator)
        {
            if (Target)
            {
                TargetPoint = transform.InverseTransformPoint(Target.position);
            }
        }

        public override Vector3 StarAlgorithm(float Angle, DreamStarGenerator_Mixer generator, Vector3 lastPosition)
        {
            float MaxDistSqrt = MaxDistance * MaxDistance;

            Angle *= Angle_MP;
            float r = 0;
            float radiant = Angle * Mathf.Deg2Rad * generator.a;
            float value1 = 1;
            r = generator.Radius * (value1);


            float x = 0;
            float y = 0;

            float dist = (TargetPoint.x - lastPosition.x) * (TargetPoint.x - lastPosition.x) + (TargetPoint.y - lastPosition.y) * (TargetPoint.y - lastPosition.y);

            float lerp = Mathf.InverseLerp(0, MaxDistSqrt, dist);
            
            x += TargetPoint.x * ImpactCurve.Evaluate(lerp) * Impact;
            y += TargetPoint.y * ImpactCurve.Evaluate(lerp) * Impact;


            return new Vector3(x, y, 0);

        }



    }
}