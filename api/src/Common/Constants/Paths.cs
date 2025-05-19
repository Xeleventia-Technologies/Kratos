namespace Kratos.Api.Common.Constants;

public static class Folders
{
    public readonly record struct Upload(string Name)
    {
        public static readonly Upload Directory = new("uploads");

        public static readonly Upload Artciles = new($"{Directory}/articles");
        public static readonly Upload Services = new($"{Directory}/services");
        public static readonly Upload Members = new($"{Directory}/members");
        public static readonly Upload DisplayPictures = new($"{Directory}/display-pictures");

        public override string ToString() => Name;
    }
}
