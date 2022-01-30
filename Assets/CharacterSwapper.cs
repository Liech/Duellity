using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSwapper : MonoBehaviour
{
    public GameObject defaultSkin;

    private GameObject _activeSkin;
    private PlayerBehavior _playerBehavior;

    private void Awake()
    {
        _playerBehavior = GetComponent<PlayerBehavior>();
        _activeSkin = defaultSkin;
        Random.InitState(DateTime.Now.Millisecond);
        ChooseRandom();
    }

    private void OnEnable()
    {
        ChooseRandom();
    }

    private void ChooseRandom()
    {
        ChooseCharacter(Random.Range(0, 6));
    }

    public void ChooseCharacter(int i)
    {
        var skin = gameObject.transform.GetChild(i).gameObject;
        if (!skin.name.StartsWith("SM_")) skin = defaultSkin;
        if (!skin.activeSelf) SetSkin(skin);
    }

    private void SetSkin(GameObject skin)
    {
        _activeSkin.SetActive(false);
        _activeSkin = skin;
        _activeSkin.SetActive(true);

        _playerBehavior.animator = _activeSkin.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}