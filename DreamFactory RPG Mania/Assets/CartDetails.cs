using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;

public class CartDetails : MonoBehaviour
{
    [SerializeField] private GameObject cartRowPrefab;
    [SerializeField] private GameObject cartView;
    [SerializeField] private Cart cart;
    //[SerializeField] private GameObject cartViewPrefab;
    [SerializeField] private TMP_Text cartAmount;

    private void OnEnable()
    {
        cart.items.ObserveAdd()
                  .Subscribe(onAddItem =>
                  {
                      var prefab = Instantiate(cartRowPrefab);
                      prefab.transform.SetParent(cartView.transform, false);
                      var component = prefab.GetComponent<CartRow>();
                      component.addButton.onClick.AddListener(()=>onAddItem.Value.Add());
                      component.removeButton.onClick.AddListener(()=>onAddItem.Value.Substract());
                      component.InitializeRow(onAddItem.Value.data.itemName, onAddItem.Value.data.price, onAddItem.Value.amount);
                      onAddItem.Value
                             .ObserveEveryValueChanged(item => item.amount)
                             .Subscribe(amount => 
                             {
                                 UpdateTotal();
                                 if(amount == 0)
                                 {
                                     cart.items.Remove(onAddItem.Value);
                                     Destroy(prefab);
                                 }
                                 else
                                 {
                                    component.UpdateValue(onAddItem.Value); 
                                 }
                             });
                  });
    }

    void UpdateTotal()
    {
        var total = 0;
        foreach(var item in cart.items)
        {
            total += item.amount * item.data.price;
        }
        cartAmount.text = $"Total: {total}";
    }
}
