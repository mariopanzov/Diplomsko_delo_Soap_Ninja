using UnityEngine;

public class ParticleManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject _soap_particles_on_touch_go;
    private ParticleSystem _soap_particles;

    [SerializeField] private GameObject _wave;
    private ParticleSystem[] _wave_particles;

    void Awake()
    {
        _soap_particles = _soap_particles_on_touch_go.GetComponent<ParticleSystem>();
        disable_soap_particles_touch();
        
        if(_wave != null && _wave.transform.childCount != 0)
        {
            _wave_particles = new ParticleSystem[_wave.transform.childCount];
            for(int i = 0; i <  _wave.transform.childCount; i++)
            {
                _wave_particles[i] = _wave.transform.GetChild(i).GetComponent<ParticleSystem>();
            }

            disable_wave_particles();
        }


    }

    public void enable_soap_particles_touch()
    {
        _soap_particles.Play(true);
    }

    public void disable_soap_particles_touch()
    {
        //particles.Stop();
        _soap_particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    }

    public void enable_wave_particles()
    {
        foreach(ParticleSystem particles in _wave_particles)
        {
            particles.Play(true);
        }
    }

    public void disable_wave_particles()
    {
        foreach(ParticleSystem particles in _wave_particles)
        {
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
    

}
