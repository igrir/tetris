using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameSettingsInstaller : ScriptableObject
{

    public GameObject TetrisBlockPrefab;
    public GameObject TetrisGameControllerPrefab;
    public int BoardRow;
    public int BoardCol;


    [MenuItem("Assets/Create/GameSettingsInstallers")]
    public static void CreateInstaller()
    {
        GameSettingsInstaller asset = ScriptableObject.CreateInstance<GameSettingsInstaller>();

        AssetDatabase.CreateAsset(asset, "Assets/GameSettingsInstaller.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    public static GameSettingsInstaller InstallFromResource(string gameSettingLocation)
    {
        var gameSettingsInstaller = Resources.Load(gameSettingLocation) as GameSettingsInstaller;

        return gameSettingsInstaller;
    }

}
