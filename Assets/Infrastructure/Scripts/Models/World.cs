using UniRx;

namespace Infrastructure.Scripts.Models
{
    public class World
    {  
        //Сохранение данных в файл на устройстве. Заготовка 
        public ReactiveProperty<int> EnemiesKilled { get; set; } = new ReactiveProperty<int>(0);

        public World()
        {
            //Реактивное
        }
    }
}