namespace Kratos.Api.Common.Constants;

public static class Folders
{
    public static class Uploads
    {
        public static readonly string Directory = "uploads";

        public static readonly string Avatars = $"{Directory}/avatars";
        public static readonly string Images = $"{Directory}/images";
    }

    public static class Images
    {
        public static readonly string Directory = "images";
    }
}

public static class Files
{
    public static class Images
    {
        public static readonly string Logo = $"{Folders.Images.Directory}/logo.png";
    }
}
