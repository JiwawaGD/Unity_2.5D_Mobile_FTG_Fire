using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Fraktalia.DreamStarGen.Algorithms;

namespace Fraktalia.DreamStarGen
{
    [RequireComponent(typeof(MeshFilter))]
    public class DreamStarGenerator_Mixer : DreamStarGenerator
    {

        public AnimationCurve Curve_A;
        public AnimationCurve Curve_B;
        public AnimationCurve Curve_C;

        [HideInInspector]
        public BasicAlgorithm[] Algorithm;


        public override void Initialize()
        {
            Algorithm = GetComponents<BasicAlgorithm>();

            if (Algorithm == null)
            {
                suspendGeneraton = true;
                Algorithm = new BasicAlgorithm[0];
            }
            for (int i = 0; i < Algorithm.Length; i++)
            {
                if (Algorithm[i] == null)
                {
                    suspendGeneraton = true;
                }

                Algorithm[i].Initialize(this);
            }
        }

        public override Vector3 StarAlgorithm(float Angle)
        {
            Vector3 output = new Vector3();
            for (int i = 0; i < Algorithm.Length; i++)
            {
                output += Algorithm[i].StarAlgorithm(Angle, this, output);
            }
            return output;
        }

    }
}