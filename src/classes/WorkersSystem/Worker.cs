using Newtonsoft.Json;
using RestaurantsClasses.Enums;

namespace RestaurantsClasses.WorkersSystem
{
    // сотрудник
    public class Worker: Model
    {
        // id должности
        public int PositionId;

        // имя
        public string FirstName { get; set; }

        // фамилия 
        public string LastName { get; set; }

        // телефон 
        public string? Phone { get; set; }

        // имя пользователя
        public string Username { get; set; }

        // имя пользователя
        public string Password { get; set; }

        // должность сотрудника
        //public Position Position => Database.GetObject<Position>($"id = {_position_id}").FirstOrDefault();

        // конструктор
        public Worker(int id, string firstName, string lastName, string? phone, int position_id, string username, string password) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            PositionId = position_id;
            Username = username;
            Password = password;
        }

        public Worker(object[] items) : base((int)items[0])
        {
            FirstName = items[1].ToString();
            LastName = items[2].ToString();
            Phone = items[3] is null ? null : items[3].ToString();
            PositionId = (int)items[4];
            Username = items[5].ToString();
            Password = items[6].ToString();
        }

        public Worker()
        {

        }

        // текстовый вывод
        //public override string ToString()
        //{
        //    return $"{Position.Name} {FirstName} {LastName}";
        //}
    }
}
