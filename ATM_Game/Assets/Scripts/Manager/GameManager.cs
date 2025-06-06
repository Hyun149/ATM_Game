using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// <b>게임 전체의 흐름과 상태를 관리하는 핵심 매니저 클래스입니다.</b><br/>
/// - 싱글톤(Singleton)으로 구성되어 전역에서 접근 가능합니다.<br/>
/// - 게임 상태 전환(타이틀, 인게임, 일시정지 등)을 관리합니다.<br/>
/// - UI 및 씬 매니저와의 연결 지점 역할도 수행할 수 있습니다.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState { get; private set; } = GameState.None; // 현재 게임의 상태를 나타냅니다.

    public UserDataManager UserDataManager { get; private set; }

    public Character PlayerCharacter { get; private set; }

    [Header("초기 아이템들")]
    public List<ItemData> initialItems;

    protected override void Awake()
    {
        base.Awake();
        UserDataManager = new UserDataManager();
        UserDataManager.Init();

        if (UserDataManager.CurrentUser != null)
        {
            SetData(UserDataManager.CurrentUser);
        }
    }

    /// <summary>
    /// 게임 시작 시 호출되는 Unity 이벤트 함수입니다.<br/>
    /// 초기 게임 상태를 Title로 설정하여 타이틀 화면 진입 흐름을 시작합니다.
    /// </summary>
    private void Start()
    {
        ChangeState(GameState.Title);
    }

    /// <summary>
    /// 게임 상태를 새 상태로 전환합니다.<br/>
    /// 중복 전환을 방지하며, 상태별 로직을 스위치로 구분하여 실행합니다.
    /// </summary>
    /// <param name="newState">전환하고자 하는 게임 상태</param>
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        Debug.Log($"[GameManager] 상태 전환됨 -> {newState}");

        GameStateHandler.Handle(newState);
        
    }

    public void SetData(UserData user)
    {
        PlayerCharacter = new Character(user);

        foreach (var itemData in initialItems)
        {
            PlayerCharacter.AddItem(itemData);
        }

        /*if (PlayerCharacter.Inventory.Count > 0)
        {
            PlayerCharacter.Inventory[0].Equip();
        }*/
    }

    public void QuitGame()
    {
        Debug.Log("[GameManager] 게임 종료 요청됨");

# if UNITY_EDITOR
        // Unity 에디터 상에서 실행 중인 경우 에디터 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서는 실제 게임 종료
        Application.Quit();
#endif
    }
}
