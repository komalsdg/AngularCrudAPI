using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCrudAPI.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public int Group { get; set; }
        public string PracticeType { get; set; }
        public string Area { get; set; }
        public string ProjectSize { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }


        public Project() { }
        public Project(int p_id, int p_group, string p_practicetype, string p_area, string p_projectsize, int p_quantity, DateTime p_startdate)
        {
            ProjectId = p_id;
            Group = p_group;
            PracticeType = p_practicetype;
            Area = p_area;
            ProjectSize = p_projectsize;
            Quantity = p_quantity;
            StartDate = p_startdate;
        }
    }
}
