namespace ToDoList
{
    using Microsoft.AspNet.Builder;
    using Nancy.Owin;
    using Nancy;
    using System.IO;
    using System.Collections.Generic;
    using Nancy.ViewEngines.Razor;
    using System.Data;
    using System.Data.SqlClient;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {

            app.UseOwin(x => x.UseNancy());
        }
    }
    public class CustomRootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Directory.GetCurrentDirectory();
        }
    }

    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            return null;
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            return null;
        }

        public bool AutoIncludeModelNamespace
        {
            get { return false; }
        }
    }

    public static class DBConfiguration
    {
        public static string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=todo;Integrated Security=SSPI;";
    }
}
