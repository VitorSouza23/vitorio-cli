using System;

namespace Vitorio.CLI.Model
{
    public class BirthDate
    {
        public string ByAge(int age)
        {
            DateTime bithDate = DateTime.Now.AddYears(age * -1);
            return bithDate.ToString("d");
        }
    }
}