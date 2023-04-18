using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);

    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeName.SetText(recipeSO.recipeName);

        foreach (Transform item in iconContainer)
        {
            if (item == iconTemplate)
            {
                continue;
            }
            else
            {
                Destroy(item.gameObject);
            }
            
        }

        foreach (var kitchenObjSO in recipeSO.kitchenObjSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjSO.sprite;
        }
    }
}
