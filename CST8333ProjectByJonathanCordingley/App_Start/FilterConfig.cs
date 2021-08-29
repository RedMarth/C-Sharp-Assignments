using System.Web;
using System.Web.Mvc;

namespace CST8333ProjectByJonathanCordingley
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
