using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SmartTask
    {
        

        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime ExpDate { get; private set; }
        public int Priority { get; private set; }
        public List<string> Labels { get; private set; } = new List<string>();

        public SmartTask(string title, string description, DateTime expDate, int priority, List<string> labels)
        {
            Title = title;
            Description = description;
            ExpDate = expDate;
            Priority = priority;
            Labels = labels;
        }

        public void SetTitle (string title) { Title = title; }  
    }
}
