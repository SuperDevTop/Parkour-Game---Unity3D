using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Climbing
{
    public class VaultOver : VaultAction
    {
        public VaultOver(ThirdPersonController _vaultingController, Action action) : base(_vaultingController, action)
        {
        }


        /// <summary>
        /// Checks if can Vault the Box Obstacle
        /// </summary>
        public override bool CheckAction()
        {
            if (controller.characterInput.jump && !controller.isVaulting)
            {
                RaycastHit hit;
                Vector3 origin = controller.transform.position + kneeRaycastOrigin;

                //Checks if Vault obstacle in front
                if (controller.characterDetection.ThrowRayOnDirection(origin, controller.transform.forward, kneeRaycastLength, out hit))
                {
                    // If direction not the same as object don't do anything
                    // or angle of movement not valid
                    if ((hit.normal == hit.collider.transform.forward ||
                        hit.normal == -hit.collider.transform.forward) == false ||
                        Mathf.Abs(Vector3.Dot(-hit.normal, controller.transform.forward)) < 0.60 ||
                        hit.transform.tag != tag)
                        return false;

                    //Gets Box width and adds an offset for the downward ray
                    Vector3 origin2 = origin + (-hit.normal * (hit.transform.localScale.z + landOffset));

                    //Get landing position and set target position
                    RaycastHit hit2;
                    if (controller.characterDetection.ThrowRayOnDirection(origin2, Vector3.down, 10, out hit2))
                    {
                        if (hit2.collider)
                        {
                            controller.characterAnimation.animator.CrossFade("Deep Jump", 0.05f);

                            startPos = controller.transform.position;
                            startRot = controller.transform.rotation;
                            targetPos = hit2.point;
                            targetRot = Quaternion.LookRotation(targetPos - startPos);
                            vaultTime = startDelay;
                            animLength = 0.85f;
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
            if (controller.isVaulting)
            {
                float actualSpeed = Time.deltaTime / animLength;
                vaultTime += actualSpeed * animator.animState.speed;

                if (vaultTime >= 1)
                {
                    controller.EnableController();
                }
                else
                {
                    if (vaultTime >= 0)
                    {
                        controller.transform.rotation = Quaternion.Lerp(startRot, targetRot, vaultTime * 4);
                        controller.transform.position = Vector3.Lerp(startPos, targetPos, vaultTime);
                    }
                    return true;
                }
            }

            return false;
        }

        public override void DrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(targetPos, 0.08f);
        }
    }
}
