using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
  public static event Action<PlayerInput> PlayerJoined;
  public static event Action<PlayerInput> PlayerLeft;
  public static Dictionary<int, PlayerInput> Players { get; private set; } = new Dictionary<int, PlayerInput>();

  public static bool HasPlayer(int playerId)
  {
    return Players.ContainsKey(playerId);
  }

  public void OnPlayerJoined(PlayerInput playerInput)
  {
    if(Players.TryAdd(playerInput.playerIndex, playerInput)){
      PlayerJoined?.Invoke(playerInput);
    }
  }
  public void OnPlayerLeft(PlayerInput playerInput)
  {
    Players.Remove(playerInput.playerIndex);
    PlayerLeft?.Invoke(playerInput);
  }
}