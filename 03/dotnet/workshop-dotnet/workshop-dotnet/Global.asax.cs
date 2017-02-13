using System.Web.Http;

namespace workshop_dotnet
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CouchbaseConfig.Setup();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_End()
        {
            CouchbaseConfig.CleanUp();
        }
    }
}
