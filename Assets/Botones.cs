using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Botones : MonoBehaviour
{
    public Button btnInicio;
    public Button btnPreventivas;
    public Button btnApagar;
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        Button btnI = btnInicio.GetComponent<Button>();
        Button btnP = btnPreventivas.GetComponent<Button>();
        Button btnA = btnApagar.GetComponent<Button>();
        timer = GameObject.Find("Main Camera").GetComponent<Timer>();

        btnI.onClick.AddListener(Iniciar);
        btnP.onClick.AddListener(Preventivas);
        btnA.onClick.AddListener(Apagar);
    }

    public void Iniciar()
    {
        timer.Opcion = Timer.EstadoOpcion.Inicio;
        timer.TiempoPasoMedioSegundo = 0;
    }

    public void Preventivas()
    {
        timer.Opcion = Timer.EstadoOpcion.Preventivo;
        timer.TiempoPasoMedioSegundo = 0;
        timer.halfSecond = 0.5f;
    }

    public void Apagar()
    {
        timer.Opcion = Timer.EstadoOpcion.Apagado;
        timer.TiempoPasoMedioSegundo = 0;
    }
}
