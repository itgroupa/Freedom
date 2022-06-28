using Freedom.Auth.Web.Resources;

namespace Freedom.Auth.Web.Utils;

public static class SiteTitleLocal
{
    public static string GetTitle(string chapter) => $"{Translate.TitleSite} - {chapter}";
}
