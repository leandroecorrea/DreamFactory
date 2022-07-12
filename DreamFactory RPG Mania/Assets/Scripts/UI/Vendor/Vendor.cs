using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField] private GameObject itemRow;
    [SerializeField] private GameObject content;
    [SerializeField] private List<ItemConfig> items;
    [SerializeField] private Cart cart;

    private void OnEnable()
    {
        items.ForEach(item => { 
            var itemPrefab = Instantiate(itemRow);
            var itemRowComponent = itemPrefab.GetComponent<ItemRow>();
            itemRowComponent.SetRow(item);
            itemRowComponent.button.onClick.AddListener(delegate {                
                cart.AddToCart(item); 
            });
            itemPrefab.transform.SetParent(content.transform);
        });
    }
}