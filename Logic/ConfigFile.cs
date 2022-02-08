using System.Text;

namespace HalcyonFlowProject.Logic {
    public static class ConfigFile {
        private const int MAX_ACTION_ATTEMPTS = 5;
        private static DelegateResolver Delegator = new();
        private const string FOLDER = "./Config/";
        private const string EXT = ".cfg";
        public static string Database => FOLDER + "Database" + EXT;

        /// <summary>
        /// Write the encoded <paramref name="value"/> into the file identified by its <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="value">The content to encode and write in the file.</param>
        public static void Write(string path, string value) {
            Delegator.EnqueueAction(() => {
                CreateFolder(path);
                if(File.Exists(path)) {
                    File.Delete(path);
                }
                File.WriteAllText(path, Encode(value));
            }, MAX_ACTION_ATTEMPTS);
        }


        /// <summary>
        /// Write the encoded <paramref name="lines"/> into the file identified by its <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="lines">The lines to encode and write in the file.</param>
        public static void WriteLines(string path, string[] lines) {
            Delegator.EnqueueAction(() => {
                CreateFolder(path);
                if(File.Exists(path)) {
                    File.Delete(path);
			    }
                File.WriteAllLines(path, EncodeLines(lines));
            }, MAX_ACTION_ATTEMPTS);
        }

        /// <summary>
        /// Read and decode the contents of the file.
        /// </summary>
        /// <param name="path">The path to the file to read.</param>
        /// <returns>The decoded content of the file, or an empty <see langword="string"/> if the file doesn't exist.</returns>
        public static string Read(string path) {
            return string.IsNullOrWhiteSpace(path) || File.Exists(path)
                ? Decode(File.ReadAllText(path))
                : string.Empty;
        }

        /// <summary>
        /// Read and decode the lines of the file.
        /// </summary>
        /// <param name="path">The path to the file to read.</param>
        /// <returns>The decoded lines of the file, or an empty array if the file doesn't exist.</returns>
        public static string[] ReadLines(string path) {
			return string.IsNullOrWhiteSpace(path) || File.Exists(path) 
                ? DecodeLines(File.ReadAllLines(path)) 
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
            for (int i = 0; i < result.Length; i++) {
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
            for (int i = 0; i < result.Length; i++) {
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
    }
}
