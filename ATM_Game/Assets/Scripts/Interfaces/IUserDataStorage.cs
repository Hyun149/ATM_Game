using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserDataStorage
{
    UserDataList Load();
    void Save(UserDataList data);
}
