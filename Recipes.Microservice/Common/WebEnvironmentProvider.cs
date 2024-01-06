namespace Recipes.Microservice.Common;

public static class WebEnvironmentProvider
{
    public static IWebHostEnvironment? Host()
    {
        var accessor = new HttpContextAccessor();
        return accessor.HttpContext?.RequestServices.GetRequiredService<IWebHostEnvironment>();
    }
}