using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniExcelLibs.Attributes;
using static ModelsLib.Const.ColumName;

namespace ModelsLib
{
    public class MissionModel
    {
        [ExcelColumnName(ACCOUNT)] 
        public string Account { get; set; }
        [ExcelColumnName(PD)]
        public string Password { get; set; }
        [ExcelColumnName(SITE)] 
        public string Site { get; set; }
        [ExcelColumnName(IP)] 
        public string Ip { get; set; }
        [ExcelColumnName(TITLE)] 
        public string Title { get; set; }
        [ExcelColumnName(CODE)]
        public string Code { get; set; }
        [ExcelColumnName(DATE)]
        public string Date { get; set; }

        public override bool Equals(object? model)
        {
            if (model == null || this.GetType() != model.GetType())
            {
                return false;
            }
            var m = (MissionModel)model;
            return Equals(m);
        }

        private bool Equals(MissionModel other)
        {
            return Title == other.Title;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
