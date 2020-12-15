using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKP_IntranetSideAPI.DB_Settings
{
    public class LoginDBSettings : ILoginDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
    public interface ILoginDBSettings
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }

    public class SaltDBSettings : ISaltDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
    public interface ISaltDBSettings
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }

    public class UserDBSettings : IUserDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
    public interface IUserDBSettings
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }

    public class ProjectDBSettings : IProjectDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
    public interface IProjectDBSettings
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }
    public class ForumDBSettings : IForumDBSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Collection { get; set; }
    }
    public interface IForumDBSettings
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
        string Collection { get; set; }
    }
}
