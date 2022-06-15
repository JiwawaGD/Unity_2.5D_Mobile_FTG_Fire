using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fraktalia.DreamStarGen.Algorithms
{
    public class Crazy : BasicAlgorithm
    {  

        public override Vector3 StarAlgorithm(float Angle, DreamStarGenerator_Mixer generator, Vector3 lastPosition)
        {
            Angle *= Angle_MP;

            float r = 0;

            float radiant = Angle * generator.a;
            float alternator = Mathf.Cos(radiant / (generator.b / 1000));


            float value1 = 1 * alternator;


            r = generator.Radius * (value1);

            float x = r * Mathf.Cos(Angle);
            float y = r * Mathf.Sin(Angle);
            return new Vector3(x, y, 0) * Impact;
        }


    }
}