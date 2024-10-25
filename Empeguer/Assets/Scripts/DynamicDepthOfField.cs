using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DynamicDepthOfField : MonoBehaviour
{
    public Transform player; // Assignez votre joueur dans l'inspecteur
    private DepthOfField depthOfField;

    void Start()
    {
        // Récupérer le Post Process Volume de la caméra
        PostProcessVolume postProcessVolume = GetComponent<PostProcessVolume>();
        
        // Récupérer le Depth of Field du volume
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out depthOfField);
        }
        
        // Vérifiez si le Depth of Field est trouvé
        if (depthOfField == null)
        {
            Debug.LogError("Depth of Field not found in the Post Process Volume!");
        }
    }

    void Update()
    {
        // Vérifiez que le Depth of Field et le joueur sont bien assignés
        if (depthOfField != null && player != null)
        {
            // Calculez la distance entre la caméra et le joueur
            float distance = Vector3.Distance(Camera.main.transform.position, player.position);
            
            // Ajustez la distance de mise au point en fonction de la distance calculée
            depthOfField.focusDistance.value = distance;
        }
    }
}
