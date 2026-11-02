using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Color tint = Color.aliceBlue;
    private Material runtimeMaterial;

    public Color Tint
    {
        get { return tint; }
        set { tint = value; }
    }

    private void Start()
    {
        runtimeMaterial = GetComponent<Renderer>().material;
        runtimeMaterial.color = tint;
    }

    private void OnDestroy()
    {
        if (runtimeMaterial != null)
        {
            Destroy(runtimeMaterial);
        }   
    }
}
