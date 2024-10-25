using System;
using System.Collections.Generic;
using Gameplay.Ball;
using Gameplay.Gate;
using Gameplay.Player;
using Gameplay.Spawn;
using Gameplay.Types;
using Gameplay.Views;
using Infarastructure.Network;
using Infarastructure.Services;
using Mirror;
using UnityEngine;

public class GameFactory
{
    private GameObject _player;
    private SpawnPointGroup _spawnPointGroup;
    private ScoreViewGroup _viewGroup;
    private GateGroup _gateGroup;
    private Ball _ball;
    
    public event Action AllPlayersSpawned;

    public GameFactory(SpawnPointGroup spawnPointGroup, ScoreViewGroup viewGroup, GateGroup gateGroup, Ball ball, ServiceProvider<GameFactory> gameFactoryProvider)
    {
        _spawnPointGroup = spawnPointGroup;
        _viewGroup = viewGroup;
        _gateGroup = gateGroup;
        _ball = ball;
        gameFactoryProvider.Initialize(this);
        
    }

    public Ball SpawnedBall { get; private set; }
    public List<GameObject> SpawnedPlayers{ get; private set; } = new List<GameObject>();
    public List<GameObject> SpawnAllPlayers(List<NetworkConnectionToClient> connectionToClients)
    {
        List<GameObject> players = new List<GameObject>();
        var sides = (SideType[]) Enum.GetValues(typeof(SideType));
        
        for (int i = 0; i < NetworkServer.maxConnections; i++)
        {
            var player = SpawnPlayer(connectionToClients[i], sides[i]);
            players.Add(player);
        }
        
        AllPlayersSpawned?.Invoke();
        return players;
    }

    public GameObject SpawnPlayer(NetworkConnectionToClient connection, SideType sideType)
    {
        var spawnPoint = _spawnPointGroup.GetByType(sideType);


        var view = _viewGroup.GetByType(sideType);

        GameObject instantiatedPlayer = GameObject.Instantiate(NetworkManager.singleton.playerPrefab, spawnPoint.SpawnPoint.position, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(connection, instantiatedPlayer);

        var playerScore = instantiatedPlayer.GetComponent<PlayerScore>();

        view.Initialize(playerScore);

        _player = instantiatedPlayer;

        if (sideType == SideType.Left)
        {
            playerScore.Initialize(_gateGroup.GetByType(SideType.Right),connection);
        }
        else
        {
            playerScore.Initialize(_gateGroup.GetByType(SideType.Left),connection);
        }

        SpawnedPlayers.Add(instantiatedPlayer);
        return instantiatedPlayer;
    }
    
    public void SpawnBall()
    {
        var ball = GameObject.Instantiate(_ball);
        NetworkServer.Spawn(ball.gameObject);

        SpawnedBall = ball;
    }
}