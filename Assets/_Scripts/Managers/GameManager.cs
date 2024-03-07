using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    [SerializeField] private GameController _gameControllerPrefab;
    [SerializeField] private BackGround _bg;
    public GameController Controller => _controller;
    private GameController _controller;

    private void Awake()
    {
        if (DataManager.Instance.LoadGameData())
        {
            _controller = Instantiate(_gameControllerPrefab);
            StartCoroutine(WaitIilInit(() => {
            _bg.SetObjectTrans(_controller.Player.transform);
            }));
        }
    }
    IEnumerator WaitIilInit(Action onComplete)
    {
        yield return new WaitForEndOfFrame();
        onComplete?.Invoke();
    }
}
