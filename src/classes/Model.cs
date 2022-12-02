namespace RestaurantsClasses
{
    // базовый общий класс для всего
    public abstract class Model
    {
        public virtual int Id { get; }

        public Model(int id)
        {
            Id = id;
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
