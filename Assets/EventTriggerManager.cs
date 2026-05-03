using UnityEngine;

public enum EnemyEventType
{
    EnterAttackRange,
    ExitAttackRange,
    EnterApproachRange,
    ExitApproachRange,
    None

}
public class EventTriggerManager : MonoBehaviour
{
    private GameObject target;
    private Enemy enemy;
    private EnemyEventType enterEvent = EnemyEventType.None;
    private EnemyEventType exitEvent = EnemyEventType.None;
    public void Register(GameObject target, Enemy enemy, EnemyEventType enterEvent, EnemyEventType exitEvent)
    {
        this.target = target;
        this.enemy = enemy;
        this.enterEvent = enterEvent;
        this.exitEvent = exitEvent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null || other.gameObject != target) return;
        enemy.TriggerEvent(enterEvent);
        Debug.Log($"enterEvent : {enterEvent}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null || other.gameObject != target) return;
        enemy.TriggerEvent(exitEvent);
        Debug.Log($"exitEvent : {exitEvent}");
    }
}
