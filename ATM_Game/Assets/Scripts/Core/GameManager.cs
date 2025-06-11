using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <b>게임 전체의 흐름과 상태를 관리하는 핵심 매니저 클래스입니다.</b><br/>
/// - 싱글톤(Singleton)으로 구성되어 전역에서 접근 가능합니다.<br/>
/// - 게임 상태 전환(타이틀, 인게임, 일시정지 등)을 관리합니다.<br/>
/// - UI 및 씬 매니저와의 연결 지점 역할도 수행할 수 있습니다.
/// </summary>
public class GameManager : MonoSingleton<GameManager>
{
    [Header("초기 아이템들")]
    public List<ItemData> initialItems;

    /// <summary>
    /// 현재 게임 상태를 나타냅니다. (예: Title, InGame, Pause 등)
    /// </summary>
    public GameState CurrentState { get; private set; } = GameState.None;

    /// <summary>
    /// 유저 데이터 관련 기능을 담당하는 매니저입니다. (의존성 주입 가능)
    /// </summary>
    public IUserDataManager UserDataManager { get; private set; }

    /// <summary>
    /// 현재 플레이어의 캐릭터 정보를 담고 있는 객체입니다.
    /// </summary>
    public Character PlayerCharacter { get; private set; }


    /// <summary>
    /// 게임 매니저의 초기화 작업을 수행합니다.<br/>
    /// 싱글톤으로서 중복 방지를 처리하고, 유저 데이터를 불러옵니다.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        var storage = new JsonUserDataStorage();
        var factory = new CharacterFactory(initialItems);

        InjectUserDataManager(new UserDataManager(storage, factory));
    }

    /// <summary>
    /// 게임이 시작될 때 호출되는 Unity 이벤트 함수입니다.<br/>
    /// 초기 상태를 타이틀 화면으로 전환합니다.
    /// </summary>
    private void Start()
    {
        ChangeState(GameState.Title);
    }

    /// <summary>
    /// 게임의 상태를 변경합니다.<br/>
    /// 현재 상태와 같은 경우 무시되며, 상태 전환 시 GameStateHandler를 통해 처리됩니다.
    /// </summary>
    /// <param name="newState">새로 설정할 게임 상태</param>
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        GameStateHandler.Handle(newState);
    }

    /// <summary>
    /// 유저 데이터를 기반으로 플레이어 캐릭터를 생성하고 설정합니다.
    /// </summary>
    public void SetPlayerCharacter(UserData user)
    {
        PlayerCharacter = new Character(user);
    }

    /// <summary>
    /// 현재 플레이어 캐릭터 데이터를 유저 데이터에 반영하고 저장합니다.<br/>
    /// 캐릭터 또는 유저 정보가 없을 경우 저장을 생략합니다.
    /// </summary>
    public void SaveGame()
    {
        if (PlayerCharacter == null || UserDataManager.CurrentUser == null)
        {
            return;
        }

        UserDataManager.CurrentUser.SaveFromCharacter(PlayerCharacter);

        UserDataManager.SaveUserData();
    }

    /// <summary>
    /// 외부에서 UserDataManager를 주입받아 초기화하고, 플레이어 캐릭터를 설정합니다.<br/>
    /// 테스트나 확장 구조에서 대체 구현체 주입이 가능합니다.
    /// </summary>
    public void InjectUserDataManager(IUserDataManager userDataManager)
    {
        UserDataManager = userDataManager;
        UserDataManager.Init();

        if (UserDataManager.CurrentUser != null)
        {
            SetPlayerCharacter(UserDataManager.CurrentUser);
        }
    }

    /// <summary>
    /// 게임을 종료합니다.<br/>
    /// 에디터에서는 실행 모드를 종료하고, 빌드된 게임에서는 실제로 종료합니다.
    /// </summary>
    public void QuitGame()
    {
# if UNITY_EDITOR
        // Unity 에디터 상에서 실행 중인 경우 에디터 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서는 실제 게임 종료
        Application.Quit();
#endif
    }
}
