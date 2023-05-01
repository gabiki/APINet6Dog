using firstAPI.Domain;

namespace firstAPI.DogService
{
    public interface IDogService
    {
        string EstimatedAge(int age, Dog dog);
    }
}