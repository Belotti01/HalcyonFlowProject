using System.Text;

namespace HalcyonFlowProject.Logic {
	public static class ConfigFile {
		// Due to files being accessible from multiple sources, retry 
		// write operations up to MAX_ACTION_ATTEMPTS times with a DelegateResolver.
		private const int MAX_ACTION_ATTEMPTS = 5;
		private static DelegateResolver? Delegator;
		private static SafeFileReader? Reader;

		private static readonly string FOLDER = Path.Combine(Environment.CurrentDirectory, "Config");
		private const string EXTENSION = ".cfg";

		#region Filepaths
		// Getters of the absolute filepaths to the configuration files.
		// Should always start with the FOLDER path, and use the preset EXTENSION.

		/// <summary>Absolute path to the file containing database connection information.</summary>
		public static string Database => Path.Combine(FOLDER, $"Database{EXTENSION}");
		public static string DatabaseTesting => Path.Combine(FOLDER, $"DatabaseTesting{EXTENSION}");
		#endregion

		/// <summary>
		/// Write the encoded <paramref name="value"/> into the file identified by its <paramref name="path"/>.
		/// </summary>
		/// <param name="path">The path to the file.</param>
		/// <param name="value">The content to encode and write in the file.</param>
		public static void Write(string path, string value, Delegate? onFinish = null) {
			InitializeDelegator();

			Delegator!.EnqueueAction(() => {
				CreateFolder(path);
				if(File.Exists(path)) {
					File.Delete(path);
				}
				File.WriteAllText(path, Encode(value));
			}, MAX_ACTION_ATTEMPTS, onFinish);
		}


		/// <summary>
		/// Write the encoded <paramref name="lines"/> into the file identified by its <paramref name="path"/>.
		/// </summary>
		/// <param name="path">The path to the file.</param>
		/// <param name="lines">The lines to encode and write in the file.</param>
		public static void WriteLines(string path, string[] lines, Delegate? onFinish = null) {
			InitializeDelegator();

			Delegator!.EnqueueAction(() => {
				CreateFolder(path);
				if(File.Exists(path)) {
					File.Delete(path);
				}
				File.WriteAllLines(path, EncodeLines(lines));
			}, MAX_ACTION_ATTEMPTS, onFinish);
		}

		/// <summary>
		/// Read and decode the contents of the file.
		/// </summary>
		/// <param name="path">The path to the file to read.</param>
		/// <returns>The decoded content of the file, or an empty <see langword="string"/> if the file doesn't exist.</returns>
		public static string Read(string path) {
			InitializeReader();

			return string.IsNullOrWhiteSpace(path) || File.Exists(path)
				? Decode(Reader!.Read(path))
				: string.Empty;
		}

		/// <summary>
		/// Read and decode the lines of the file.
		/// </summary>
		/// <param name="path">The path to the file to read.</param>
		/// <returns>The decoded lines of the file, or an empty array if the file doesn't exist.</returns>
		public static string[] ReadLines(string path) {
			InitializeReader();

			return string.IsNullOrWhiteSpace(path) || File.Exists(path)
				? DecodeLines(Reader!.ReadLines(path))
				: Array.Empty<string>();
		}



		/// <summary>
		/// Encode the <paramref name="value"/> to a BASE64 <see langword="string"/>.
		/// </summary>
		/// <param name="value">The plain text.</param>
		/// <returns>The BASE64-encoded text.</returns>
		private static string Encode(string value) {
			byte[] toEncodeAsBytes
				  = Encoding.ASCII.GetBytes(value);
			return Convert.ToBase64String(toEncodeAsBytes);
		}

		/// <summary>
		/// Encode each of the <paramref name="lines"/> into a BASE64 <see langword="string"/>.
		/// </summary>
		/// <param name="lines">The plain-text lines.</param>
		/// <returns>The BASE64-encoded lines.</returns>
		private static string[] EncodeLines(params string[] lines) {
			string[] result = new string[lines.Length];
			for(int i = 0; i < result.Length; i++) {
				result[i] = Encode(lines[i]);
			}
			return result;
		}

		/// <summary>
		/// Decode the <paramref name="value"/> from a BASE64 <see langword="string"/> into the equivalent 
		/// plain text.
		/// </summary>
		/// <param name="value">The BASE64-encoded text.</param>
		/// <returns>The plain text.</returns>
		private static string Decode(string value) {
			byte[] encodedDataAsBytes
				= Convert.FromBase64String(value);
			return Encoding.ASCII.GetString(encodedDataAsBytes);
		}

		/// <summary>
		/// Decode each of the <paramref name="lines"/> from BASE64.
		/// </summary>
		/// <param name="lines">The BASE64-encoded text.</param>
		/// <returns>The plain text.</returns>
		private static string[] DecodeLines(params string[] lines) {
			string[] result = new string[lines.Length];
			for(int i = 0; i < result.Length; i++) {
				result[i] = Decode(lines[i]);
			}
			return result;
		}

		/// <summary>
		/// Create, if it doesn't exist yet, the folder to store the file into.
		/// </summary>
		/// <param name="filepath">The path to the file to save.</param>
		private static void CreateFolder(string filepath) {
			if(filepath is null) return;
			string? folder = Path.GetDirectoryName(filepath);

			if(folder is null) return;
			Directory.CreateDirectory(folder);
		}

		private static void InitializeDelegator() {
			// Replace the delegator with a new one, only if it's disposed or
			// about to be.
			if(Delegator?.DisposeOnFinish ?? false) {
				// Wait for the current delegator to be disposed internally
				while(!Delegator.Disposed) {
					Thread.Sleep(100);
				}
			}

			if(Delegator?.Disposed ?? true) {
				Delegator = new();
			}
		}

		private static void InitializeReader() {
			Reader ??= new(Path.Combine(FOLDER, "Temp"));
		}
	}
}
