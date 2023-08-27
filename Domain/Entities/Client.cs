using Domain.Common;

namespace Domain.Entities
{
    public class Client:AuditableBaseEntity
    {
        private int age; // Backing field:EF
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string? Telephone { get; set; }   
        public string? Email { get; set; }
        public string? Address { get; set; }

        public int Age
        {
            get
            {
                if (this.age <= 0)
                {
                    this.age = new DateTime(DateTime.Now.Subtract(this.Birthday).Ticks).Year - 1;
                }
                return this.age;
            }
            set
            {
                this.age=value;
            }
        }
    }
}
