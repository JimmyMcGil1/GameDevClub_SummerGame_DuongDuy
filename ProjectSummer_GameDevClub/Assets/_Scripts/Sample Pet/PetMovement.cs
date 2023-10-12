using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Pathfinding;
using UnityEngine.U2D.Animation;

namespace Units.Pet
{
    public class PetMovement : MonoBehaviour
    {
        public AIPath aiFinding;
        public CircleCollider2D circleBox;
        public Transform followPos;
        bool closeToMaster = false;
        PetController _petController;
        public PetController petController => _petController;
        [SerializeField] GameObject master;
        [SerializeField] SkeletonAsset graphix;
        [SerializeField] Transform firePos;
        public bool mouseClick = false;
        public Transform bourbon {get; private set;}
        private void Awake()
        {
            _petController = gameObject.GetComponent<PetController>();
            aiFinding = GetComponent<AIPath>();
            circleBox = GetComponent<CircleCollider2D>();
            followPos = GameObject.FindGameObjectWithTag("Bourbon").transform.Find("posFollowedByPet");
            bourbon = GameObject.FindGameObjectWithTag("Bourbon").transform;
        }
        private void Start()
        {
         //   aiFinding.canMove = false;
        }
        
        private void LateUpdate()   
        {
            mouseClick = false;
        }
        private void OnMouseDown()
        {
            transform.DOScale(new Vector2(1, 0.7f), 0.1f);
        }
        private void OnMouseUp()
        {
            transform.DOScale(new Vector2(1, 1f), 0.1f);

        }
        
        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(circleBox.bounds.center, followPos.position);
            Gizmos.DrawWireSphere(circleBox.bounds.center, aiFinding.endReachedDistance);
        }
    }
}
