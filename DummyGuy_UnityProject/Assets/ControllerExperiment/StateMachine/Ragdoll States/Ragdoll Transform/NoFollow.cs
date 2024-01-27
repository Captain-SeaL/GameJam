﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class NoFollow : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            subComponentProcessor.SetEntity(SetRagdoll.INSTANT_ROTATE_ENTITY);

            int t = subComponentProcessor.GetInt(RagdollInt.RAGDOLL_TRANSFORM_STATE);

            if (t == (int)RagdollTransformState.INSTANT_FOLLOW)
            {
                stateProcessor.TransitionTo(typeof(InstantFollowPlayerController));
            }
        }
    }
}