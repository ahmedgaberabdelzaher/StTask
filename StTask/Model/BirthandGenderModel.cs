using System;
namespace StTask.Model
{
    public class BirthandGenderModel
    {
        public int BirthDate { get; set; }
        public int Gender { get; set; }
        public BirthandGenderModel()
        {

        }
        public BirthandGenderModel(int birthDate,int gender)
        {
            BirthDate = birthDate;
            Gender = gender;
        }
    }
    enum Gender
    {
        Male,Female
    }
}
