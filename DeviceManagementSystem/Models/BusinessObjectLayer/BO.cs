using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceManagementSystem.Models.BusinessObjectLayer
{
    public class BO : DataAccessLayer.DAL
    {
        public string Operation { get; set; }
        public int Code { get; set; }
        public int StaffID { get; set; }
        public string Search { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DistID { get; set; }
        public int StateID { get; set; }
        public string DistrictName { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
    }
}