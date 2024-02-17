using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMono<GameController>
{
    // New input system ref
    public GameInput Input => _gameInput;
    [SerializeField] private GameInput _gameInput;
}
