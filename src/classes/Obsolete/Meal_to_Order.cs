using RestaurantsClasees.OrderSystem;

namespace RestaurantsClasses.Obsolete
{
    // блюдо в заказе, для реализации связи многие ко многим - уже не используется
    [Obsolete]
    public class Meal_to_Order
    {
        // заказ
        public Order Order { get; }

        // блюдо
        public Meal Meal { get; }

        // количество
        public int Count { get; }

        // конструктор
        public Meal_to_Order(Order order, Meal meal, int count)
        {
            if (order is null)
            {
                throw new Exception("Заказ обязан существовать");
            }

            if (meal is null)
            {
                throw new Exception("Блюдо обязано существовать");
            }

            if (count <= 0)
            {
                throw new Exception("Количество блюд не может быть отрицательным или равно нулю");
            }

            Order = order;
            Meal = meal;
            Count = count;
        }
    }
}
