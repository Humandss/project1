using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

        Transform player;

        public float ApproachRange { get; set; }

        public float AttackRange { get; set; }

        public Vector3 SpawnLocation { get; private set;}

        public float Hp { get; set; }

        Renderer rend;
            
        State state;
        //이벤트 큐
        private readonly Queue<EnemyEventType> events = new Queue<EnemyEventType>();
        public void TriggerEvent(EnemyEventType type) => events.Enqueue(type);
        public bool HasEvents => events.Count > 0;
        public EnemyEventType DequeueEvent() => events.Dequeue();

        public void ChangeColor(Color newColor)
        {
                rend.material.SetColor("_Color", newColor);
        }

        
        void Start()
        {
                AttackRange = 5f;
                ApproachRange = 10f;
                Hp = 100.0f;

                SpawnLocation = transform.position;
                

                rend = GetComponent<Renderer>();
                
                player = GameObject.Find("Player").transform;
                //콜라이더 생성
                CreateRangeSphereCollider("AttackRangeCollider", AttackRange, EnemyEventType.EnterAttackRange, EnemyEventType.ExitAttackRange);
                CreateRangeSphereCollider("ApproachRangeCollider", ApproachRange, EnemyEventType.EnterApproachRange, EnemyEventType.ExitApproachRange);

                state = new IdleState(this, player);
                state.Enter();
        }


        private void Update()
        {
                State state_ = state.HandleInput();
                if (state_ != null)
                {
                    state.Exit();
                    state = state_;
                    state.Enter();
                }
                state.DoAction();
        }
        //스피어 콜라이더 생성 함수
        private void CreateRangeSphereCollider(string name, float range, EnemyEventType enterEvent, EnemyEventType extiEvent)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(this.transform, false);

            SphereCollider collider = go.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.enabled = true;
            collider.radius = range;
            //각 콜라이더에 트리거 등록
            EventTriggerManager trigger = go.AddComponent<EventTriggerManager>();
            trigger.Register(player.gameObject, this, enterEvent, extiEvent);
              
        }

        public void AttackPlayer()
        {
                Debug.Log("Enemy attacks player!");
        }

        public void GetDamage(float damage)
        {
                ChangeColor(Color.magenta);
                Hp -= damage;
                if (Hp < 0f) Hp = 0f;
        }

        // 기존 APIs
        public bool IsPlayerInApproachRange()
        {
                return Vector3.Distance(player.position, transform.position) <= ApproachRange;
        }

        public bool IsPlayerOutOfApproachRange()
        {
                return Vector3.Distance(player.position, transform.position) > ApproachRange;
        }

        public bool IsPlayerInAttackRange()
        {
                return Vector3.Distance(player.position, transform.position) <= AttackRange;
        }

        public bool IsPlayerOutOfAttackRange()
        {
                return Vector3.Distance(player.position, transform.position) > AttackRange;
        }

        public bool IsInSpawnLocation()
        {
                return Vector3.Distance(transform.position, SpawnLocation) <= Mathf.Epsilon;
        }

        public bool IsInTargetLocation(Vector3 targetLocation)
        {
                return Vector3.Distance(transform.position, targetLocation) <= Mathf.Epsilon;
        }

        public Vector3 GetOppositeDirectionFromPlayer()
        {
                return transform.position + (transform.position - player.position).normalized * 5f;
        }

        public Vector3 GetSpawnLocation()
        {
                return SpawnLocation;
        }

        public void MoveTowards(Vector3 target, float step)
        {
                transform.position = Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
        }

        public void MoveTowardsPlayer(float step)
        {
                transform.position = Vector3.MoveTowards(transform.position, player.position, step * Time.deltaTime);
        }

        public void MoveTowardsSpawnLocation(float step)
        {
                transform.position = Vector3.MoveTowards(transform.position, SpawnLocation, step * Time.deltaTime);
        }
}

