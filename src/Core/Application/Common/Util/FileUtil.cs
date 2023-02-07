using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Common.Util;
public static class FileUtil
{
    #region 文件操作
    public static int GetRandom(int minNum, int maxNum)
    {
        int seed = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
        return new Random(seed).Next(minNum, maxNum);
    }

    public static FileInfo[] GetFiles(string directoryPath)
    {
        if (!IsExistDirectory(directoryPath))
        {
            throw new DirectoryNotFoundException();
        }

        var root = new DirectoryInfo(directoryPath);
        return root.GetFiles();
    }

    public static bool IsExistDirectory(string directoryPath)
    {
        return Directory.Exists(directoryPath);
    }

    public static string ReadFile(string Path)
    {
        string s;
        if (!File.Exists(Path))
        {
            s = "不存在相应的目录";
        }
        else
        {
            var f2 = new StreamReader(Path, Encoding.Default);
            s = f2.ReadToEnd();
            f2.Close();
            f2.Dispose();
        }

        return s;
    }

    public static void FileMove(string OrignFile, string NewFile)
    {
        File.Move(OrignFile, NewFile);
    }

    public static void CreateDir(string dir)
    {
        if (dir.Length == 0) return;
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }
    #endregion
}
