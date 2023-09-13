namespace SchoolLib
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Salary { get; set; }

        
        public void ValidateName()
        {
            if (Name == null)
            {
                throw new ArgumentNullException("Name");
            }
            if (Name == "")
            {
                throw new ArgumentException("Name cannot be empty", "Name");
            }

        }

        public void Validate()
        {
            {
                ValidateName();
            }
        }
    }
}