using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fraktalia.DreamStarGen.Algorithms
{
    public class Circle : BasicAlgorithm
    {
       

        public override Vector3 StarAlgorithm(float Angle, DreamStarGenerator_Mixer generator, Vector3 lastPosition)
        {
            Angle *= Angle_MP;
            float r = 0;
            float radiant = Angle * Mathf.Deg2Rad * generator.a;
            float value1 = 1;
            r = generator.Radius * (value1);

            float x = r * Mathf.Cos(radiant);
            float y = r * Mathf.Sin(radiant);
            return new Vector3(x, y, 0) * Impact;

        }



    }
}