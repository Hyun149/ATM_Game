/// <summary>
/// 유저 데이터를 저장하고 불러오는 기능을 정의하는 인터페이스입니다.<br/>
/// - 다양한 저장 방식(파일, 클라우드 등)에 대해 공통된 접근 방식을 제공합니다.
/// </summary>
public interface IUserDataStorage
{
    UserDataList Load();
    void Save(UserDataList data);
}
