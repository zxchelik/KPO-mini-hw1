using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class VetClinic : IVetClinic
{
    public bool CheckHealth(Animal animal, HealthStates state)
    {
        var isHealthy = state == HealthStates.Healthy;
        animal.IsHealthy = isHealthy;
        return isHealthy;
    }
}