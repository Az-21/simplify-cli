namespace Simplify;

public sealed class FileMetadata
{
  public string FullPath { get; }
  public string Name { get; }
  public string Extension { get; }
  public string NameWithExtension { get; }
  public string Directory { get; }

  public FileMetadata(string fullPath)
  {
    // Windows will return path with "\", UNIX will return path with "/"
    FullPath = fullPath.Replace('\\', '/');

    // Extrapolate
    Name = Path.GetFileNameWithoutExtension(FullPath);
    Extension = Path.GetExtension(FullPath);
    NameWithExtension = Path.GetFileName(FullPath);
    Directory = Path.GetDirectoryName(FullPath) ?? "/";
  }
}

public sealed class FolderMetadata
{
  public string FullPath { get; }
  public string ParentDirectory { get; }
  public string Name { get; }

  public FolderMetadata(string fullPath)
  {
    // Windows will return path with "\", UNIX will return path with "/"
    FullPath = fullPath.Replace('\\', '/');

    // Extrapolate
    ParentDirectory = Path.GetDirectoryName(FullPath)!;
    Name = FullPath.Split('/').Last();
  }
}
