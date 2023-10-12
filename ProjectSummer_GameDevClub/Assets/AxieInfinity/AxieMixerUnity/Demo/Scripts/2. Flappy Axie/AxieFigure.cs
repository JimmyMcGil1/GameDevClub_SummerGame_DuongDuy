using AxieMixer.Unity;
using Spine;
using Spine.Unity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Game
{
    public class AxieFigure : MonoBehaviour
    {
        public string id;
        //public string genes;
        protected SkeletonAnimation skeletonAnimation;
        [SerializeField] private bool _flipX = false;

        [SerializeField] List<string> axieGenes;

        public bool flipX
        {
            get
            {
                return _flipX;
            }
            set
            {
                _flipX = value;
                if (skeletonAnimation != null)
                {
                    skeletonAnimation.skeleton.ScaleX = (_flipX ? -1 : 1) * Mathf.Abs(skeletonAnimation.skeleton.ScaleX);
                }
            }
        }

        protected void Awake()
        {
            skeletonAnimation = gameObject.GetComponent<SkeletonAnimation>();
            
            // Shouldn't be here, but it's useful
            Mixer.Init();
            Mixer.SpawnSkeletonAnimation(skeletonAnimation, id, GetRandomAxie());

            skeletonAnimation.transform.localPosition = new Vector3(0f, -0.32f, 0f);
            skeletonAnimation.transform.SetParent(transform, false);
            skeletonAnimation.transform.localScale = new Vector3(1, 1, 1);
            skeletonAnimation.skeleton.ScaleX = (_flipX ? -1 : 1) * Mathf.Abs(skeletonAnimation.skeleton.ScaleX);
            skeletonAnimation.timeScale = 0.5f;
            skeletonAnimation.skeleton.FindSlot("shadow").Attachment = null;
            skeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
            skeletonAnimation.state.End += SpineEndHandler;
        }

        protected void OnDisable()
        {
            if (skeletonAnimation != null)
            {
                skeletonAnimation.state.End -= SpineEndHandler;
            }
        }

        public void DoJumpAnim()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "action/move-forward", false);
        }

        public void DoAttackMeleeAnim()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/melee/tail-roll", false);
        }


        public void DoAttackRangedAnim()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/tail-roll", false);
        }

        public void DoBuffAnim()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "attack/ranged/cast-fly", false);
        }

        public void DoDefence()
        {
            skeletonAnimation.timeScale = 1f;
            skeletonAnimation.AnimationState.SetAnimation(0, "defense/evade", false);
        }
        protected void SpineEndHandler(TrackEntry trackEntry)
        {
            string animation = trackEntry.Animation.Name;
            if (animation == "action/move-forward")
            {
                skeletonAnimation.state.SetAnimation(0, "action/idle/normal", true);
                skeletonAnimation.timeScale = 0.5f;
            }
        }
        string GetRandomAxie()
        {
            int index = Random.Range(0, axieGenes.Count);
            return axieGenes[index];
        }
    }
}
