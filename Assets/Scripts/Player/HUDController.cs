using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class HUDController : MonoSingleton<HUDController>
{
    GameObject _placeHolder;
    [SerializeField] GameObject TurretInPool1; //CHANGE IT!
    [SerializeField] GameObject TurretInPool2; //CHANGE IT!
    [SerializeField] Text _livesCount, _wavesCount, _warFundCount;
    [SerializeField] Sprite[] _playButtonSprites, _fastFowardButtonSprites;
    [SerializeField] Image fastForward, slowForward;
    [SerializeField] TextMeshProUGUI titleText;

    


    public static Action<bool> PlacingTurret;


    private void Start()
    {
        UpdateHUD();

    }

    // Update is called once per frame
    void Update()
    {
        if (_placeHolder != null)
            PlaceHolderFolowing();
        
    }

    private void PlaceHolderFolowing()
    {
        if (_placeHolder != null)
        {
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                if (hitInfo.transform.tag == "TurretSpot")
                {

                    _placeHolder.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + .5f, hitInfo.transform.position.z);

                    if (Input.GetMouseButtonDown(0))
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
        if (Time.timeScale != 0)
        {
            buttonPlay.sprite = _playButtonSprites[1];
            GameManager.Instance.ChangeTimeScale(0);
        }
        else
        {
            GameManager.Instance.ChangeTimeScale();
            buttonPlay.sprite = _playButtonSprites[0];
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
                _livesCount.text = TheHQ.Instance.GetHealth().ToString();
                break;

            case "waves":
                //_wavesCount.text = WaveManager.Instance.GetCurrentWave().ToString();
                break;

            case "funds":
                _warFundCount.text = GameManager.Instance.GetWarFunds().ToString();
                break;

            default:
                _livesCount.text = TheHQ.Instance.GetHealth().ToString();
                //_wavesCount.text = WaveManager.Instance.GetCurrentWave().ToString();
                _warFundCount.text = GameManager.Instance.GetWarFunds().ToString();
                break;
        }
    }

    
}

