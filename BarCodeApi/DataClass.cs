using Microsoft.AspNetCore.Http.HttpResults;
using System.Drawing;

namespace BarCodeApi
{
    public class DataClass
    {
        Random rnd = new Random();
        private string[] names = { "Heelo", "Dima", "Vasya", "Petya", "Oleg" };
        private string[] surnames = { "Heelo", "Dash", "Play", "Bones", "SNOT" };
        private string[] midnames = { "Heelo", "Dmitrievich", "Vasilievich", "Petrovich", "Olegvich" };
        
        //создай проверку использованных айдишников

        public string GetRandomName()
        {
            return names[rnd.Next(names.Length)];
        }
        public string GetRandomSurname()
        {
            return surnames[rnd.Next(surnames.Length)];
        }
        public string GetRandomMidname()
        {
            return midnames[rnd.Next(midnames.Length)];
        }

    }
}
