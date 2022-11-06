﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Climbing
{
    public class VaultSlide : VaultAction
    {
        private float dis;
        public VaultSlide(ThirdPersonController _vaultingController, Action _actionInfo) : base(_vaultingController, _actionInfo)
        {
        }

        
        /// <summary>
        /// Checks if Player can Slide the Obstacle
        /// </summary>
        public override bool CheckAction()
        {
            if (controller.characterInput.drop && !controller.isVaulting)
            {
                RaycastHit hit;
                Vector3 origin = controller.transform.position + kneeRaycastOrigin;

                //Finds Obstacle
                if (controller.characterDetection.ThrowRayOnDirection(origin, controller.transform.forward, kneeRaycastLength, out hit))
                {
                    Vector3 origin2 = origin + (-hit.normal * (hit.transform.localScale.z + landOffset));

                    // If direction not the same as object don't do anything
                    // or angle of movement not valid
                    if ((hit.normal == hit.collider.transform.forward ||
                        hit.normal == -hit.collider.transform.forward) == false ||
                        Mathf.Abs(Vector3.Dot(-hit.normal, controller.transform.forward)) < 0.60 ||
                        hit.transform.tag != tag)
                        return false;

                    RaycastHit hit2;
                    //Get ending position
                    if (controller.characterDetection.ThrowRayOnDirection(origin2, Vector3.down, 10, out hit2)) //Ground Hit
                    {
                        if (hit2.collider)
                        {
                            controller.characterAnimation.animator.CrossFade("Running Slide", 0.05f);
                            dis = 4 / Vector3.Distance(startPos, targetPos);
                            controller.characterAnimation.animator.SetFloat("AnimSpeed", dis);
                            controller.characterAnimation.switchCameras.SlideCam();

                            startPos = controller.transform.position;
                            startRot = controller.transform.rotation;
                            targetPos = hit2.point;
                            targetRot = Quaternion.LookRotation(targetPos - startPos);
                            vaultTime = startDelay;
                            animLength = clip.length + startDelay;
                            controller.DisableController();

                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Executes Vaulting Animation
        /// </summary>
        public override bool Update()
        {
            bool ret = false;
            if (controller.isVaulting)
            {
                float actualSpeed = Time.deltaTime / animLength;
                vaultTime += actualSpeed * (animator.animState.speed + dis);

                if (vaultTime > 1)
                {
                    controller.characterAnimation.animator.SetFloat("AnimSpeed",1);
                    controller.characterAnimation.switchCameras.FreeLookCam();
                    controller.EnableController();
                }
                else
                {
                    controller.transform.rotation = Quaternion.Lerp(startRot, targetRot, vaultTime * 4);
                    controller.transform.position = Vector3.Lerp(startPos, targetPos, vaultTime);
                    ret = true;
                }
            }

            return ret;
        }

        public override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(targetPos, 0.08f);
        }
    }
}
