using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSUQuest.Model
{
    public class Result
    {
        public Guid Id { get; set; }
        public DateTime FinishDate { get; set; }
        public string Name { get; set;}
        public int Score { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
