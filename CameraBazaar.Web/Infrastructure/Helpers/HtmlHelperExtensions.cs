namespace CameraBazaar.Web.Infrastructure.Helpers
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class HtmlHelperExtensions
    {
        public static IHtmlContent Stock(this IHtmlHelper helper, int quantity)
        {
            if (quantity == 0)
            {
                return new HtmlString(@"<span style=""color: red; font-size: large"">OUT OF STOCK</span>");
            }

            return new HtmlString(@"<span style=""color: green; font-size: large"">IN STOCK</span>");
        }
        
        public static IHtmlContent EditProfile(this IHtmlHelper helper, string logInUserName, string profileUserName)
        {
            HtmlString result = new HtmlString(string.Empty);

            if (logInUserName.Equals(profileUserName))
            {
                result = new HtmlString(@"<a href=""/Users/Edit"" class=""btn btn-primary btn-lg"">Edit Profile</a>");
            }

            return result;
        }
    }
}
