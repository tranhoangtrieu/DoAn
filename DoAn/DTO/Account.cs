using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DoAn.DTO
{
    public class Account
    {
        private int type;
        private string UserName;
        private string Password;
        private string DisplayName;

        public Account(int type, string userName, string password, string displayName)
        {
            this.type = type;
            this.UserName = userName;
            this.Password = password;
            this.DisplayName = displayName;
            
        }

        public Account(DataRow row)
        {
            this.type = (int)row["Type"];
            this.UserName = row["UserName"].ToString();
            this.Password = row["Password"].ToString();
            this.DisplayName = row["DisplayName"].ToString();
        }
        public Account() { }

        public int Type { get => type; set => type = value; }
        public string UserName1 { get => UserName; set => UserName = value; }
        public string Password1 { get => Password; set => Password = value; }
        public string DisplayName1 { get => DisplayName; set => DisplayName = value; }

        
    }
}
