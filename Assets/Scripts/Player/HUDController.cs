using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HUDController : MonoSingleton<HUDController>
{
    GameObject _placeHolder;
    [SerializeField] GameObject TurretInPool1; //CHANGE IT!
    [SerializeField] GameObject TurretInPool2; //CHANGE IT!

    [Header("Text Containers")]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Text _livesCount, _wavesCount, _warFundCount, _healthCurrentStatus;

    [Header("Images Containers")]
    [SerializeField] Image fastForward;
    [SerializeField] Image slowForward, playPause;

    [Header("Button Sprites arrays")]

    [SerializeField] Sprite[] _playButtonSprites;
    [SerializeField] Sprite[] _fastFowardButtonSprites;

    [Header("UI Status Coloration Presets")]
    [SerializeField] Color[] _UIColors = { Color.white, Color.white, Color.white };

    [Header("War Funds Animation Settings")]
    [SerializeField] float warFundAnimationTime = 1.5f;
    float desiredFunds, initialFunds, currentFunds;

    //--- IENumerator Init ---//
    Coroutine instCountDown = null;

    //ControlScheme controlScheme;

    public static Action<bool> PlacingTurret;
    public static Action<Color> UIColoration;


    private void Start()
    {
        currentFunds = GameManager.Instance.WarFunds;
        initialFunds = currentFunds;
        desiredFunds = currentFunds;
        UpdateHUD();
    }

    // Update is called once per frame
    void Update()
    {
        if (_placeHolder != null)
            PlaceHolderFolowing();
        WarFundAnimation();
    }

    private void PlaceHolderFolowing()
    {
        if (_placeHolder != null)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition); //controlScheme.UI.Point.ReadValue<Vector2>()
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.transform.tag == "TurretSpot")
                {

                    _placeHolder.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + .5f, hitInfo.transform.position.z);

                    if (Input.GetMouseButtonDown(0))//controlScheme.UI.Click.ReadValue<bool>()
                    {
                        if (_placeHolder.name == "Gatling_Gun_PlaceHolder(Clone)")
                        {
                            hitInfo.transform.gameObject.GetComponent<TurretSpot>().PlaceTower(TurretInPool1);
                        }
                        else
                        {
                            hitInfo.transform.gameObject.GetComponent<TurretSpot>().PlaceTower(TurretInPool2);
                        }
                    }
                }
                else
                {
                    _placeHolder.transform.position = hitInfo.point;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_placeHolder != null)
            {
                PlacingTurret?.Invoke(false);
                Destroy(_placeHolder.gameObject);
            }
        }
    }

    public void BuyTurret(GameObject turret)
    {
        PlacingTurret?.Invoke(true);

        if (_placeHolder != null)
        {
            if (turret.name != _placeHolder.name)
            {
                Destroy(_placeHolder);
                _placeHolder = Instantiate(turret);
            }
        }
        else
        {
            _placeHolder = Instantiate(turret);
        }
    }

    public void TimePause(Image buttonPlay)
    {
        if (Time.timeScale == 0)
        {
            GameManager.Instance.ChangeTimeScale();
            buttonPlay.sprite = _playButtonSprites[0];
        }
        else
        {
            buttonPlay.sprite = _playButtonSprites[1];
            GameManager.Instance.ChangeTimeScale(0);
        }
    }

    public void TimeSpeed()
    {
        GameObject buttonObj = EventSystem.current.currentSelectedGameObject;
        Image buttonImg = buttonObj.GetComponent<Image>();
        if (_fastFowardButtonSprites.Length >= 5)
        {
            if (buttonObj.name == "FastForward")
            {
                slowForward.sprite = _fastFowardButtonSprites[0];
                slowForward.transform.rotation = Quaternion.Euler(0, 0, 180);

                if (buttonImg.sprite.name == _fastFowardButtonSprites[0].name)
                {
                    GameManager.Instance.ChangeTimeScale(2);
                    buttonImg.sprite = _fastFowardButtonSprites[1];
                }
                else
                if (buttonImg.sprite.name == _fastFowardButtonSprites[1].name)
                {
                    GameManager.Instance.ChangeTimeScale(3);
                    buttonImg.sprite = _fastFowardButtonSprites[2];

                }
                else
                if (buttonImg.sprite.name == _fastFowardButtonSprites[2].name)
                {
                    GameManager.Instance.ChangeTimeScale(4);
                    buttonImg.sprite = _fastFowardButtonSprites[3];

                }
                else
                if (buttonImg.sprite.name == _fastFowardButtonSprites[3].name)
                {
                    GameManager.Instance.ChangeTimeScale(1);
                    buttonImg.sprite = _fastFowardButtonSprites[0];
                }
            }
            else if (buttonObj.name == "SlowForward")
            {
                fastForward.sprite = _fastFowardButtonSprites[0];

                if (buttonImg.sprite.name == _fastFowardButtonSprites[4].name)
                {
                    GameManager.Instance.ChangeTimeScale(1);
                    buttonImg.sprite = _fastFowardButtonSprites[0];
                    buttonObj.transform.Rotate((Vector3.forward * 180));
                }
                else
                {
                    GameManager.Instance.ChangeTimeScale(0.5f);
                    buttonImg.sprite = _fastFowardButtonSprites[4];
                    buttonObj.transform.Rotate((Vector3.forward * 180));
                }
            }
        }
    }

    public void UpdateHUD(string nameToUpdate = null)
    {
        switch (nameToUpdate)
        {
            case "lives":
                _livesCount.text = GameManager.Instance.Health.ToString();
                HealthStatus();
                break;

            case "waves":
                _wavesCount.text = SpawnerManager.Instance.CurrentWave.ToString();
                break;

            case "funds":
                _warFundCount.text = Mathf.RoundToInt(currentFunds).ToString();
                break;

            default:
                _livesCount.text = GameManager.Instance.Health.ToString();
                _wavesCount.text = (SpawnerManager.Instance.CurrentWave + "/" + SpawnerManager.Instance.SubWaveInCurrentWave);
                _warFundCount.text = currentFunds.ToString().ToString();
                HealthStatus();
                break;
        }
    }

    void HealthStatus()
    {
        int curretnHealth = GameManager.Instance.Health;
        if (curretnHealth > 60)
        {
            _healthCurrentStatus.text = "Good";
            UIColoration?.Invoke(_UIColors[0]);
        }
        else if (curretnHealth > 20)
        {
            _healthCurrentStatus.text = "Damaged";
            UIColoration?.Invoke(_UIColors[1]);
        }
        else if (curretnHealth > 0)
        {
            _healthCurrentStatus.text = "DANGER";
            UIColoration?.Invoke(_UIColors[2]);
        }
        else
        {
            _healthCurrentStatus.text = "Destroyed";
            UIColoration?.Invoke(_UIColors[3]);
        }
    }

    public void ResetLevel()
    {
        // Add Confirmation and Delay
        GameManager.Instance.ResetGameSettings();

        fastForward.sprite = _fastFowardButtonSprites[0];

        slowForward.sprite = _fastFowardButtonSprites[0];
        slowForward.transform.rotation = Quaternion.Euler(0, 0, 180);

        if (Time.timeScale == 0)
        {
            GameManager.Instance.ChangeTimeScale();
            playPause.sprite = _playButtonSprites[0];
        }
    }

    public void UpdateWarFund()
    {
        initialFunds = currentFunds;
        desiredFunds = GameManager.Instance.WarFunds;
    }

    void WarFundAnimation()
    {
        if (currentFunds != desiredFunds)
        {
            if (initialFunds < desiredFunds)
            {
                currentFunds += (warFundAnimationTime * Time.fixedDeltaTime) * (desiredFunds - initialFunds);
                if (currentFunds >= desiredFunds)
                    currentFunds = desiredFunds;
            }
            else
            {
                currentFunds -= (warFundAnimationTime * Time.fixedDeltaTime) * (initialFunds - desiredFunds);
                if (currentFunds <= desiredFunds)
                    currentFunds = desiredFunds;
            }

            UpdateHUD("funds");

        }
    }

    public void WaveStartTimer(int time)
    {
        titleText.gameObject.SetActive(true);
        instCountDown = StartCoroutine(StartingWaveCountDown(time));
    }

    public void WaveStopTimer()
    {
        StopCoroutine(instCountDown);
        titleText.gameObject.SetActive(false);
    }

    WaitForSeconds StartingWaveCountDownWaitForSeconds = new WaitForSeconds(1);
    IEnumerator StartingWaveCountDown(int time)
    {
        for (int i = time; i > 0; i--)
        {
            titleText.text = i.ToString();
            yield return StartingWaveCountDownWaitForSeconds;
        }
        SpawnerManager.Instance.StartSpawning();
        WaveStopTimer();
    }
}

