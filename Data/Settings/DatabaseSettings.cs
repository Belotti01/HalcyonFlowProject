using System.Text;

namespace HalcyonFlowProject.Data.Settings {
    public class DatabaseSettings : ICloneable {
        // These properties will be binded to a razor component, so initialize
        // them statically to always avoid a NullValueException.
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 3306;
        public string DatabaseName { get; set; } = string.Empty;
        public string Username { get; set; } = "root";
        public string Password { get; set; } = string.Empty;

        public DatabaseSettings(bool loadPrevious = true) {
            if (loadPrevious) {
                Load();
            }
        }

        public void Save() {
            try {
                string[] data = { Host, DatabaseName, Username, Password };
                ConfigFile.WriteLines(ConfigFile.Database, data);
            }catch { }
        }

        public void Load() {
            string[] data;
            try {
                data = ConfigFile.ReadLines(ConfigFile.Database);
                Host = data[0];
                DatabaseName = data[1];
                Username = data[2];
                Password = data[3];
            } catch { }
        }

        public object Clone() {
            return new DatabaseSettings(false) {
                Host = Host,
                DatabaseName = DatabaseName,
                Username = Username,
                Password = Password
            };
        }

        public string GetConnectionString() {
            string connectionString = string.Concat(
                $"server={Host};",
                $"database={DatabaseName};",
                $"port={Port};",
                $"user={Username};",
                $"password={Password}"
            );

            return connectionString;
        }

        public override string ToString() => GetConnectionString();
    }
}
