using UnityEngine;

public class Turret : MonoBehaviour
{
    private GameObject _model;

    public void SetModel(GameObject model)
    {
        if (model != null) 
            Destroy(_model);
        
        _model = Instantiate(model, transform.position, Quaternion.identity, transform);
    }

}
