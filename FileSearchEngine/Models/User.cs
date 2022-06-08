using System;

namespace FileSearchEngine.Models
{
    public class User
    {
        public string Name { get; set; }
        
        public string LastName { get; set; }
        
        public DateTime BirthDay { get; set; }

        public override string ToString() => $"{Name} {LastName} ({BirthDay:dd.MM.yy})";
    }
}