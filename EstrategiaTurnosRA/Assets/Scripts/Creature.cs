using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    public Vector2Int localPosition;//posición en el mapa
    public GameObject selectionIndicator;

    // Start is called before the first frame update
    void Start()
    {
        this.SetSelectionStatus(false);
    }
    public void SetSelectionStatus(bool isSelected)
    {
        this.selectionIndicator.SetActive(isSelected);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
