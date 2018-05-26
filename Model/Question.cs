using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSUQuest.Model
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Answer { get; set; }
        public int Number { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
