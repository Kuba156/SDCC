using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CharacterControl
{
    [CreateAssetMenu(menuName = "Character", fileName = "CharacterInformation")]
    public class CharacterInformation : ScriptableObject
    {
        [SerializeField]
        private AnimationCurve _gravityCurve;

        [SerializeField]
        private AnimationCurve _movementCurve;

        [SerializeField]
        private AnimationCurve _jumpingCurve;

        [SerializeField]
        private AnimationCurve _wallrunHorizontalCurve;

        [SerializeField]
        private AnimationCurve _wallrunVerticalCurve;


        public AnimationCurve gravityCurve => _gravityCurve;
        public AnimationCurve movementCurve => _movementCurve;
        public AnimationCurve jumpingCurve => _jumpingCurve;
        public AnimationCurve wallrunHorizontalCurve => _wallrunHorizontalCurve;
        public AnimationCurve wallrunVerticalCurve => _wallrunVerticalCurve;

    }

}
