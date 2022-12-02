using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantsClasses.OnlineSystem
{
    // модель онлайн заказа
    public class OnlineOrder
    {
        // id из базы
        public int Id { get; }

        // название должности
        public string Name { get; }

        // стандартный оклад
        public double Salary { get; }

        // процентный размер премии
        public double Prize { get; }

        // уровень прав
        public WorkerRole Role { get; }
    }
}
