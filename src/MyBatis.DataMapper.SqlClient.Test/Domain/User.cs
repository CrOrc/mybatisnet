using System.Collections.Generic;

namespace MyBatis.DataMapper.SqlClient.Test.Domain
{
    public class ApplicationUser
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string UserName
        {
            get { return name; }
            set { name = value; }
        }

        private Address address;
        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        private IList<Role> roles;
        public IList<Role> Roles
        {
            get { return roles; }
            set { roles = value; }
        }

    }
}
