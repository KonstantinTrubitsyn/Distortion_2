using UnityEngine;

public class InstrumentClick : MonoBehaviour
{
    public InstrumentManager instrumentManager;

    private void OnMouseDown()
    {
        instrumentManager.OnInstrumentClicked(gameObject);
    }
}
