﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imya.Utils
{
    public static class CollectionExtension
    {
        public static IEnumerable<TResult> SelectNoNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult?> selector) where TResult : class
        {
            return source.Select(selector).Where(x => x is not null).Select(x => x!);
        }
    }

    public static class DirectoryEx
    {
        /// <summary>
        /// Delete target folder before moving.
        /// </summary>
        public static void CleanMove(string source, string target)
        {
            if (Directory.Exists(target))
                Directory.Delete(target, true);
            Directory.Move(source, target);
        }

        /// <summary>
        /// Recursively copy a folder.
        /// </summary>
        public static void Copy(string source, string target)
        {
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            foreach (string file in Directory.EnumerateFiles(source))
                File.Copy(file, Path.Combine(target, Path.GetFileName(file)));
            foreach (string folder in Directory.EnumerateDirectories(source))
                Copy(folder, Path.Combine(target, Path.GetFileName(folder)));
        }

        /// <summary>
        /// Delete target folder before recursively copying source into it.
        /// </summary>
        public static void CleanCopy(string source, string target)
        {
            if (Directory.Exists(target))
                Directory.Delete(target, true);
            Copy(source, target);
        }

        /// <summary>
        /// Delete folder if it exists.
        /// </summary>
        /// <param name="path"></param>
        public static void EnsureDeleted(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
    }
}