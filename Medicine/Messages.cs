using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicine
{
    public static class Messages
    {
        public const string MainMenu = "Главное меню\n" +
                "Выберите один из вариантов (указать номер):\n" +
                "1. Товары,\n" +
                "2. Аптеки,\n" +
                "3. Склады,\n" +
                "4. Партий,\n" +
                "0. Выход";

        public const string Menu = "Выберите один из вариантов (указать номер):\n" +
            "1. Вывести все записи,\n" +
            "2. Добавить новую запись,\n" +
            "3. Удалить запись (по номеру),\n";

        public const string PharmacyMenu = "4. Вывести на экран весь список товаров и его количество в выбранной аптеке (количество товара во всех складах аптеки),";

        public const string EndMenu = "0. Назад";
    }
}
