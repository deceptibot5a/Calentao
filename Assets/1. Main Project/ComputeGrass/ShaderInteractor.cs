using UnityEngine;
 
public class ShaderInteractor : MonoBehaviour
{
    public float radius = 1f;
    
    void Update()
    {
        Shader.SetGlobalVector("_PositionMoving", transform.position);
    }
}