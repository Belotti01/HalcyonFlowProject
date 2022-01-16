using System.Text;

namespace HalcyonFlowProject.Logic {
    public static class ConfigFile {
        private const string FOLDER = "./Config/";
        private const string EXT = ".cfg";
        public static string Database => FOLDER + "Database" + EXT;

        public static void Write(string path, string value) {
            CreateFolder(path);
            File.WriteAllText(path, Encode(value));
        }

        public static void WriteLines(string path, string[] lines) {
            CreateFolder(path);
            File.WriteAllLines(path, EncodeLines(lines));
        }

        public static string Read(string path) {
            return Decode(File.ReadAllText(path));
        }

        public static string[] ReadLines(string path) {
            return DecodeLines(File.ReadAllLines(path));
        }



        private static string Encode(string value) {
            byte[] toEncodeAsBytes
                  = Encoding.ASCII.GetBytes(value);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        private static string[] EncodeLines(params string[] lines) {
            string[] result = new string[lines.Length];
            for (int i = 0; i < result.Length; i++) {
                result[i] = Encode(lines[i]);
            }
            return result;
        }

        private static string Decode(string value) {
            byte[] encodedDataAsBytes
                = Convert.FromBase64String(value);
            return Encoding.ASCII.GetString(encodedDataAsBytes);
        }

        private static string[] DecodeLines(params string[] lines) {
            string[] result = new string[lines.Length];
            for (int i = 0; i < result.Length; i++) {
                result[i] = Decode(lines[i]);
            }
            return result;
        }

        private static void CreateFolder(string filepath) {
            if(filepath is null) return;
            string? folder = Path.GetDirectoryName(filepath);

            if(folder is null) return;
            Directory.CreateDirectory(folder);
        }
    }
}
