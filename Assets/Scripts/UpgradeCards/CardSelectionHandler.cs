using System.Collections.Specialized;
using System.Threading;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField] private float _verticalMoveAmount = 30f;
    [SerializeField] private float _moveTime = 0.1f;
    [Range(0f, 2f), SerializeField] private float _scaleAmount = 1.1f;
    // Start is called before the first frame update

    private Vector3 _startPos;
    private Vector3 _startScale;

    [System.Serializable]
    public class UpgradeChoosen : UnityEvent<string> {}

    [System.Serializable]
    public class WeaponChoosen: UnityEvent<string> { }

    public UpgradeChoosen upgradeChoosen;
    public WeaponChoosen weaponChoosen;

    void Start()
    {
        _startPos = transform.position;
        _startScale = transform.localScale;

        if (gameObject.name.Contains("Upgrade")) {
            GetComponent<Button>().onClick.AddListener(UpgradeClicked);
        } else if (gameObject.name.Contains("Weapon")){
            GetComponent<Button>().onClick.AddListener(WeaponClicked);
        }        
    }

    private IEnumerator MoveCard(bool startingAnimation)
    {

        Vector3 endPosition;
        Vector3 endScale;
        float elapsedTime = 0f;
        while (elapsedTime < _moveTime)
        {

            elapsedTime += Time.deltaTime;

            if (startingAnimation)
            {
                endPosition = _startPos + new Vector3(0f, _verticalMoveAmount, 0f);
                endScale = _startScale * _scaleAmount;

            }
            else
            {
                endPosition = _startPos;
                endScale = _startScale;
            }
            //calculate the lerped amounts
            Vector3 lerpedPos = Vector3.Lerp(transform.position, endPosition, elapsedTime / _moveTime);
            Vector3 lerpedScale = Vector3.Lerp(transform.localScale, endScale, elapsedTime / _moveTime);
            //actually apply the changes to position and scale
            transform.position = lerpedPos;
            transform.localScale = lerpedScale;

            yield return null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //select the Card
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //deselect the card
        eventData.selectedObject = null;
    }

    public void OnSelect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(true));
    }

    public void OnDeselect(BaseEventData eventData)
    {
        StartCoroutine(MoveCard(false));

    }

    void UpgradeClicked()
    {
        name = gameObject.name.Split('-')[1];
        upgradeChoosen.Invoke(name.Substring(0, gameObject.name.IndexOf('(')));
    }

    void WeaponClicked()
    {
        name = gameObject.name.Split('-')[1];
        weaponChoosen.Invoke(name.Substring(0, gameObject.name.IndexOf('(')));
    }
}
