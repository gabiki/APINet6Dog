using firstAPI.Domain;

namespace firstAPI.DogService
{
    public class DogService : IDogService
    {
        public string EstimatedAge(int age, Dog dog)
        {
            var newAge = age + dog.age;
            return $"{dog.name} will be {newAge}.";
        }
    }
}
