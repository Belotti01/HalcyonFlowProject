using System.Text;

namespace HalcyonFlowProject.Data.Settings {
    public class DatabaseSettings {
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

        /// <summary>
        /// Save the settings to the database configuration file.
        /// </summary>
        public void Save() => Save(ConfigFile.Database);
        /// <summary>
        /// Save the settings to a custom configuration file.
        /// </summary>
        /// <param name="filepath">The path to the configuration file.</param>
        public void Save(string filepath) {
            try {
                string[] data = { Host, DatabaseName, Username, Password };
                ConfigFile.WriteLines(ConfigFile.Database, data);
            }catch { }
        }

        /// <summary>
        /// Load the settings from the database configuration file.
        /// </summary>
        public void Load() => Load(ConfigFile.Database);
        /// <summary>
        /// Load the settings from a custom configuration file.
        /// </summary>
        /// <param name="filepath">The path to the configuration file.</param>
        public void Load(string filepath) {
            string[] data;
            try {
                data = ConfigFile.ReadLines(ConfigFile.Database);
                Host = data[0];
                DatabaseName = data[1];
                Username = data[2];
                Password = data[3];
            } catch { }
        }

        // Not implementing ICloneable cuz it sucks
        public DatabaseSettings Clone() {
            return new DatabaseSettings(false) {
                Host = Host,
                DatabaseName = DatabaseName,
                Username = Username,
                Password = Password
            };
        }

        public override string ToString() => GetConnectionString();
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

    }
}
