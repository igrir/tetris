using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{

    TetrisBoard tetrisBoard;

    public override void InstallBindings()
    {
        var gameSettings = GameSettingsInstaller.InstallFromResource("GameSettingsInstaller") as GameSettingsInstaller;

        this.tetrisBoard = new TetrisBoard(gameSettings.BoardRow, gameSettings.BoardCol);

        Container.Bind<GameObject>().WithId("TetrisBlockPrefab").FromInstance(gameSettings.TetrisBlockPrefab);
        Container.Bind<float>().WithId("IterateTestWaitTime").FromInstance(0.1f);
        Container.Bind<TetrisBoard>().FromInstance(tetrisBoard).AsSingle();
        Container.BindFactory<TetrisPiece, TetrisPiece.Factory>();
    }
}