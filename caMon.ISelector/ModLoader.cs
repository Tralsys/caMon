using System;
using System.IO;
using System.Reflection;

namespace caMon
{
	public static class ModLoader
	{
		/// <summary>dllから指定の型を持つクラスをロードする</summary>
		/// <typeparam name="T">ロードする型</typeparam>
		/// <param name="FPath">dllへのパス</param>
		/// <returns>読み込んだインスタンス</returns>
		/// <exception cref="FileNotFoundException">指定のファイルが存在しなかった場合にthrowされる</exception>
		/// <exception cref="EntryPointNotFoundException">指定のdllに指定のクラスが含まれていなかった場合にthrowされる</exception>
		public static T LoadDllInst<T>(string FPath) => (T)LoadCompatibleTypeFromDLL<T>(FPath).GetConstructor(Array.Empty<Type>()).Invoke(null);

		/// <summary>dllから指定の型に互換性がある持つクラスの型を取得する</summary>
		/// <typeparam name="T">ロードする型</typeparam>
		/// <param name="FPath">dllへのパス</param>
		/// <returns>読み込んだ型</returns>
		/// <exception cref="FileNotFoundException">指定のファイルが存在しなかった場合にthrowされる</exception>
		/// <exception cref="EntryPointNotFoundException">指定のdllに指定のクラスが含まれていなかった場合にthrowされる</exception>
		public static Type LoadCompatibleTypeFromDLL<T>(string FPath)
		{
			//ref : https://qiita.com/rita0222/items/609583c31cb7f0132086
			if (!File.Exists(FPath))
				throw new FileNotFoundException();

			var types = Assembly.LoadFrom(FPath).GetTypes();

			foreach (var t in types)
			{
				if (t.IsInterface)
					continue;

				if (typeof(T).IsAssignableFrom(t))
					return t;
			}

			throw new EntryPointNotFoundException("Search from:" + FPath);
		}
	}
}
