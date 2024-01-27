using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class RotateRagdollEntity : BaseSubComponent
    {
        [Header("Rotation Attributes")]
        [SerializeField] GameObject TargetRotationObj;
        [SerializeField] float DesiredYRotation;

        [Header("Rotation Debug")]
        [SerializeField] Rigidbody RootPivot;

        private void Start()
        {
            subComponentProcessor.DelegateSetEntity(SetRagdoll.INSTANT_ROTATE_ENTITY, InstantRotateEntity);
            RootPivot = RagdollPartFinder.GetRootJoint(subComponentProcessor.owner);
        }

        public override void OnFixedUpdate()
        {
            if (TargetRotationObj != null)
            {
                DesiredYRotation = TargetRotationObj.transform.rotation.eulerAngles.y;
            }
        }

        void InstantRotateEntity()
        {
            subComponentProcessor.owner.rbody.MoveRotation(Quaternion.Euler(0, DesiredYRotation, 0f));
            
            // make sure root pivot is aligned with the root obj
            RootPivot.MoveRotation(Quaternion.Euler(0, DesiredYRotation, 0f));
        }
    }
}