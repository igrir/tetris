using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<TetrisPieceFactory>().AsSingle();
    }
}