using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

/// <summary>
/// <b>이벤트 키 문자열을 기반으로 이벤트를 등록/호출하는 범용 이벤트 매니저입니다.</b><br/>
/// - <b>StartListening</b>: 이벤트에 리스너(메서드)를 등록합니다.<br/>
/// - <b>StopListening</b>: 등록된 이벤트 리스너를 제거합니다.<br/>
/// - <b>TriggerEvent</b>: 특정 이벤트를 트리거하여 등록된 메서드들을 실행합니다.<br/>
/// - <b>ClearAllEvents</b>: 전체 이벤트 딕셔너리를 초기화합니다.
/// </summary>
public class EventManager : MonoSingleton<EventManager>
{
    private Dictionary<string, Action> eventDicionary = new Dictionary<string, Action>();  // 이벤트 이름과 연결된 델리게이트 저장소

    /// <summary>
    /// 특정 이벤트에 메서드를 리스너로 등록합니다.<br/>
    /// - 이벤트 키가 이미 존재하면 델리게이트에 메서드를 추가하고,<br/>
    /// - 존재하지 않으면 새롭게 키와 메서드를 추가합니다.
    /// </summary>
    /// <param name="eventName">이벤트 키 문자열</param>
    /// <param name="listener">등록할 메서드 (Action)</param>
    public static void StartListening(string eventName, Action listener)
    {
        if (Instance == null) return;

        if (Instance.eventDicionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent += listener;
            Instance.eventDicionary[eventName] = thisEvent;
        }
        else
        {
            Instance.eventDicionary[eventName] = listener;
        }
    }

    /// <summary>
    /// 특정 이벤트에서 지정한 리스너(메서드)를 제거합니다.<br/>
    /// - 제거 후 해당 이벤트에 리스너가 없으면 딕셔너리에서 삭제합니다.
    /// </summary>
    /// <param name="eventName">이벤트 키 문자열</param>
    /// <param name="listener">제거할 메서드 (Action)</param>
    public static void StopListening(string eventName, Action lisener)
    {
        if (Instance == null) return;

        if (Instance.eventDicionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent -= lisener;
            if (thisEvent == null)
            {
                Instance.eventDicionary.Remove(eventName);
            }
            else
            {
                Instance.eventDicionary[eventName] = thisEvent;
            }
        }
    }

    /// <summary>
    /// 지정한 이벤트 키에 해당하는 이벤트를 호출(Trigger)합니다.<br/>
    /// - 등록된 모든 리스너 메서드가 순차적으로 실행됩니다.
    /// </summary>
    /// <param name="eventName">트리거할 이벤트 키 문자열</param>
    public static void TriggerEvent(string eventName)
    {
        if (Instance == null) return;

        if (Instance.eventDicionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    /// <summary>
    /// 모든 이벤트 리스너를 초기화합니다.<br/>
    /// - 주로 씬 전환 시 호출되어 이전 씬의 이벤트 참조를 제거하는 데 사용됩니다.
    /// </summary>
    public static void ClearAllEvents()
    {
        if (Instance == null) return;

        Instance.eventDicionary.Clear();
    }
}
