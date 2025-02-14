using Core.Enums;
using Core.Models;

namespace Core.Interfaces;

public interface IVetClinic
{
    bool CheckHealth(Animal animal, HealthStates state);
}