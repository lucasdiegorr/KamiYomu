using System.Net;

using Microsoft.AspNetCore.Http;

namespace KamiYomu.Web.Extensions;

public static class UriExtensions
{
    private static IHttpContextAccessor? _httpContextAccessor;

    internal static void SetHttpContextAccessor(IHttpContextAccessor? accessor)
    {
        _httpContextAccessor = accessor;
    }

    public static bool IsValidImageUri(this Uri uri)
    {
        if (uri == null || !uri.IsAbsoluteUri)
        {
            return false;
        }
        string[] validExtensions = [".jpg", ".jpeg", ".png", ".gif", ".bmp", ".ico", ".svg"];
        string extension = Path.GetExtension(uri.LocalPath).ToLowerInvariant();
        return validExtensions.Contains(extension);
    }

    public static string GetFileNameFromUri(this Uri uri)
    {
        return Path.GetFileName(uri.LocalPath);
    }

    public static string GetContentType(this Uri uri)
    {
        string extension = Path.GetExtension(uri.LocalPath).ToLowerInvariant();
        return ExtensionToContentType(extension);
    }

    public static string ExtensionToContentType(string extension)
    {
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            ".svg" => "image/svg+xml",
            ".ico" => "image/x-icon",
            _ => "application/octet-stream"
        };
    }

    public static string ToEncodedString(this Uri uri)
    {
        return WebUtility.UrlEncode(uri.ToString());
    }

    public static Uri ToInternalImageUrl(this Uri uri)
    {
        if (!uri.IsValidImageUri())
            return uri;

        var pathBase = _httpContextAccessor?.HttpContext?.Request.PathBase ?? "";
        return new Uri($"{pathBase}/Libraries/Collection/Index?handler=Image&uri={uri.ToEncodedString()}", UriKind.Relative);
    }



}
