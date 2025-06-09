/// <summary>
/// 인벤토리 아이템의 저장용 데이터 클래스입니다.<br/>
/// - JSON 등으로 저장될 때 필요한 최소한의 정보만 담고 있습니다.<br/>
/// - 아이템의 고유 ID와 장착 여부를 저장합니다.
/// </summary>
[System.Serializable]
public class ItemSaveData
{
    public string itemid;
    public bool isEquipped;

    /// <summary>
    /// 아이템 저장 데이터를 생성합니다.
    /// </summary>
    /// <param name="itemid">아이템 고유 ID</param>
    /// <param name="isEquipped">장착 여부</param>
    public ItemSaveData(string itemid, bool isEquipped)
    {
        this.itemid = itemid;
        this.isEquipped = isEquipped;
    }
}
