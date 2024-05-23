using UnityEngine;

public class ParticleManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject soap_particles_go;
    private ParticleSystem soap_particles;
    [SerializeField] private GameObject water_particles_go;
    private ParticleSystem[] water_particles;
    [SerializeField] private GameObject paper_particles_go;
    private ParticleSystem paper_particles;
    

    [SerializeField] private GameObject wave_go;
    private ParticleSystem[] wave_particles;
//...
    void Awake()
    {
        if(soap_particles_go != null)
        {
            soap_particles = soap_particles_go.GetComponent<ParticleSystem>();
            disableSoapParticles();
        }
        if(paper_particles_go != null)
        {
            paper_particles = paper_particles_go.GetComponent<ParticleSystem>();
            disablePaperParticles();
        }
        
        if(wave_go != null && wave_go.transform.childCount != 0)
        {
            wave_particles = new ParticleSystem[wave_go.transform.childCount];
            for(int i = 0; i <  wave_go.transform.childCount; i++)
            {
                wave_particles[i] = wave_go.transform.GetChild(i).GetComponent<ParticleSystem>();
            }

            disableWaveParticles();
        }
        if(water_particles_go != null && water_particles_go.transform.childCount != 0)
        {
            water_particles = new ParticleSystem[water_particles_go.transform.childCount];
            for(int i = 0; i <  water_particles_go.transform.childCount; i++)
            {
                water_particles[i] = water_particles_go.transform.GetChild(i).GetComponent<ParticleSystem>();
            }

            disableWaterParticles();
        }
    }

    public void manageParticleEvent(Component sender, string function, object data)
    {
        switch(function)
        {
            case "enableTouchParticles":
                enableTouchParticles((string) data);
                break;
            case "disableTouchParticles":
                disableTouchParticles((string) data);
                break;
            case "enableWaveParticles":
                enableWaveParticles();
                break;
            case "disableWaveParticles":
                disableWaveParticles();
                break;
        }
    }

    private void enableTouchParticles(string data)
    {
        switch(data)
        {
            case "soap":
                enableSoapParticles();
            break;
            case "water":
                enableWaterParticles();
            break;
            case "paper":
                enablePaperParticles();
            break;
        }
    }
    
    private void disableTouchParticles(string data)
    {
        switch(data)
        {
            case "soap":
                disableSoapParticles();
            break;
            case "water":
                disableWaterParticles();
            break;
            case "paper":
                disablePaperParticles();
            break;
        }
    }

    public void enableSoapParticles()
    {
        soap_particles.Play(true);
    }

    public void disableSoapParticles()
    {
        //particles.Stop();
        soap_particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    }

    public void enablePaperParticles()
    {
        paper_particles.Play(true);
    }

    public void disablePaperParticles()
    {
        //particles.Stop();
        paper_particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

    }

    public void enableWaveParticles()
    {
        foreach(ParticleSystem particles in wave_particles)
        {
            particles.Play(true);
        }
    }

    public void disableWaveParticles()
    {
        foreach(ParticleSystem particles in wave_particles)
        {
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

        public void enableWaterParticles()
    {
        foreach(ParticleSystem particles in water_particles)
        {
            particles.Play(true);
        }
    }

    public void disableWaterParticles()
    {
        foreach(ParticleSystem particles in water_particles)
        {
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
