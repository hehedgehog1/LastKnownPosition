using Models;
using TMPro;
using UnityEngine;

public class MisPerProfileHandler : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI fullname;
    [SerializeField] private TextMeshProUGUI age;
    [SerializeField] private TextMeshProUGUI lastSeen;
    [SerializeField] private TextMeshProUGUI physicalDescription;

    public void SetMisPerProfile(Profile profile)
    {
        fullname.text = profile.Name;
        age.text = profile.Age;
        lastSeen.text = profile.LastSeen;
        physicalDescription.text = profile.PhysicalDescription;
    }
}
